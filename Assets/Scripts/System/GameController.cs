using System.Collections.Generic;
using Events;
using UnityEngine;
using Zenject;

public sealed class GameController : MonoBehaviour
{
    private IUnitsFactory _unitsFactory = null;
    private ILevelFactory _levelFactory = null;
    private IWeaponFactory _weaponFactory = null;

    private GameEvents _gameEvents = null;

    private ILevel _currentLevel = null;
    private IInput _input = null;
    private Player _player = null;

    [Inject]
    public void Construct(IUnitsFactory unitsFactory, ILevelFactory levelsFactory, IWeaponFactory weaponFactory,
        IInput input, GameEvents gameEvents)
    {
        _unitsFactory = unitsFactory;
        _levelFactory = levelsFactory;
        _weaponFactory = weaponFactory;
        _input = input;

        _gameEvents = gameEvents;
        gameEvents.OnGameLoaded += OnLevelLoaded;
        gameEvents.OnGameFinish += OnGameFinish;
    }

    private void OnLevelLoaded()
    {
        LoadGame();
    }


    private async void LoadGame()
    {
        if (_currentLevel != null)
            UnloadLevel();

        _currentLevel = await _levelFactory.CreateLevel<ILevel>("Level_0");
        _currentLevel.Init(_gameEvents);

        InitPlayer();
        InitEnemy();
    }

    private async void InitPlayer()
    {
        _player = await _unitsFactory.CreateUnit<Player>("Player", _currentLevel.PlayerSpawnPoint,
            Quaternion.identity, _currentLevel.Transform, true);
        _player.OnDeath += OnPlayerDeath;

        var weaponProcessor = _player.GetComponent<IWeaponProcessor>();
        weaponProcessor.SetWeapon(await _weaponFactory.CreateWeapon<IWeapon>("Weapon_sword"));

        var controllers = _player.GetComponentsInChildren<IInputInit>();
        if (controllers != null)
        {
            foreach (var controller in controllers)
            {
                controller.Init(_input);
            }
        }
    }

    private void InitEnemy()
    {
        foreach (KeyValuePair<string, Vector3> enemy in _currentLevel.EnemySpawnPoints)
        {
            _unitsFactory.CreateUnit<IUnit>(enemy.Key, enemy.Value,
                Quaternion.identity, _currentLevel.Transform, true);
        }
    }

    private void UnloadLevel()
    {
        _unitsFactory.ClearCached();
        _currentLevel.Dispose();
    }

    private void OnPlayerDeath()
    {
        Debug.LogError("Player Death");
        LoadGame();
    }

    private void OnGameFinish()
    {
        Debug.LogError("Game Finish");
        LoadGame();
    }
}