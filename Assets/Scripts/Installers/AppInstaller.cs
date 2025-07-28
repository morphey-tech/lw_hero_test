using ContractsInterfaces;
using Domain.Gameplay.Models;
using Presentation.Gameplay.Presenters;
using UseCases;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public sealed class AppInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            HeroStatsModel statsModel = new();
            statsModel.Add(new HeroHealthStat());
            statsModel.Add(new HeroDamageStat());
            statsModel.Add(new HeroMovementSpeedStat());
            builder.RegisterInstance(new HeroStatsModel());
            builder.Register<UpgradeHeroStatsUseCase>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<IUpgradeHeroStatsView>();
            builder.Register<HeroStatsPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}