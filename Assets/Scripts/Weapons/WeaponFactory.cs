using System.Threading.Tasks;
using UnityEngine;

public class WeaponFactory : IWeaponFactory
{
    private IGettableAsset _assetGetter = null;
    private IDamageConfigAccessor _damageConfigAccessor = null;
    public WeaponFactory(IGettableAsset assetGetter, IDamageConfigAccessor damageConfigAccessor)
    {
        _assetGetter = assetGetter;
        _damageConfigAccessor = damageConfigAccessor;
    }

    public async Task<T> CreateWeapon<T>(string id, GameObject owner, string targetTag) where T : class, IWeapon
    {
        T weapon = await _assetGetter.LoadResource<T>(id);
        weapon.Init(owner, await _damageConfigAccessor.GetConfig(weapon.DamageId), targetTag);
        return weapon as T;
    }
}