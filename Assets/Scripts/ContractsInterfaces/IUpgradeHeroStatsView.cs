using System;
using System.Collections.Generic;
using Domain.Gameplay.Models;

namespace ContractsInterfaces
{
    public interface IUpgradeHeroStatsView
    {
        void Refresh(KeyValuePair<Type, IHeroStat> stats);
    }
}