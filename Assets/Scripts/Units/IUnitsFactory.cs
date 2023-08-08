using System.Threading.Tasks;
using UnityEngine;

public interface IUnitsFactory
{
    public Task<T> CreateUnit<T>(string id, Vector3 position, Quaternion rotation, Transform parent, bool cacheIt)
        where T : class, IUnit;

    public void ClearCached();
}