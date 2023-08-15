using UnityEngine;
using Zenject;

public class SystemInitialization : MonoInstaller
{
   [SerializeField] private GameController _gameController = null;
   [SerializeField] private UIController _uiController = null;
   [SerializeField] private CameraController _cameraController = null;

   public override void InstallBindings()
   {
      Container.Bind<GameController>()
         .FromInstance(_gameController)
         .AsSingle()
         .NonLazy();

      Container.Bind<UIController>()
         .FromInstance(_uiController)
         .AsSingle()
         .NonLazy();

      Container.Bind<CameraController>()
         .FromInstance(_cameraController)
         .AsSingle()
         .NonLazy();
   }
}