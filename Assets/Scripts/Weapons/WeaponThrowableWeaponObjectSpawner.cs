using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.Mathematics;
using UnityEngine;

public class WeaponThrowableWeaponObjectSpawner : MonoBehaviour, IThrowableWeaponSpawner
{
    [SerializeField] private string _throwableObjectId = "Weapon_throwable_knife_object";
    [SerializeField] private int _spawnCoolDownMiliseconds = 500;

    private GameObject _spawnObject = null;
    private List<IThrowableWeaponObject> _spawnedObjects = new List<IThrowableWeaponObject>();

    protected string _targetTag = "Enemy";
    protected bool _isActivated = false;
    protected IDamageConfig _damageConfig = null;
    protected GameObject _owner = null;
    private DateTime _lastSpawnTime = default;

    public Transform Transform => transform;

    public bool IsActivated { get; set; }

    private void Awake()
    {
        var getter = new AddressableAsyncAssetGetter<GameObject>();
        getter.LoadResource(_throwableObjectId, OnObjectLoaded);
    }

    private void OnObjectLoaded(GameObject obj)
    {
        _spawnObject = obj;
    }

    public virtual void Init(IDamageConfig damageConfig, GameObject owner, string targetTag)
    {
        _damageConfig = damageConfig;
        _owner = owner;
        _targetTag = targetTag;

        ResetSpawned();
    }

    public Action<IDamagableTarget> OnTriggeredTarget { get; set; }

    public void ThrowObject()
    {
        if ((DateTime.Now - _lastSpawnTime).TotalMilliseconds < _spawnCoolDownMiliseconds)
            return;

        var _throwObj = Instantiate(_spawnObject, transform.position, quaternion.identity)
            .GetComponent<IThrowableWeaponObject>();
        _throwObj.Init(_damageConfig, _owner, _targetTag);
        _throwObj.IsActivated = true;
        _throwObj.Throw();
        _spawnedObjects.Add(_throwObj);

        _lastSpawnTime = DateTime.Now;
    }

    public void Dispose()
    {
        ResetSpawned();
        Destroy(this.gameObject);
    }

    private void ResetSpawned()
    {
        foreach (var obj in _spawnedObjects)
        {
            if (obj != null)
                obj.Dispose();
        }

        _spawnedObjects.Clear();
    }
}