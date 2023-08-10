using System.Threading.Tasks;

public class WeaponFactory : IWeaponFactory
{
    private IGettableAsset _assetGetter = null;

    public WeaponFactory(IGettableAsset assetGetter)
    {
        _assetGetter = assetGetter;
    }

    public async Task<T> CreateWeapon<T>(string id) where T : class, IWeapon
    {
        T weapon = await _assetGetter.LoadResource<T>(id);
        return weapon as T;
    }
}