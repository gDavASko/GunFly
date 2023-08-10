using UnityEngine;


public class WeaponProcessorBase : MonoBehaviour, IWeaponProcessor
{
    [SerializeField] private Transform _weaponSlot = null;

    public IWeapon CurWeapon { get; protected set; } = null;

    public void SetWeapon(IWeapon weapon)
    {
        CurWeapon = weapon;
        CurWeapon.transform.SetParent(_weaponSlot);
        CurWeapon.transform.localPosition = Vector3.zero;
    }

    public virtual void UseWeapon()
    {
        if (CurWeapon != null)
            CurWeapon.Attack();
    }

    public void OnDestroy()
    {
        if(CurWeapon != null)
            CurWeapon.Dispose();
    }
}