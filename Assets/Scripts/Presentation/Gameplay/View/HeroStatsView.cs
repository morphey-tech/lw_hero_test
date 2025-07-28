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
                    DoPunchAnimation(_healthLabel);
                    break;
                case EnumHeroStatType.DAMAGE:
                    _damageLabel.text = $"Damage:{amount.ToString()}";
                    DoPunchAnimation(_damageLabel);
                    break;
                case EnumHeroStatType.MOVEMENT_SPEED:
                    _msLabel.text = $"MS:{amount.ToString()}";
                    DoPunchAnimation(_msLabel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static void DoPunchAnimation(VisualElement target)
        {
            const float duration = 0.1f;
            float time = 0f;
            target.schedule.Execute(() =>
            {
                time += Time.deltaTime;
                float scale = 1f + 0.02f * Mathf.Sin(Mathf.PI * time / duration);
                target.style.scale = new StyleScale(Vector2.one * scale);
            }).Every(3).Until(() => time >= duration);
        }
    }
}