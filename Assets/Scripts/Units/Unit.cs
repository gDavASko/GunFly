using System;
using Events;
using UnityEngine;

[RequireComponent(typeof(IHitPointSystem))]
public abstract class Unit : MonoBehaviour, IUnit
{
    [SerializeField] private string _id = default;
    [SerializeField] private bool disableOnDeath = true;

    protected IHitPointSystem _hpSystem = null;
    protected UnitEvents _unitEvents = null;

    public Transform Transform => transform;
    public Action OnDeath { get; set; }

    public virtual void Init(Vector3 pos, Quaternion rotation, Transform parent, UnitEvents unitEvents)
    {
        transform.position = pos;
        transform.rotation = rotation;
        transform.SetParent(parent);

        _unitEvents = unitEvents;

        _hpSystem = GetComponent<IHitPointSystem>();
        _hpSystem.OnDeath += Death;
    }

    public virtual void Death()
    {
        OnDeath?.Invoke();
        _unitEvents.OnUnitDeath?.Invoke( new UnitDeathContext(_id, gameObject.tag, transform.position));

        if(disableOnDeath)
            gameObject.SetActive(false);
    }

    public virtual void Dispose()
    {
        Destroy(this.gameObject);
    }
}