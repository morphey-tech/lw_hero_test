using System;
using ObservableCollections;

namespace Domain.Gameplay.Models
{
    public sealed class HeroStatsModel
    {
        public IReadOnlyObservableDictionary<Type, IHeroStat> Stats => _stats;
        
        private readonly ObservableDictionary<Type, IHeroStat> _stats = new();

        public void Add<T>(in T stat) where T : IHeroStat
        {
            if (stat.Amount < 0)
            {
                throw new InvalidOperationException("Can't increase stat by value equal or less than zero.");
            }
            if (_stats.ContainsKey(typeof(T)))
            {
                _stats[typeof(T)].Amount += stat.Amount;
            }
            else
            {
                _stats.Add(typeof(T), stat);
            }
        }

        public T Get<T>(T stat) where T : IHeroStat
        {
            return _stats.TryGetValue(typeof(T), out IHeroStat result) ? (T)result : default;
        }
    }
}