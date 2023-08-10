using System;
using UnityEngine;using Zenject;

public class MobileInput : IInput, ITickable
{
    public Action<ActionType> OnAction { get; set; }

    private bool isTouchBegan = false;

    public void Tick()
    {
        if (Input.touchCount > 0)
        {
            if (!isTouchBegan)
            {
                isTouchBegan = true;
                OnAction?.Invoke(ActionType.Jump);
            }
        }
        else if (isTouchBegan)
        {
            OnAction?.Invoke(ActionType.Attack);
            isTouchBegan = false;
        }
    }



}