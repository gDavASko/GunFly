using UnityEngine;

public interface IUnitDeathContext
{
    string UnitId { get; }
    string UnitTag { get; }
    Vector3 Position { get; }
}