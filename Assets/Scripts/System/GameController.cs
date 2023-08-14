using System.Collections.Generic;
using System.Threading.Tasks;
using Events;
using ModestTree;
using UnityEngine;
using Zenject;

public sealed class GameController : MonoBehaviour
{
    public const string PLAYER_ID = "Player";
    public const string ENEMY_ID = "Enemy";

    private IUnitsFactory _unitsFactory = null;
    private ILevelFactory _levelFactory = null;
    private IWeaponFactory _weaponFactory = null;
    private IWeaponInitConfigAccessor _weaponInitConfigAccessor = null;

    private GameEvents _gameEvents = null;

    private ILevel _currentLevel = null;
    private IInput _input = null;
    private IUnit _player = null;

    [Inject]
    public void Construct(IUnitsFactory unitsFactory, ILevelFactory levelsFactory, IWeaponFactory weaponFactory,
        IInput input, GameEvents gameEvents, IWeaponInitConfigAccessor weaponInitConfigAccessor)
    {
        _unitsFactory = unitsFactory;
        _levelFactory = levelsFactory;
        _weaponFactory = weaponFactory;
        _input = input;
        _weaponInitConfigAccessor = weaponInitConfigAccessor;

        _gameEvents = gameEvents;
        gameEvents.OnGameLoaded += OnLevelLoaded;
        gameEvents.OnGameStart += OnGameStart;
        gameEvents.OnGameFinish += OnGameFinish;
        gameEvents.OnNextGame += OnNextGame;
        gameEvents.OnRestartGame += OnRestartGame;
    }

    private void OnRestartGame()
    {
        LoadGame();
    }

    private void OnNextGame()
    {
        LoadGame();
    }

    private void OnGameStart()
    {
        InitPlayer();
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

        InitEnemy();
    }

    private async void InitPlayer()
    {
        _player = await ConstructUnit(PLAYER_ID, _currentLevel.PlayerSpawnPoint);
        _player.OnDeath += OnPlayerDeath;

        var controllers = _player.Transform.GetComponentsInChildren<IInputInit>();
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
            ConstructUnit(enemy.Key, enemy.Value);
        }
    }

    private async Task<IUnit> ConstructUnit(string id, Vector3 pos)
    {
        IUnit unit = await _unitsFactory.CreateUnit<IUnit>(id, pos,
            Quaternion.identity, _currentLevel.Transform, true);

        string initWeaponId = await _weaponInitConfigAccessor.GetInitWeaponIdForUnit(id);

        if (!initWeaponId.IsEmpty())
        {
            string targetTag = id != PLAYER_ID ? PLAYER_ID : ENEMY_ID;

            IWeaponProcessor weaponProcessor = unit.Transform.GetComponent<IWeaponProcessor>();
            weaponProcessor?.SetWeapon(
                await _weaponFactory.CreateWeapon<IWeapon>(initWeaponId, unit.Transform.gameObject, targetTag));
        }

        return unit;
    }

    private void UnloadLevel()
    {
        _unitsFactory.ClearCached();
        _currentLevel.Dispose();
    }

    private void OnPlayerDeath()
    {
        _gameEvents.OnGameFinish?.Invoke(GameEvents.GameResult.Death);
    }

    private void OnGameFinish(GameEvents.GameResult result)
    {
        LoadGame();
    }
}