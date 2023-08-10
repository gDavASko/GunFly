using System;
using UnityEngine;

[RequireComponent(typeof(IHitPointSystem))]
public class DamageProcessor : MonoBehaviour, IDamagableTarget
{

    private IHitPointSystem _hpSystem = null;

    private void Awake()
    {
        _hpSystem = GetComponent<IHitPointSystem>();
    }

    public void DealDamage(float value)
    {
        _hpSystem.ReduceHitPoints(value);
    }
}