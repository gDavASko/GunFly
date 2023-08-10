using System;
using UnityEngine;

public class HitPointSystem : MonoBehaviour, IHitPointSystem
{
    [SerializeField] private float _maxHitpoints = 10f;

    public System.Action OnDeath { get; set; }

    private void Awake()
    {
        HitPoints = _maxHitpoints;
    }

    public float HitPoints { get; private set; } = 0f;

    public void ReduceHitPoints(float value)
    {
        if (HitPoints <= 0)
            return;

        HitPoints -= value;
        Debug.LogError($"Get Damage {name}");

        if(HitPoints <= 0)
            OnDeath?.Invoke();
    }

    public void Kill()
    {
        ReduceHitPoints(HitPoints + 1);
        Debug.LogError($"Kill {name}");
    }
}