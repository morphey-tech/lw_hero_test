using System;
using Domain.Gameplay.MessageDTO;
using Domain.Gameplay.Models;
using MessagePipe;

namespace Application
{
    public sealed class UpgradeHeroStatsService : IDisposable
    {
        private readonly HeroStatsModel _model;
        private readonly IDisposable _disposable;

        private UpgradeHeroStatsService(ISubscriber<UpgradeHeroStatDTO> heroStatsSubscriber)
        {
            _model = new HeroStatsModel();
            _disposable = heroStatsSubscriber.Subscribe(OnStatUpgrade);
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