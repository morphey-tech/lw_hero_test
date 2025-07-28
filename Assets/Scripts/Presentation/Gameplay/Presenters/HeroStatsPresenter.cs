using System;
using System.Collections.Generic;
using ContractsInterfaces;
using Domain.Gameplay.MessageDTO;
using Domain.Gameplay.Models;
using JetBrains.Annotations;
using MessagePipe;
using R3;
using UnityEngine;
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
        private IDisposable _disposable;
        
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
            _disposable = _model.Stats.ToObservable().Subscribe(OnStatsChanged);
            foreach (KeyValuePair<Type,IHeroStat> pair in _model.Stats) {
                _view.Refresh(pair);
            }
            _view.OnUpgradeButtonClick += OnUpgradeButtonClick;
        }

        void IDisposable.Dispose()
        {
            _disposable?.Dispose();
            _view.OnUpgradeButtonClick -= OnUpgradeButtonClick;
        }

        private void OnStatsChanged(KeyValuePair<Type, IHeroStat> stats)
        {
            _view.Refresh(stats);
        }

        private void OnUpgradeButtonClick()
        {
            _heroStatsUpgradePublisher.Publish(UpgradeHeroStatDTO.StatAdded, 
                new UpgradeHeroStatDTO
                {
                    Stat = new HeroHealthStat
                    {
                        Amount = 10
                    }
                });
            _heroStatsUpgradePublisher.Publish(UpgradeHeroStatDTO.StatAdded, 
                new UpgradeHeroStatDTO
                {
                    Stat = new HeroDamageStat
                    {
                        Amount = 10
                    }
                });
            _heroStatsUpgradePublisher.Publish(UpgradeHeroStatDTO.StatAdded, 
                new UpgradeHeroStatDTO
                {
                    Stat = new HeroMovementSpeedStat
                    {
                        Amount = 10
                    }
                });
        }
    }
}