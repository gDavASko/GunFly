
using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WeaponSword : WeaponBase
{
    [SerializeField] private Transform _rotator = null;

    private IWeaponDamageTrigger _wepDamageTrigger = null;
    private WaitForEndOfFrame _waiter = null;
    private Coroutine _attackPlay = null;
    private Vector3 _initWeaponPos = default;

    public override void Init(GameObject owner, IDamageConfig damageConfig, string targetTag)
    {
        base.Init(owner, damageConfig, targetTag);

        _wepDamageTrigger = GetComponentInChildren<IWeaponDamageTrigger>();
        _wepDamageTrigger.Init(damageConfig, owner, targetTag);
        _waiter = new WaitForEndOfFrame();
        _initWeaponPos = _rotator.eulerAngles;
    }

    public override void Attack()
    {
        if (!gameObject.activeSelf || !gameObject.activeInHierarchy)
            return;

        if (_attackPlay != null)
            StopCoroutine(_attackPlay);

        _attackPlay = StartCoroutine(ProcessAttack());
    }

    private IEnumerator ProcessAttack()
    {
        if (_rotator == null)
            yield break;

        Vector3 rotationTarget = _initWeaponPos;
        rotationTarget.z -= 359;
        Vector3 rotationInit = _initWeaponPos;

        _wepDamageTrigger.IsActivated = true;

        for (float i = 0; i < 1; i += 0.01f)
        {
            _rotator.rotation = Quaternion.Euler(Vector3.Lerp(rotationInit, rotationTarget, i));
            yield return _waiter;
        }

        _wepDamageTrigger.IsActivated = false;
    }

    public override void Dispose()
    {
        _wepDamageTrigger.Dispose();
    }

    private void OnDestroy()
    {
        if (_attackPlay != null)
            StopCoroutine(_attackPlay);
    }
}