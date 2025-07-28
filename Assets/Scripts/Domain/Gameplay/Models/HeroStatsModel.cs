using System;
using ObservableCollections;

namespace Domain.Gameplay.Models
{
    public sealed class HeroStatsModel
    {
        public IReadOnlyObservableDictionary<EnumHeroStatType, IHeroStat> Stats => _stats;
        
        private readonly ObservableDictionary<EnumHeroStatType, IHeroStat> _stats = new();

        public void Add(IHeroStat stat)
        {
            if (_stats.TryGetValue(stat.Type, out IHeroStat existed))
            {
                existed.Amount.Value += stat.Amount.Value;
            }
            else
            {
                if (stat.Amount.Value < 0)
                {
                    throw new InvalidOperationException("Can't add stat with negative value.");
                }
                _stats.Add(stat.Type, stat);
            }
        }

        public IHeroStat Get(EnumHeroStatType statType)
        {
            return _stats.TryGetValue(statType, out IHeroStat result) ? result : default;
        }
    }
}