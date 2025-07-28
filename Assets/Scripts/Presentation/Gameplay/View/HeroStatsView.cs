using System;
using System.Collections.Generic;
using ContractsInterfaces;
using Domain.Gameplay.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Gameplay.View
{
    public sealed class HeroStatsView : MonoBehaviour, IUpgradeHeroStatsView
    {
        public event Action OnUpgradeButtonClick;
        
        [field: SerializeField]
        public TextMeshProUGUI Health { get; private set; }
        
        [field: SerializeField]
        public TextMeshProUGUI Damage { get; private set; }
        
        [field: SerializeField]
        public TextMeshProUGUI MovementSpeed { get; private set; }

        [field: SerializeField]
        public Button UpgradeButton { get; private set; }

        private void Awake()
        {
            UpgradeButton.onClick.AddListener(() => OnUpgradeButtonClick?.Invoke());
        }

        private void OnDestroy()
        {
            UpgradeButton.onClick.RemoveAllListeners();
        }
        

        void IUpgradeHeroStatsView.Refresh(KeyValuePair<Type, IHeroStat> stats)
        {
            switch (stats.Value)
            {
                case HeroHealthStat health:
                    Health.text = $"Health:{health.Amount.ToString()}";
                    break;
                case HeroDamageStat damage:
                    Damage.text = $"Damage:{damage.Amount.ToString()}";
                    break;
                case HeroMovementSpeedStat movementSpeed:
                    MovementSpeed.text = $"Movement Speed:{movementSpeed.Amount.ToString()}";
                    break;
            }
        }
    }
}