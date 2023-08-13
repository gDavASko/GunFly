using System;
using UnityEngine;
using Zenject;

public interface IUnit: IDisposable
{
    Transform Transform { get; }
    System.Action OnDeath { get; set; }

    void Init(Vector3 pos, Quaternion rotation, Transform parent);
    void Death();
}