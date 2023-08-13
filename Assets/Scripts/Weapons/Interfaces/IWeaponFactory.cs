using System.Threading.Tasks;
using UnityEngine;

public interface IWeaponFactory
{
    public Task<T> CreateWeapon<T>(string id, GameObject owner, string targetTag)
        where T : class, IWeapon;
}