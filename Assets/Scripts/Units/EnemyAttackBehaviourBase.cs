using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(IAttackController))]
public class EnemyAttackBehaviourBase : EnemyBehaviourBase
{
    [SerializeField] private float _attackDelay = 3.5f;

    private bool _attacked = false;
    private bool _canAttack = false;

    private IAttackController _attackController = null;

    protected override void Awake()
    {
        base.Awake();

        _attackController = GetComponent<IAttackController>();
        _attackController.Init(this);

        StartCoroutine(AttackRandomDelay());
    }

    private IEnumerator AttackRandomDelay()
    {
        yield return new WaitForSeconds(Random.Range(0f, 2f));
        _canAttack = true;
    }

    public override void Update()
    {
        base.Update();

        if (_canAttack && !_attacked && _timeCounter >= _attackDelay)
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