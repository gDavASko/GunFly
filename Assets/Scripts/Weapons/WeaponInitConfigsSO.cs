using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInitConfigsSO", menuName = "GunFly/WeaponInitSO", order = 2)]
public class WeaponInitConfigsSO: ScriptableObject
{
    [SerializeField] private WeaponInitConfig[] _configs;
    public WeaponInitConfig[] Configs => _configs;
}

[System.Serializable]
public class WeaponInitConfig : IWeaponInitConfig
{
    [SerializeField] private string _unitId = "unit";
    [SerializeField] private string _weaponId = "weapon";

    public string UnitId => _unitId;
    public string WeaponId => _weaponId;
}