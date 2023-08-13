
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

    [Inject]
    public StatisticProcessor(IStorableParams storableParams, UnitEvents unitEvents, GameEvents gameEvents)
    {
        _storableParams = storableParams;
        _intStats = new StorableStatistic<int>(storableParams);
        _stringStats = new StorableStatistic<string>(storableParams);

        _unitEvents = unitEvents;
        _unitEvents.OnUnitDeath += OnUnitDeath;
        _unitEvents.OnUnitWeaponChange += OnWeaponChange;

        _gameEvents = gameEvents;
        _gameEvents.OnGameFinish += OnGameFinish;
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
        if (context.UnitTag == "Player")
        {
            _intStats.SetValue(SaveKey.Deaths, _intStats.GetValue(SaveKey.Deaths, 0) + 1);
        }
        else
        {
            _intStats.SetValue(SaveKey.Credits, _intStats.GetValue(SaveKey.Credits, 0) + 1);
        }
    }

    private void OnGameFinish(GameEvents.GameResult result)
    {
        _intStats.SetValue(SaveKey.Sessions, _intStats.GetValue(SaveKey.Sessions, 0) + 1);

        if(result == GameEvents.GameResult.Victory)
            _intStats.SetValue(SaveKey.Victories, _intStats.GetValue(SaveKey.Victories, 0) + 1);
    }

    private void OnWeaponChange(string unitId, string weaponId)
    {
        if(unitId == "Player")
            _stringStats.SetValue(SaveKey.LastWeapon, weaponId);
    }


    public void Dispose()
    {
        _unitEvents.OnUnitDeath -= OnUnitDeath;
        _gameEvents.OnGameFinish -= OnGameFinish;
    }
}