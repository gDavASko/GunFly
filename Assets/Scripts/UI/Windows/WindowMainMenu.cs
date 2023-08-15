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

    [SerializeField] private TextMeshProUGUI _textLevelScore = null;
    [SerializeField] private TextMeshProUGUI _textBestScore = null;

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
        _gameEvents = parameters.Get<GameEvents>();

        UpdateValues();
        base.ShowWithParams(parameters);
    }

    private void UpdateValues()
    {
        _textLevel.text = (_storableParams.Get(SaveKey.LevelNumber, 0) + 1).ToString();
        _textLevelScore.text = _storableParams.Get(SaveKey.Scores, 0).ToString();
        _textBestScore.text = _storableParams.Get(SaveKey.BestScores, 0).ToString();
    }


    private void OnPlayClick()
    {
        _gameEvents.OnGameStart?.Invoke();
    }
}