using Domain.Gameplay.Models;

namespace Domain.Gameplay.MessageDTO
{
    public sealed class HeroStatAddDTO
    {
        public IHeroStat Stat { get; set; }
    }
}