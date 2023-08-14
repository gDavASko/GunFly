using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonClickSound : AudioPlayer
{
    private Button _button = null;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        base.PlaySound();
    }
}