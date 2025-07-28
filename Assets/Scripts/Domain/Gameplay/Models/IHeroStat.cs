using R3;

namespace Domain.Gameplay.Models
{
    public interface IHeroStat
    {
        EnumHeroStatType Type { get; set; } 
        ReactiveProperty<int> Amount { get; set; }
    }

    public enum EnumHeroStatType
    {
        HEALTH = 0,
        DAMAGE = 1,
        MOVEMENT_SPEED = 3
    }
}
