using UnityEngine;

[RequireComponent(typeof(IThrowableWeaponSpawner))]
public class WeaponThrowKnife : WeaponBase
{
    private IThrowableWeaponSpawner _objectSpawner = null;

    private void Awake()
    {
        _objectSpawner = GetComponent<IThrowableWeaponSpawner>();
    }

    public override void Init(GameObject owner, IDamageConfig damageConfig, string targetTag)
    {
        base.Init(owner, damageConfig, targetTag);
        _objectSpawner.Init(damageConfig, owner, targetTag);
    }

    public override void Attack()
    {
        base.Attack();

        if (!gameObject.activeSelf || !gameObject.activeInHierarchy)
            return;

        _objectSpawner.ThrowObject();
    }

    public override void Dispose()
    {
        _objectSpawner.Dispose();
        gameObject.SetActive(false);
    }
}