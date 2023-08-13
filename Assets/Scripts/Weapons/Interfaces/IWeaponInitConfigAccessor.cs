using System.Threading.Tasks;

public interface IWeaponInitConfigAccessor
{
    Task<string> GetInitWeaponIdForUnit(string unitId);
}