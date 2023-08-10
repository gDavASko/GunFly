using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [SerializeField] protected float _damage = 0;

    protected GameObject _owner = null;

    public abstract void Attack();

    public virtual void Init(IInput input, GameObject owner)
    {
        input.OnAction += OnAttack;
        _owner = owner;
    }

    private void OnAttack(ActionType action)
    {
        if (action == ActionType.Attack)
            Attack();
    }

    public virtual void Dispose()
    {
        Destroy(this.gameObject);
    }
}