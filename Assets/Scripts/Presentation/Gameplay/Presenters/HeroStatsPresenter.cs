using System;
using System.Collections.Generic;
using ContractsInterfaces;
using Domain.Gameplay.Models;
using JetBrains.Annotations;
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
        private IDisposable _disposable;
        
        [Inject]
        private HeroStatsPresenter(HeroStatsModel model, IUpgradeHeroStatsView view)
        {
            _model = model;
            _view = view;
        }
        
        void IInitializable.Initialize()
        {
            _disposable = _model.Stats.ToObservable().Subscribe(OnStatsChanged);
            foreach (KeyValuePair<Type,IHeroStat> pair in _model.Stats) {
                _view.Refresh(pair);
            }
        }

        void IDisposable.Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnStatsChanged(KeyValuePair<Type, IHeroStat> stats)
        {
            _view.Refresh(stats);
        }
    }
}