using Events;
using Zenject;

public class SystemDontDestroyInitialization : MonoInstaller
{
   public override void InstallBindings()
   {
     /* #if UNITY_ANDROID
      Container.Bind(typeof(IInput), typeof(ITickable)).To<MobileInput>().AsSingle();
      #else*/
      Container.Bind(typeof(IInput), typeof(ITickable)).To<PCInput>().AsSingle();
      /*#endif*/

      Container.Bind<ILevelFactory>()
         .To<LevelFactory>()
         .AsSingle()
         .WithArguments(new AddressableAssetGetter());

      Container.Bind<IUnitsFactory>()
         .To<UnitsFactory>()
         .AsSingle()
         .WithArguments(new AddressableNonCachedAssetGetter());

      Container.Bind<GameEvents>().AsSingle();
   }
}