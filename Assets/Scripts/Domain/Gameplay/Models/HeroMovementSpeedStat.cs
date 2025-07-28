using R3;

namespace Domain.Gameplay.Models
{
    public sealed class HeroMovementSpeedStat : IHeroStat
    {
        public EnumHeroStatType Type { get; set; } = EnumHeroStatType.MOVEMENT_SPEED;
        public ReactiveProperty<int> Amount { get; set; } = new();
    }
}