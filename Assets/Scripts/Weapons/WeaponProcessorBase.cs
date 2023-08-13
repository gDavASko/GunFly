using System.Collections;
using System.ComponentModel.Design.Serialization;
using Unity.Mathematics;
using UnityEngine;


public class WeaponProcessorBase : MonoBehaviour, IWeaponProcessor
{
    [SerializeField] private Transform _weaponSlot = null;
    [SerializeField] private bool _inverseX = false;

    public IWeapon CurWeapon { get; protected set; } = null;

    public void SetWeapon(IWeapon weapon)
    {
        CurWeapon = weapon;
        CurWeapon.transform.SetParent(_weaponSlot);
        CurWeapon.transform.localPosition = Vector3.zero;
        CurWeapon.transform.rotation = quaternion.identity;

        if(_inverseX)
            CurWeapon.transform.localScale = new Vector3(-CurWeapon.transform.localScale.x,
                                                    CurWeapon.transform.localScale.y,
                                                    CurWeapon.transform.localScale.z);
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