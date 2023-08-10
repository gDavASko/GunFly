using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IAttackController))]
public class EnemyAttackBehaviourBase : EnemyBehaviourBase
{
    [SerializeField] private float _attackDelay = 3.5f;

    private bool _attacked = false;

    private IAttackController _attackController = null;

    protected override void Awake()
    {
        base.Awake();

        _attackController = GetComponent<IAttackController>();
        _attackController.Init(this);
    }

    public override void Update()
    {
        base.Update();

        if (!_attacked && _timeCounter >= _attackDelay)
        {
            OnAction?.Invoke(ActionType.Attack);
            _attacked = true;
        }
    }

    protected override void ResetCycle()
    {
        base.ResetCycle();
        _attacked = false;
    }
}