using System;
using UnityEngine;

public class PCInput : MonoBehaviour, IInput
{
    private KeyCode _jumpKeyCode = KeyCode.Space;

    public Action OnJump { get; set; }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(_jumpKeyCode))
        {
            OnJump?.Invoke();
        }
    }
}