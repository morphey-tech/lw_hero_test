using System;
using System.Collections.Generic;
using Domain.Gameplay.Models;

namespace ContractsInterfaces
{
    public interface IUpgradeHeroStatsView : IView
    {
        void Refresh(IReadOnlyDictionary<Type, IHeroStat> stats);
    }
}