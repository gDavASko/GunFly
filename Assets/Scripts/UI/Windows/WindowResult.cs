using Events;
using Saves;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowResult : UIElementBase
{
    [SerializeField] private TextMeshProUGUI _textLevelComplete = null;
    [SerializeField] private TextMeshProUGUI _textLevelScore = null;
    [SerializeField] private TextMeshProUGUI _textBestScore = null;
    [SerializeField] private Button _buttonContinue = null;

    private IStorableParams _storableParams = null;
    private GameEvents _gameEvents = null;
    private GameEvents.GameResult _gameResult = GameEvents.GameResult.Victory;

    public override string Id => "WindowMainMenu";

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<IUIElementAnimator>();
        _buttonContinue.onClick.AddListener(OnContinueClick);
    }

    public override void ShowWithParams(params object[] parameters)
    {
        _storableParams = parameters.Get<IStorableParams>();
        _gameEvents = parameters.Get<GameEvents>();
        _gameResult = parameters.Get<GameEvents.GameResult>();

        UpdateValues();
        base.ShowWithParams(parameters);
    }

    private void UpdateValues()
    {
        string textCaption = $"{_storableParams.Get(SaveKey.LevelNumber, 0).ToString()} Level " +
                             $"{(_gameResult == GameEvents.GameResult.Victory ? "Complete!" : "Lose!")}";

        _textLevelComplete.text = textCaption;
        _textLevelScore.text = _storableParams.Get(SaveKey.Scores, 0).ToString();
        _textBestScore.text = _storableParams.Get(SaveKey.BestScores, 0).ToString();

    }

    private void OnContinueClick()
    {
        if (_gameResult == GameEvents.GameResult.Victory)
        {
            _gameEvents.OnNextGame?.Invoke();
        }
        else
        {
            _gameEvents.OnRestartGame?.Invoke();
        }
    }
}