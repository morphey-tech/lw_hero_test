using R3;

namespace Domain.Gameplay.Models
{
    public sealed class HeroDamageStat : IHeroStat
    {
        public EnumHeroStatType Type { get; set; } = EnumHeroStatType.DAMAGE;
        public ReactiveProperty<int> Amount { get; set; } = new();
        
        public HeroDamageStat(int amount)
        {
            Amount.Value = amount;
        }
    }
}