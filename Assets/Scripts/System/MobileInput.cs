using System;
using UnityEngine;

public class MobileInput : MonoBehaviour, IInput
{
    public Action OnJump { get; set; }

    private bool isTouchBegan = false;
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (!isTouchBegan)
            {
                isTouchBegan = true;
                OnJump?.Invoke();
            }
        }
        else if (isTouchBegan)
        {
            isTouchBegan = false;
        }
    }
}