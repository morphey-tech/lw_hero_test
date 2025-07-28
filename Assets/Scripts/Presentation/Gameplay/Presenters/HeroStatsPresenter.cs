using System;
using ContractsInterfaces;
using Domain.Gameplay.MessageDTO;
using Domain.Gameplay.Models;
using JetBrains.Annotations;
using MessagePipe;
using R3;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    [UsedImplicitly]
    public sealed class HeroStatsPresenter : IInitializable, IDisposable
    {
        private readonly HeroStatsModel _model;
        private readonly IUpgradeHeroStatsView _view;
        private readonly IPublisher<string, UpgradeHeroStatDTO> _heroStatsUpgradePublisher;

        private readonly CompositeDisposable _disposable = new();
        
        [Inject]
        private HeroStatsPresenter(HeroStatsModel model,
                                   IUpgradeHeroStatsView view,
                                   IPublisher<string, UpgradeHeroStatDTO> heroStatsUpgradePublisher)
        {
            _model = model;
            _view = view;
            _heroStatsUpgradePublisher = heroStatsUpgradePublisher;
        }
        
        void IInitializable.Initialize()
        {
            _view.OnUpgradeButtonClick += OnUpgradeButtonClick;
            foreach ((EnumHeroStatType type, IHeroStat stat) in _model.Stats)
            {
                //TODO: remove closure
                stat.Amount
                    .Subscribe(x => OnHeroStatsUpgraded(x, type))
                    .AddTo(_disposable);
            }
        }
        
        void IDisposable.Dispose()
        {
            _disposable?.Dispose();
            _view.OnUpgradeButtonClick -= OnUpgradeButtonClick;
        }

        private void OnHeroStatsUpgraded(int amount, EnumHeroStatType type)
        {
            _view.Refresh(amount, type);
        }

        private void OnUpgradeButtonClick()
        {
            PublishStatAddedMessage(new HeroHealthStat{Amount = new ReactiveProperty<int>(10)});
            PublishStatAddedMessage(new HeroDamageStat{Amount = new ReactiveProperty<int>(5)});
            PublishStatAddedMessage(new HeroMovementSpeedStat{Amount =  new ReactiveProperty<int>(3)});
        }

        private void PublishStatAddedMessage(IHeroStat value)
        {
            _heroStatsUpgradePublisher.Publish(UpgradeHeroStatDTO.StatAdded, 
                new UpgradeHeroStatDTO
            {
                Stat = value
            });
        }
    }
}