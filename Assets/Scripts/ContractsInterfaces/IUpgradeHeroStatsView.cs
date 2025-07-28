using System;
using System.Collections.Generic;
using Domain.Gameplay.Models;

namespace ContractsInterfaces
{
    public interface IUpgradeHeroStatsView
    {
        event Action OnUpgradeButtonClick;
        void Refresh(KeyValuePair<Type, IHeroStat> stats);
    }
}