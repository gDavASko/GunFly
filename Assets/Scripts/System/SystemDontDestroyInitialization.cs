using Events;
using Zenject;

public class SystemDontDestroyInitialization : MonoInstaller
{
   public override void InstallBindings()
   {
      #if UNITY_ANDROID
      Container.Bind<IInput>().To<MobileInput>().AsSingle();
      #else
      Container.Bind<IInput>().To<PCInput>().AsSingle();
      #endif

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