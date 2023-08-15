using System;
using System.Threading.Tasks;
using Events;
using Saves;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour
{
    [SerializeField] private string _windowMainMenuId = "WindowMainMenu";
    [SerializeField] private string _windowBattleId = "WindowBattle";
    [SerializeField] private string _windowResultId = "WindowResult";
    [SerializeField] private string _windowFailId = "WindowFail";
    [SerializeField] private RectTransform _rootElements = null;
    [SerializeField] private GameObject _windowLoading = null;

    private IUIFactory _uiFactory = null;
    private GameEvents _gameEvents = null;
    private UnitEvents _unitEvents = null;
    private IStorableParams _storableParams = null;
    private IUIElement _lastWindow = null;
    private IUnit _player = null;

    [Inject]
    public void Init(IUIFactory uiFactory, GameEvents gameEvents, UnitEvents unitEvents, IStorableParams storableParams)
    {
        _uiFactory = uiFactory;

        _gameEvents = gameEvents;
        _gameEvents.OnGameLoaded += OnGameLoaded;
        _gameEvents.OnGameFinish += OnGameFinish;
        _gameEvents.OnNextGame += OnGameLoaded;
        _gameEvents.OnRestartGame += OnStartForce;
        _gameEvents.OnToMainMenu += OnGameLoaded;

        _unitEvents = unitEvents;
        _unitEvents.OnUnitCreated += OnUnitCreated;

        _storableParams = storableParams;

        _lastWindow = _windowLoading.GetComponent<IUIElement>();
        _lastWindow.ShowWithParams(_gameEvents);
    }

    private void OnStartForce()
    {
        OnGameLoaded();
        _gameEvents.OnGameStart();
    }

    private void OnUnitCreated(IUnit unit)
    {
        if (unit.Transform.CompareTag(GameController.PLAYER_ID))
        {
            _player = unit;
            OnGameStart();
        }
    }

    private async void OnGameStart()
    {
        _lastWindow = await ShowWindow(_windowBattleId);
        _lastWindow.ShowWithParams(_storableParams, _player);
    }

    private async void OnGameFinish(GameEvents.GameResult result)
    {
        if (result == GameEvents.GameResult.Victory)
        {
            _lastWindow = await ShowWindow(_windowResultId);
        }
        else
        {
            _lastWindow = await ShowWindow(_windowFailId);
        }

        _lastWindow.ShowWithParams(_storableParams, result, _gameEvents);
    }

    private async void OnGameLoaded()
    {
        _lastWindow = await ShowWindow(_windowMainMenuId);
        _lastWindow.ShowWithParams(_storableParams, _gameEvents);
    }

    private async Task<IUIElement> ShowWindow(string windowId)
    {
        _lastWindow.Hide(false);

        var window = await _uiFactory.GetUIElement<IUIElement>(windowId);
        if (window.RectTransform.parent == null)
        {
            window.RectTransform.SetParent(_rootElements);
            window.RectTransform.sizeDelta = Vector2.zero;
            window.RectTransform.localPosition = Vector2.zero;
        }

        return window;
    }
}