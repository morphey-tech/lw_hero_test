using ContractsInterfaces;
using Domain.Gameplay.MessageDTO;
using Domain.Gameplay.Models;
using MessagePipe;
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
            MessagePipeOptions messagePipeOptions =
                builder.RegisterMessagePipe(o => { o.EnableCaptureStackTrace = true; });
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
            builder.RegisterMessageBroker<string, UpgradeHeroStatDTO>(messagePipeOptions);

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