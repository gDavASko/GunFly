using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class UnitsFactory
{
    private IGettableAsset _assetGetter = null;
    public UnitsFactory(IAssetGetter assetGetter)
    {
        _assetGetter = assetGetter;
    }

    public async Task<T> CreateUnit<T>(string id, Vector3 position, quaternion rotation, Transform parent = null) where T : class, IUnit
    {
        var unit = await _assetGetter.LoadResource<T>(id);
        return unit as T;
    }
}