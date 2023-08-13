using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponInitConfigsAccessorSO : IWeaponInitConfigAccessor
{
    public const string ASSET_WEAPON_INIT_CONFIG_ID = "WeaponInitConfigsSO";

    private WeaponInitConfigsSO _configs = null;
    private Dictionary<string, string> _configsDict = null;

    private Dictionary<string, string> ConfigsDict
    {
        get
        {
            if (_configsDict == null)
            {
                _configsDict = new Dictionary<string, string>();
                foreach (var config in _configs.Configs)
                {
                    _configsDict[config.UnitId] = config.WeaponId;
                }
            }
            return _configsDict;
        }
    }

    public WeaponInitConfigsAccessorSO()
    {
        var loader = new AddressableAsyncAssetGetter<ScriptableObject>();
        loader.LoadResource(ASSET_WEAPON_INIT_CONFIG_ID, OnLoadedConfig);
    }

    private void OnLoadedConfig(ScriptableObject config)
    {
        _configs = config as WeaponInitConfigsSO;
    }


    public async Task<string> GetInitWeaponIdForUnit(string unitId)
    {
        if (ConfigsDict.TryGetValue(unitId, out string weaponId))
            return weaponId;

        return string.Empty;
    }
}