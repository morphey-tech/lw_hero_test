using System;
using System.Collections.Generic;
using ContractsInterfaces;
using Cysharp.Threading.Tasks;
using Domain.Gameplay.Models;
using TMPro;
using UnityEngine;

namespace Presentation.Gameplay.View
{
    public sealed class HeroStatsView : MonoBehaviour, IUpgradeHeroStatsView
    {
        [field: SerializeField]
        public TextMeshProUGUI Health { get; private set; }
        
        [field: SerializeField]
        public TextMeshProUGUI Damage { get; private set; }
        
        [field: SerializeField]
        public TextMeshProUGUI MovementSpeed { get; private set; }

        UniTask IView.Show()
        {
            throw new NotImplementedException();
        }

        UniTask IView.Hide()
        {
            throw new NotImplementedException();
        }

        void IUpgradeHeroStatsView.Refresh(IReadOnlyDictionary<Type, IHeroStat> stats)
        {
            foreach (KeyValuePair<Type,IHeroStat> pair in stats)
            {
                switch (pair.Value)
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
}