using System.Collections.Generic;
using System.Threading.Tasks;
using Events;
using ModestTree;
using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour, ILevelController
{
    [SerializeField] private string[] _levelsOrderedIds = null;
    [SerializeField] private int _cycledFrom = 3;

    private IUnitsFactory _unitsFactory = null;
    private ILevelFactory _levelFactory = null;
    private IWeaponFactory _weaponFactory = null;
    private IGameItemsFactory _itemsFactory = null;
    private IWeaponInitConfigAccessor _weaponInitConfigAccessor = null;
    private IStatistic<int> _intStat = null;

    private GameEvents _gameEvents = null;
    private ILevel _currentLevel = null;

    [Inject]
    private void Construct(IUnitsFactory unitsFactory, IWeaponFactory weaponFactory, ILevelFactory levelFactory,
        IWeaponInitConfigAccessor weaponInitConfigAccessor, IGameItemsFactory itemsFactory,
        IStatistic<int> intStat, GameEvents gameEvents)
    {
        _unitsFactory = unitsFactory;
        _levelFactory = levelFactory;
        _weaponFactory = weaponFactory;
        _itemsFactory = itemsFactory;
        _weaponInitConfigAccessor = weaponInitConfigAccessor;
        _intStat = intStat;

        _gameEvents = gameEvents;
        gameEvents.OnNextGame += OnNextGame;
        gameEvents.OnRestartGame += OnRestartGame;

        LoadCurrent();
    }

    public ILevel CurrentLevel => _currentLevel;

    public void LoadNext()
    {
        int lvlNumber = _intStat.GetValue(SaveKey.LevelNumber, 0);
        lvlNumber++;
        LoadLevelByNumber(lvlNumber);
        _intStat.SetValue(SaveKey.LevelNumber, lvlNumber);
    }

    public async void LoadCurrent()
    {
       LoadLevelByNumber(_intStat.GetValue(SaveKey.LevelNumber, 0));
    }

    private async void LoadLevelByNumber(int number)
    {
        if (_currentLevel != null)
            UnloadLevel();

        _currentLevel =
            await _levelFactory.CreateLevel<ILevel>(GetLevelId(number));
        _currentLevel.Init(_gameEvents);

        InitEnemy();
        InitItems();
    }

    private string GetLevelId(int levelNumber)
    {
        int index = levelNumber % _levelsOrderedIds.Length;

        return _levelsOrderedIds[index];
    }

    private void InitItems()
    {
        foreach (KeyValuePair<string, Vector3> enemy in _currentLevel.ItemsSpawnPoints)
        {
            ConstructItem(enemy.Key, enemy.Value);
        }
    }

    private async Task<IGameItem> ConstructItem(string id, Vector3 pos)
    {
        IGameItem item = await _itemsFactory.CreateGameItem<IGameItem>(id, pos,true);
        return item;
    }

    private void InitEnemy()
    {
        foreach (KeyValuePair<string, Vector3> enemy in _currentLevel.EnemySpawnPoints)
        {
            ConstructUnit(enemy.Key, enemy.Value);
        }
    }

    public async Task<IUnit> ConstructUnit(string id, Vector3 pos)
    {
        IUnit unit = await _unitsFactory.CreateUnit<IUnit>(id, pos,
            Quaternion.identity, _currentLevel.Transform, true);

        string initWeaponId = await _weaponInitConfigAccessor.GetInitWeaponIdForUnit(id);

        if (!initWeaponId.IsEmpty())
        {
            string targetTag = id != GameController.PLAYER_ID ? GameController.PLAYER_ID : GameController.ENEMY_ID;

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

    private void OnRestartGame()
    {
        LoadCurrent();
    }

    private void OnNextGame()
    {
        LoadNext();
    }
}