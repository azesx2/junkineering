using Core.Config;
using Core.DataProviders;
using Core.Factories;
using Core.Services;
using Core.Services.UnitSpawnerService;
using Core.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Lifetimes
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private UnitsView[] _unitsViews;
        [SerializeField] private MatchConfigScriptableObject _matchConfigScriptableObject;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterUnitsViews(builder);

            builder.Register(_ => new ScriptableObjectMatchConfigProvider(_matchConfigScriptableObject), Lifetime.Scoped)
                .AsImplementedInterfaces();

            builder.Register<AddressableUnitSpawner>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<MatchService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TargetingService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MatchUnitsService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<UnitControllerFactory>(Lifetime.Scoped).AsImplementedInterfaces();
        }

        private void RegisterUnitsViews(IContainerBuilder builder)
        {
            foreach (var unitsView in _unitsViews)
            {
                builder.RegisterInstance(unitsView).AsSelf();
            }
        }
    }
}
