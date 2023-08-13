using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public interface IWeaponDamageTrigger: IDisposable
{
    Transform Transform { get; }
    bool IsActivated { get; set; }
    void Init(IDamageConfig damageConfig, GameObject owner, string targetTag);
    System.Action<IDamagableTarget> OnTriggeredTarget { get; set; }
}