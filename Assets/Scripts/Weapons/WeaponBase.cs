using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [SerializeField] protected string _damageId = "damage";

    protected IDamageConfig _damageConfig = null;
    protected GameObject _owner = null;
    protected string _targetTag = string.Empty;

    public string DamageId => _damageId;

    public abstract void Attack();

    public virtual void Init(GameObject owner, IDamageConfig damageConfig, string targetTag)
    {
        _owner = owner;
        _damageConfig = damageConfig;
        _targetTag = targetTag;
    }

    private void OnAttack(ActionType action)
    {
        if (action == ActionType.Attack)
            Attack();
    }

    public abstract void Dispose();
}