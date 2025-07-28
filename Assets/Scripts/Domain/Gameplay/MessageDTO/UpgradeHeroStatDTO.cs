using Domain.Gameplay.Models;

namespace Domain.Gameplay.MessageDTO
{
    public sealed class UpgradeHeroStatDTO
    {
        public const string StatAdded = "heroStatAddedEvent";
        
        public IHeroStat Stat { get; set; }
    }
}