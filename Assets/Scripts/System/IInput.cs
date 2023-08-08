using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    System.Action OnJump { get; set; }
}