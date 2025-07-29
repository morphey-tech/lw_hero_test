using System;
using Domain.Gameplay.MessageDTO;
using Domain.Gameplay.Models;
using JetBrains.Annotations;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace UseCases
{
    [UsedImplicitly]
    public sealed class UpgradeHeroStatsUseCase :  IInitializable, IDisposable
    {
        private readonly HeroStatsModel _model;
        private readonly ISubscriber<string, UpgradeHeroStatDTO> _heroStatSubscriber;

        private IDisposable _disposable;

        [Inject]
        private UpgradeHeroStatsUseCase(HeroStatsModel model, ISubscriber<string, UpgradeHeroStatDTO> heroStatsSubscriber)
        {
            _model = model;
            _heroStatSubscriber = heroStatsSubscriber;
        }
        
        void IInitializable.Initialize()
        {
            _disposable = _heroStatSubscriber.Subscribe(UpgradeHeroStatDTO.StatAdded, OnStatUpgrade);
        }
        
        void IDisposable.Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnStatUpgrade(UpgradeHeroStatDTO dto)
        {
            _model.Add(dto.Stat);
        }
    }
}