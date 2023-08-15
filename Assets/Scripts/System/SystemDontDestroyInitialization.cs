using Events;
using Saves;
using Zenject;

public class SystemDontDestroyInitialization : MonoInstaller
{
   public override void InstallBindings()
   {
      Container.Bind<GameEvents>().AsSingle();
      Container.Bind<UnitEvents>().AsSingle();
      Container.Bind<ItemEvents>().AsSingle();

      Container.Bind<IStatistic<int>>()
         .To<StorableStatistic<int>>()
         .AsSingle()
         .NonLazy();

      /* #if UNITY_ANDROID
       Container.Bind(typeof(IInput), typeof(ITickable))
       .To<MobileInput>()
       .AsSingle()
       .NonLazy();
       #else*/
      Container.Bind(typeof(IInput), typeof(ITickable))
         .To<PCInput>()
         .AsSingle()
         .NonLazy();
      /*#endif*/

      Container.Bind<IStorableParams>()
         .To<JSONSaves>()
         .AsSingle()
         .NonLazy();

      Container.Bind<IStatisticProcessor>()
         .To<StatisticProcessor>()
         .AsSingle()
         .NonLazy();

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

      Container.Bind<IGettableAsset>()
         .To<AddressableNonCachedAssetGetter>()
         .AsSingle();

      Container.Bind<IUnitsFactory>()
         .To<UnitsFactory>()
         .AsSingle();

      Container.Bind<IWeaponFactory>()
         .To<WeaponFactory>()
         .AsSingle();

      Container.Bind<IGameItemsFactory>()
         .To<GameItemsFactory>()
         .AsSingle();

      Container.Bind<IUIFactory>()
         .To<UIFactory>()
         .AsSingle().NonLazy();
   }
}