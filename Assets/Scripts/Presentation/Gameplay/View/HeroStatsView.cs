using System;
using System.Collections.Generic;
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
        private Button _upgradeButton;
        
        private readonly Dictionary<EnumHeroStatType, Label> _statLabels = new();
        
        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _statLabels[EnumHeroStatType.HEALTH] = _root.Q<Label>("HealthStat");
            _statLabels[EnumHeroStatType.DAMAGE] = _root.Q<Label>("DamageStat");
            _statLabels[EnumHeroStatType.MOVEMENT_SPEED] = _root.Q<Label>("MovementSpeedStat");
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
            DoPunchAnimation(_upgradeButton, -0.02f);
        }

        void IUpgradeHeroStatsView.Refresh(int amount, EnumHeroStatType type)
        {
            if (_statLabels.TryGetValue(type, out Label label))
            {
                label.text = FormatStat(type, amount);
                DoPunchAnimation(label, 0.02f);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        private static string FormatStat(EnumHeroStatType type, int value)
        {
            return type switch
            {
                EnumHeroStatType.HEALTH => $"Health: {value.ToString()}",
                EnumHeroStatType.DAMAGE => $"Damage: {value.ToString()}",
                EnumHeroStatType.MOVEMENT_SPEED => $"MS: {value.ToString()}",
                _ => $"{type}: {value.ToString()}"
            };
        }

        private static void DoPunchAnimation(VisualElement target, float punchPower)
        {
            const float duration = 0.1f;
            float time = 0f;
            target.schedule.Execute(() =>
            {
                time += Time.deltaTime;
                float scale = 1f + punchPower * Mathf.Sin(Mathf.PI * time / duration);
                target.style.scale = new StyleScale(Vector2.one * scale);
            }).Every(6).Until(() => time >= duration);
        }
    }
}