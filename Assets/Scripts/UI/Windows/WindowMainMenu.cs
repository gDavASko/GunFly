using System;
using Events;
using Saves;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowMainMenu : UIElementBase
{
    [SerializeField] private TextMeshProUGUI _textLevel = null;
    [SerializeField] private Button _buttonPlay = null;

    private IStorableParams _storableParams = null;
    private GameEvents _gameEvents = null;

    public override string Id => "WindowMainMenu";

    private void Awake()
    {
        _animator = GetComponent<IUIElementAnimator>();
        _buttonPlay.onClick.AddListener(OnPlayClick);
    }

    public override void ShowWithParams(params object[] parameters)
    {
        _storableParams = parameters.Get<IStorableParams>();
        _storableParams.OnValueChanged += OnValueChanges;
        _gameEvents = parameters.Get<GameEvents>();

        UpdateValues();
        base.ShowWithParams(parameters);
    }

    private void UpdateValues()
    {
        _textLevel.text = (_storableParams.Get(SaveKey.LevelNumber, 0) + 1).ToString();
    }

    private void OnValueChanges(SaveKey key)
    {
        switch (key)
        {
            case SaveKey.LevelNumber:
                _textLevel.text = (_storableParams.Get(key, 0) + 1).ToString();
                break;
        }
    }

    private void OnPlayClick()
    {
        _gameEvents.OnGameStart?.Invoke();
    }
}