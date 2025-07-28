using System;
using Domain.Gameplay.Models;

namespace ContractsInterfaces
{
    public interface IUpgradeHeroStatsView
    {
        event Action OnUpgradeButtonClick;
        void Refresh(int amount, EnumHeroStatType type);
    }
}