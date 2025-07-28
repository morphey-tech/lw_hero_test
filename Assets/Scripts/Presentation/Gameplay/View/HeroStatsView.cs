using TMPro;
using UnityEngine;

namespace Presentation.Gameplay.View
{
    public sealed class HeroStatsView : MonoBehaviour
    {
        [field: SerializeField]
        public TextMeshProUGUI Health { get; private set; }
        
        [field: SerializeField]
        public TextMeshProUGUI Damage { get; private set; }
        
        [field: SerializeField]
        public TextMeshProUGUI MovementSpeed { get; private set; }
    }
}