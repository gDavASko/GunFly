
using System.Threading.Tasks;
using UnityEngine;

public interface IAssetGetter : IGettableAsset, IUnloadableAsset
{
    public GameObject CachedObject { get; }
}

public interface IUnloadableAsset
{
    void UnloadResource();
}

public interface IGettableAsset
{
    Task<T> LoadResource<T>(string assetId);
}