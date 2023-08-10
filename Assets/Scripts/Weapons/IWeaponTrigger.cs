using System;

public interface IWeaponTrigger: IDisposable
{
    bool CanUse { get; set; }
    System.Action<IDamagableTarget> OnTriggeredTarget { get; set; }
}