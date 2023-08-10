using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon: IDisposable
{
    Transform transform { get; }
    void Init(IInput input, GameObject owner);
    void Attack();
}