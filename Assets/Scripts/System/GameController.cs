using Events;
using UnityEngine;
using Zenject;

public sealed class GameController : MonoBehaviour
{
    public const string PLAYER_ID = "Player";
    public const string ENEMY_ID = "Enemy";


    private IWeaponFactory _weaponFactory = null;
    private ILevelController _levelController = null;

    private GameEvents _gameEvents = null;
    private ItemEvents _itemEvents = null;

    private ILevel _currentLevel = null;
    private IInput _input = null;
    private IUnit _player = null;

    [Inject]
    public void Construct(ILevelController levelController, IWeaponFactory weaponFactory,
        IInput input, GameEvents gameEvents, ItemEvents itemEvents)
    {
        _levelController = levelController;
        _weaponFactory = weaponFactory;
        _input = input;

        _gameEvents = gameEvents;
        gameEvents.OnGameFinish += OnGameFinish;
        gameEvents.OnGameStart += OnGameStart;

        _itemEvents = itemEvents;
        _itemEvents.OnItemPlayerCollision += OnItemGet;
    }

    //ToDo: Move to GameItemsController
    private async void OnItemGet(IGameItem item)
    {
        if (item.IsWeapon)
        {
            IWeaponProcessor weaponProcessor = _player.Transform.GetComponent<IWeaponProcessor>();
            weaponProcessor?
                .SetWeapon(await _weaponFactory.CreateWeapon<IWeapon>(item.id, _player.Transform.gameObject, ENEMY_ID));
        }
    }

    private void OnGameFinish(GameEvents.GameResult result)
    {
        _player.Transform.gameObject.SetActive(false);
    }

    private void OnGameStart()
    {
        InitPlayer();
    }

    private async void InitPlayer()
    {
        _player = await _levelController.ConstructUnit(PLAYER_ID, _levelController.CurrentLevel.PlayerSpawnPoint);
        _player.OnDeath += OnPlayerDeath;

        var controllers = _player.Transform.GetComponentsInChildren<IInputInit>();
        if (controllers != null)
        {
            foreach (var controller in controllers)
            {
                controller.Init(_input);
            }
        }
    }

    private void OnPlayerDeath()
    {
        _gameEvents.OnGameFinish?.Invoke(GameEvents.GameResult.Death);
    }
}