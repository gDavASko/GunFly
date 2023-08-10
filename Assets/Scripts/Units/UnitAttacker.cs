using System;
using UnityEngine;

[RequireComponent(typeof(IWeaponProcessor))]
public class UnitAttacker : Unit, IAttackController
{
    private IWeaponProcessor _weaponProcessor = null;

    private void Awake()
    {
        _weaponProcessor = GetComponent<IWeaponProcessor>();
    }

    public void Init(IInput input)
    {
        input.OnAction += OnAttack;
    }

    private void OnAttack(ActionType action)
    {
        if (action == ActionType.Attack)
            Attack();
    }

    public void Attack()
    {
        if (_weaponProcessor != null)
            _weaponProcessor.UseWeapon();
    }
}