using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColdWeaponTrigger : MonoBehaviour, IWeaponTrigger
{
    [SerializeField] private string[] _targetTags = new string[] { "Enemy" };
    public bool CanUse { get; set; } = false;

    public Action<IDamagableTarget> OnTriggeredTarget { get; set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!CanUse || !_targetTags.Contains(col.tag))
            return;

        IDamagableTarget targetDamage = col.GetComponent<IDamagableTarget>();
        if(targetDamage != null)
            OnTriggeredTarget?.Invoke(targetDamage);
    }

    public void Dispose()
    {
        Destroy(this.gameObject);
    }
}