
using System;
using Events;
using Saves;
using Zenject;

public class StatisticProcessor : IStatisticProcessor, IDisposable
{
    private IStorableParams _storableParams = null;
    private IStatistic<int> _intStats = null;
    private IStatistic<string> _stringStats = null;

    private UnitEvents _unitEvents = null;
    private GameEvents _gameEvents = null;
    private ItemEvents _itemEvents = null;

    [Inject]
    public StatisticProcessor(IStorableParams storableParams, IStatistic<int> intStats, UnitEvents unitEvents, GameEvents gameEvents, ItemEvents itemEvents)
    {
        _storableParams = storableParams;
        _intStats = intStats;
        _stringStats = new StorableStatistic<string>(storableParams);

        _unitEvents = unitEvents;
        _unitEvents.OnUnitDeath += OnUnitDeath;
        _unitEvents.OnUnitWeaponChange += OnWeaponChange;

        _gameEvents = gameEvents;
        _gameEvents.OnGameFinish += OnGameFinish;
        _gameEvents.OnGameStart += OnGameStart;

        _itemEvents = itemEvents;
        _itemEvents.OnItemPlayerCollision += OnItemGet;

        _intStats.SetValue(SaveKey.Scores, 0);
    }

    //ToDo: Move to GameItemsController
    private void OnItemGet(IGameItem item)
    {
        _intStats.SetValue(SaveKey.Scores, _intStats.GetValue(SaveKey.Scores, 0) + (int)item.Count);
    }

    private void OnGameStart()
    {
        _intStats.SetValue(SaveKey.Scores, 0);
    }

    public void Load()
    {
        _storableParams.Load();
    }

    public void Save()
    {
        _storableParams.Save();
    }

    private void OnUnitDeath(IUnitDeathContext context)
    {
        if (context.UnitTag == GameController.PLAYER_ID)
        {
            _intStats.SetValue(SaveKey.Deaths, _intStats.GetValue(SaveKey.Deaths, 0) + 1);
        }
        else
        {
            _intStats.SetValue(SaveKey.Scores, _intStats.GetValue(SaveKey.Scores, 0) + 1);
        }
    }

    private void OnGameFinish(GameEvents.GameResult result)
    {
        _intStats.SetValue(SaveKey.Sessions, _intStats.GetValue(SaveKey.Sessions, 0) + 1);

        if(result == GameEvents.GameResult.Victory)
            _intStats.SetValue(SaveKey.Victories, _intStats.GetValue(SaveKey.Victories, 0) + 1);

        var score = _intStats.GetValue(SaveKey.Scores, 0);
        if(score > _intStats.GetValue(SaveKey.BestScores, 0))
            _intStats.SetValue(SaveKey.BestScores, score);
    }

    private void OnWeaponChange(string unitId, string weaponId)
    {
        if(unitId == GameController.PLAYER_ID)
            _stringStats.SetValue(SaveKey.LastWeapon, weaponId);
    }


    public void Dispose()
    {
        _unitEvents.OnUnitDeath -= OnUnitDeath;
        _unitEvents.OnUnitWeaponChange -= OnWeaponChange;

        _gameEvents.OnGameFinish -= OnGameFinish;
        _gameEvents.OnGameStart -= OnGameStart;

        _itemEvents.OnItemPlayerCollision -= OnItemGet;
    }
}