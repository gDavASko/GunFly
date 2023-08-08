using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public Vector3 Position
    {
        get => transform.position;
    }

    public void Dispose()
    {

    }


}