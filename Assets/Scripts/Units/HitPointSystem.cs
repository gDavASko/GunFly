using System;
using UnityEngine;

public class HitPointSystem : MonoBehaviour, IHitPointSystem
{
    [SerializeField] private float _maxHitpoints = 10f;


    //ToDo: Move audio damage sound to audio event system
    private IAudioPlayer _soundDamage = null;

    public System.Action OnDeath { get; set; }

    private void Awake()
    {
        HitPoints = _maxHitpoints;
        _soundDamage = GetComponent<AudioPlayer>();
    }

    public float HitPoints { get; private set; } = 0f;
    public float HitPointPercent => HitPoints / _maxHitpoints;

    public Action<float> OnHPChanged { get; set; }

    public void ReduceHitPoints(float value)
    {
        if (HitPoints <= 0)
            return;

        HitPoints -= value;

        _soundDamage?.PlaySound();

        OnHPChanged?.Invoke(value);

        if(HitPoints <= 0)
            OnDeath?.Invoke();
    }

    public void Kill()
    {
        ReduceHitPoints(HitPoints + 1);
    }
}