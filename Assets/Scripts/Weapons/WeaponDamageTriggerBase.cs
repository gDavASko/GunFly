using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponDamageTriggerBase : MonoBehaviour
{
    [SerializeField] private bool _checkStayState = false;

    protected string _targetTag = "Enemy";
    protected bool _isActivated = false;
    protected IDamageConfig _damageConfig = null;
    protected GameObject _owner = null;

    private List<Collider2D> _stayColllision = null;

    public Action<IDamagableTarget> OnTriggeredTarget { get; set; }

    public bool IsActivated
    {
        get
        {
            return _isActivated;
        }
        set
        {
            _isActivated = value;

            if(_checkStayState)
                CheckDamageOnCollisionStay();
        }
    }

    protected virtual void Awake()
    {
        if (_checkStayState)
            _stayColllision = new List<Collider2D>();
    }

    public virtual void Init(IDamageConfig damageConfig, GameObject owner, string targetTag)
    {
        _damageConfig = damageConfig;
        _owner = owner;
        _targetTag = targetTag;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _owner || !col.CompareTag(_targetTag))
            return;

        if (IsActivated)
        {
            ProcessDamage(col);
        }
        else if(_checkStayState)
        {
            _stayColllision.Add(col);
        }
    }

    private void ProcessDamage(Collider2D col)
    {
        IDamagableTarget targetDamage = col.GetComponent<IDamagableTarget>();
        if (targetDamage != null)
        {
            targetDamage.DealDamage(_damageConfig.Damage);
            OnTriggeredTarget?.Invoke(targetDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!_checkStayState || col.gameObject == _owner || !col.CompareTag(_targetTag))
            return;

        _stayColllision.Remove(col);
    }

    private void CheckDamageOnCollisionStay()
    {
        if (_stayColllision.Count > 0)
        {
            foreach (var target in _stayColllision)
            {
                ProcessDamage(target);
            }
        }
    }


    public void Dispose()
    {
        OnTriggeredTarget = null;
    }
}