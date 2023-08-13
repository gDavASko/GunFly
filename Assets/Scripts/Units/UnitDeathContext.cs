using UnityEngine;

public struct UnitDeathContext : IUnitDeathContext
{
    public string UnitId { get; private set; }
    public string UnitTag { get; private set; }
    public Vector3 Position { get; private set; }

    public UnitDeathContext(string unitId, string unitTag, Vector3 position)
    {
        UnitId = unitId;
        UnitTag = unitTag;
        Position = position;
    }
}