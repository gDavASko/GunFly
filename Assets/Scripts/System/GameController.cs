using System.Collections.Generic;
using Events;
using UnityEngine;
using Zenject;

public sealed class GameController : MonoBehaviour
{
    private IUnitsFactory _unitsFactory = null;
    private ILevelFactory _levelFactory = null;

    private ILevel _currentLevel = null;
    private IInput _input = null;
    private Player _player = null;

    [Inject]
    public void Construct(IUnitsFactory unitsFactory, ILevelFactory levelsFactory, IInput input, GameEvents gameEvents)
    {
        _unitsFactory = unitsFactory;
        _levelFactory = levelsFactory;
        _input = input;

        gameEvents.OnGameLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded()
    {
        LoadGame();
    }


    private async void LoadGame()
    {
        if(_currentLevel != null)
            UnloadLevel();

        _currentLevel = await _levelFactory.CreateLevel<ILevel>("Level_0");

        _player = await _unitsFactory.CreateUnit<Player>("Player", _currentLevel.PlayerSpawnPoint,
            Quaternion.identity, _currentLevel.Transform, true);
        var controllers = _player.GetComponentsInChildren<IInputInit>();
        if (controllers != null)
        {
            foreach (var controller in controllers)
            {
                controller.Init(_input);
            }
        }

        foreach (KeyValuePair<string, Vector3> enemy in _currentLevel.EnemySpawnPoints)
        {
            _unitsFactory.CreateUnit<IUnit>(enemy.Key, enemy.Value,
                Quaternion.identity, _currentLevel.Transform, true);
        }
    }

    private void UnloadLevel()
    {
        _unitsFactory.ClearCached();
    }
}