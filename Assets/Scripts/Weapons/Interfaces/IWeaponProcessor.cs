using System;

public interface IWeaponProcessor
{
    IWeapon CurWeapon { get; }
    void SetWeapon(IWeapon weapon);
    void UseWeapon();
}