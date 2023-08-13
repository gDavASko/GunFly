using System;
using UnityEngine;

public interface IThrowableWeaponObject: IWeaponDamageTrigger
{
    void Throw();
}