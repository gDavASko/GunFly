
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WeaponSword : WeaponBase
{
    [SerializeField] private Transform _rotator = null;

    private IWeaponTrigger _wepTrigger = null;

    private void Awake()
    {
        _wepTrigger = GetComponentInChildren<IWeaponTrigger>();
        _wepTrigger.OnTriggeredTarget += ProccesTrigger;
    }

    private void ProccesTrigger(IDamagableTarget target)
    {
        target.DealDamage(_damage);
    }

    public override void Attack()
    {
        ProcessAttack().Forget();
    }

    private async UniTaskVoid ProcessAttack()
    {
        if (_rotator == null)
            return;

        Vector3 rotationTarget = _rotator.eulerAngles;
        rotationTarget.z -= 359;
        Vector3 rotationInit = _rotator.eulerAngles;

        _wepTrigger.CanUse = true;

        for (float i = 0; i < 1; i += 0.01f)
        {
            _rotator.rotation = Quaternion.Euler(Vector3.Lerp(rotationInit, rotationTarget, i));
            await UniTask.WaitForEndOfFrame();
        }

        _wepTrigger.CanUse = false;
    }

    public override void Dispose()
    {
        _wepTrigger.Dispose();
        base.Dispose();
    }
}