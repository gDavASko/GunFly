using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonClickSound : AudioPlayer
{
    [SerializeField] private string _soundOnClickId = "LightTap";
    private Button _button = null;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        //ToDo: Remake hardcode to switch variant
        base.PlaySound(_soundOnClickId);
    }
}