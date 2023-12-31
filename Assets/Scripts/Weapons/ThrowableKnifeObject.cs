using System;
using System.Collections;
using System.Security.Cryptography;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class ThrowableKnifeObject : WeaponDamageTriggerBase, IThrowableWeaponObject
{
    [SerializeField] private float _flyDeltaTime = 0.005f;
    [SerializeField] private float _maxFlyDistance = 10f;

    private Coroutine _throwPlay = null;
    private bool _continueFly = true;
    private WaitForFixedUpdate _flyWaiter = new WaitForFixedUpdate();

    public Transform Transform => transform;

    protected override void Awake()
    {
        OnTriggeredTarget += OnTriggered;
    }

    public void Throw()
    {
        if(_throwPlay != null)
            StopCoroutine(_throwPlay);

        _continueFly = true;
        _throwPlay = StartCoroutine(ThrowSelf());
    }

    private IEnumerator ThrowSelf()
    {
        Vector3 iniPos = transform.position;
        bool isNotPlayer = !_owner.CompareTag(GameController.PLAYER_ID);
        Vector3 targetPos = iniPos + (isNotPlayer ? -1 : 1) * transform.right  * 10f;
        float angle = (isNotPlayer ? 1 : -1) * 6f;

        for (float i = 0; i <= 1 && _continueFly; i += _flyDeltaTime)
        {
            transform.position = Vector3.Lerp(iniPos, targetPos, i);
            transform.Rotate(new Vector3(0f, 0f, angle));
            yield return _flyWaiter;
        }

        Dispose();
        Destroy(this.gameObject);
    }

    private void OnTriggered(IDamagableTarget _)
    {
        _continueFly = false;
    }
}