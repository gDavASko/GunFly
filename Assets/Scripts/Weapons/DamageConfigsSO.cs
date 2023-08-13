using UnityEngine;

[CreateAssetMenu(fileName = "DamageConfigsSO", menuName = "GunFly/DamageConfigsSO", order = 1)]
public class DamageConfigsSO: ScriptableObject
{
    [SerializeField] private DamageConfig[] _damageConfigs = null;
    public DamageConfig[] Configs => _damageConfigs;
}

[System.Serializable]
public class DamageConfig: IDamageConfig
{
    [SerializeField] private string _id = "damage";
    [SerializeField] private float _damage = 0f;

    public string Id => _id;
    public float Damage => _damage;
}