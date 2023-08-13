using Events;
using UnityEngine;
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

      Container.Bind<IWeaponInitConfigAccessor>()
         .To<WeaponInitConfigsAccessorSO>()
         .AsSingle()
         .NonLazy();

      Container.Bind<IDamageConfigAccessor>()
         .To<DamageConfigAccessorSO>()
         .AsSingle()
         .NonLazy();

      Container.Bind<ILevelFactory>()
         .To<LevelFactory>()
         .AsSingle()
         .WithArguments(new AddressableAssetGetter());


      var noneCacahbleGetter = new AddressableNonCachedAssetGetter();
      Container.Bind<IUnitsFactory>()
         .To<UnitsFactory>()
         .AsSingle()
         .WithArguments(noneCacahbleGetter);

      Container.Bind<IWeaponFactory>()
         .To<WeaponFactory>()
         .AsSingle()
         .WithArguments(noneCacahbleGetter);

      Container.Bind<GameEvents>().AsSingle();
   }
}