using R3;

namespace Domain.Gameplay.Models
{
    public sealed class HeroHealthStat : IHeroStat
    {
        public EnumHeroStatType Type { get; set; } = EnumHeroStatType.HEALTH;
        public ReactiveProperty<int> Amount { get; set; } = new();
    }
}