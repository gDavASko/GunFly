

using System.Threading.Tasks;
using UnityEngine;

public interface IWeaponFactory
{
    public Task<T> CreateWeapon<T>(string id)
        where T : class, IWeapon;
}