using System;
using ContractsInterfaces;
using Domain.Gameplay.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace Presentation.Gameplay.View
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class HeroStatsView : MonoBehaviour, IUpgradeHeroStatsView
    {
        public event Action OnUpgradeButtonClick;

        private VisualElement _root;
        private Label _healthLabel;
        private Label _damageLabel;
        private Label _msLabel;
        private Button _upgradeButton;
        
        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _healthLabel = _root.Q<Label>("HealthStat");
            _damageLabel = _root.Q<Label>("DamageStat");
            _msLabel = _root.Q<Label>("MovementSpeedStat");
            _upgradeButton = _root.Q<Button>("UpgradeButton");
            _upgradeButton.clicked += OnClick;
        }

        private void OnDestroy()
        {
            _upgradeButton.clicked -= OnClick;
        }

        private void OnClick()
        {
            OnUpgradeButtonClick?.Invoke();
        }

        void IUpgradeHeroStatsView.Refresh(int amount, EnumHeroStatType type)
        {
            switch (type)
            {
                case EnumHeroStatType.HEALTH:
                    _healthLabel.text = $"Health:{amount.ToString()}";
                    break;
                case EnumHeroStatType.DAMAGE:
                    _damageLabel.text = $"Damage:{amount.ToString()}";
                    break;
                case EnumHeroStatType.MOVEMENT_SPEED:
                    _msLabel.text = $"MS:{amount.ToString()}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}