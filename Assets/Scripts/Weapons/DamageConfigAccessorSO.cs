using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class DamageConfigAccessorSO : IDamageConfigAccessor
{
    public const string ASSET_DAMAGE_CONFIG_ID = "DamageConfigsSO";

    private DamageConfigsSO _damageConfigs = null;

    private Dictionary<string, IDamageConfig> _configs = null;
    private Dictionary<string, IDamageConfig> Configs
    {
        get
        {
            if (_configs == null)
            {
                _configs = new Dictionary<string, IDamageConfig>();
                foreach (var config in _damageConfigs.Configs)
                {
                    _configs[config.Id] = config;
                }
            }

            return _configs;
        }
    }

    public DamageConfigAccessorSO()
    {
        var loader = new AddressableAsyncAssetGetter<ScriptableObject>();
        loader.LoadResource(ASSET_DAMAGE_CONFIG_ID, OnLoadedConfig);
    }

    private void OnLoadedConfig(ScriptableObject config)
    {
        _damageConfigs = config as DamageConfigsSO;
    }

    public async Task<IDamageConfig> GetConfig(string id)
    {
        if (Configs.TryGetValue(id, out IDamageConfig config))
        {
            return config;
        }
        else
        {
            Debug.LogError($"[{nameof(DamageConfigAccessorSO)}] Try to get non contains config [{id}]");
            return null;
        }
    }
}