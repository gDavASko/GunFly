using System;
using UnityEngine;
using Zenject;

public interface IUnit: IDisposable
{
    void Init(Vector3 pos, Quaternion rotation, Transform parent);
}