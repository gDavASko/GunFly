using Zenject;

public class SystemInitialization : MonoInstaller
{
   public override void InstallBindings()
   {
      base.InstallBindings();

      #if UNITY_ANDROID
      Container.Bind<IInput>().To<MobileInput>().AsSingle();
      #else
      Container.Bind<IInput>().To<PCInput>().AsSingle();
      #endif

      Container.Bind<LevelFactory>().AsSingle().WithArguments(new AddressableAssetGetter());
      Container.Bind<UnitsFactory>().AsSingle().WithArguments(new AddressableNonCachedAssetGetter());
   }
}