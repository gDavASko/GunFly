using System;
using UnityEngine;

public interface IWeapon: IDisposable
{
    string DamageId { get; }
    Transform transform { get; }
    void Init(GameObject owner, IDamageConfig damageConfig, string targetTag);
    void Attack();
}