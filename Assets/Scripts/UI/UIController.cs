using Events;
using Saves;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour, IUIController
{
    [SerializeField] private string _resDrowPanelId = "StatisticDrawer";
    [SerializeField] private GameObject _windowLoading = null;

    private IUIFactory _uiFactory = null;
    private GameEvents _gameEvents = null;
    private IStorableParams _storableParams = null;

    [Inject]
    public void Init(IUIFactory uiFactory, GameEvents gameEvents, IStorableParams storableParams)
    {
        _uiFactory = uiFactory;

        _gameEvents = gameEvents;
        _gameEvents.OnGameLoaded += OnGameLoaded;

        _storableParams = storableParams;
    }

    private async void OnGameLoaded()
    {
        _windowLoading.SetActive(false);
        var panel = await _uiFactory.GetUIElement<IUIElement>(_resDrowPanelId);
        panel.RectTransform.SetParent(this.transform);
        panel.Init(_storableParams);
    }
}