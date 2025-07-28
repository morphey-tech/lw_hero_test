using Domain.Gameplay.Models;

namespace Domain.Gameplay.MessageDTO
{
    public sealed class UpgradeHeroStatDTO
    {
        public IHeroStat Stat { get; set; }
    }
}