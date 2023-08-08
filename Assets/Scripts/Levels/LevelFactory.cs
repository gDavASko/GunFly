using System.Threading.Tasks;

public class LevelFactory
{
    private IAssetGetter _assetGetter = null;
    private ILevel curLevel = null;

    public LevelFactory(IAssetGetter assetGetter)
    {
        _assetGetter = assetGetter;
    }

    public async Task<T> CreateLevel<T>(string id) where T : class, ILevel
    {
        if(curLevel != null)
            _assetGetter.UnloadResource();

        var level = await _assetGetter.LoadResource<T>(id);
        return level as T;
    }
}