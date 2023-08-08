using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer: IUnit
{
    Vector3 Position { get; }
}