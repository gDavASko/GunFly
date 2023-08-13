using System;
using UnityEngine;

[RequireComponent(typeof(IHitPointSystem))]
public abstract class Unit : MonoBehaviour, IUnit
{
    [SerializeField] private bool disableOnDeath = true;
    protected IHitPointSystem _hpSystem = null;

    public Transform Transform => transform;
    public Action OnDeath { get; set; }

    public virtual void Init(Vector3 pos, Quaternion rotation, Transform parent)
    {
        transform.position = pos;
        transform.rotation = rotation;
        transform.SetParent(parent);

        _hpSystem = GetComponent<IHitPointSystem>();
        _hpSystem.OnDeath += Death;
    }

    public virtual void Death()
    {
        OnDeath?.Invoke();

        if(disableOnDeath)
            gameObject.SetActive(false);
    }

    public virtual void Dispose()
    {
        Destroy(this.gameObject);
    }
}