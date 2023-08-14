using System.Collections.Generic;
using System.Threading.Tasks;
using Events;
using UnityEngine;

public class UnitsFactory : IUnitsFactory
{
    private IGettableAsset _assetGetter = null;
    private IWeaponFactory _weaponFactory = null;
    private UnitEvents _unitEvents = null;
    private List<IUnit> _cachedUnits = new List<IUnit>();


    public UnitsFactory(IGettableAsset assetGetter, IWeaponFactory _weaponFactory, UnitEvents unitEvents)
    {
        _assetGetter = assetGetter;
        _unitEvents = unitEvents;
    }

    public async Task<T> CreateUnit<T>(string id, Vector3 position, Quaternion rotation, Transform parent = null, bool cacheIt = true) where T : class, IUnit
    {
        T unit = await _assetGetter.LoadResource<T>(id);

        unit.Init(position, rotation, parent, _unitEvents);

        _unitEvents.OnUnitCreated?.Invoke(unit);

        if(cacheIt)
            _cachedUnits.Add(unit);

        return unit as T;
    }

    public void ClearCached()
    {
        foreach(var unit in _cachedUnits)
            unit.Dispose();
        _cachedUnits.Clear();
    }
}