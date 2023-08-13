using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IInput
{
    System.Action<ActionType> OnAction { get; set; }
}

public enum ActionType
{
    None = 0,
    Jump = 1,
    Attack = 2,
}