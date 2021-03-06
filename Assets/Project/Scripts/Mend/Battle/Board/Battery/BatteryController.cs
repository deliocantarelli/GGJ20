using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Assertions;
using DG.Tweening;
using UnityEngine.Events;
using PointNSheep.Common.Health;

namespace PointNSheep.Mend.Battle {
    [Serializable]
    class BatteryConfig {
        public int minLife = 0;
        public GameObject gameObject = null;
        private SpriteRenderer _sprite;
        public SpriteRenderer sprite { get {
            if(_sprite == null) {
                _sprite = gameObject.GetComponent<SpriteRenderer>();
                Assert.IsNotNull(_sprite);
            }
            return _sprite;
        }}
    }
    [RequireComponent(typeof(Targetable))]
    public class BatteryController : MonoBehaviour, IHealth {
        [Inject]
        private GameTargets targets;
        [Inject]
        private BatteryManager batteryManager;
        public event HealthChange HealthChanged;
        public Targetable targetable;
        private SpellHitChecker hitChecker = new SpellHitChecker();
        public event Action BatteryFilled;
        [SerializeField]
        public int maxHealth = 10;
        [SerializeField]
        private Image imageFill;
        private bool filled;
        [SerializeField]
        private BatteryConfig[] batteriesNodes;
        private BatteryConfig currentConfig;
        [SerializeField]
        private Animator animator;


        [Inject]
        private BattleSceneController controller;

        public int CurrentHealth => targetable.Life;
        public int MaxHealth => maxHealth;

        [Inject]
        private void Init() {
            targetable = GetComponent<Targetable>();
            targetable.RegisterOnTargetDestroyed(OnTargetDestroyed);
            targetable.RegisterOnHealthChanged(OnHealthChanged);


            foreach (var node in batteriesNodes)
            {
                node.gameObject.SetActive(false);
            }

            RegisterBattery();
            OnHealthChanged(0);

        }

        private void OnHealthChanged(int change)
        {
            if (change > 0)
            {
                animator.SetTrigger("Heal");
            }
            else if (change < 0)
            {
                animator.SetTrigger("Damage");
            }

            UpdateBatteryNode();
            imageFill.fillAmount = targetable.Life / (float)maxHealth;
            HealthChanged?.Invoke(this, change);
        }

        private void RegisterBattery() {
            targets.RegisterBattery(targetable);
            batteryManager.AddBattery(this);
        }

        private void OnTargetDestroyed() {
            controller.OnLose();
            targets.OnBatteryDestroyed(targetable);
        }

        public void TryHeal(Spell.Hit hit)
        {
            if(hitChecker.CheckHit(hit, out int dmg))
            {
                Heal(dmg);
            }
        }

        private void Heal(int damage)
        {
            targetable.Heal(damage);
            if(targetable.Life >= maxHealth && !filled)
            {
                filled = true;

                targetable.SetInvulnerable();
                targets.OnBatterySaved(targetable);
                BatteryFilled?.Invoke();
            }
        }
        private void Update()
        {
            hitChecker.CheckReset();
        }

        private void UpdateBatteryNode() {
            float p = targetable.Life / (float)maxHealth;

            int index = (int)Math.Floor(p * 3);
            ChangeNode(batteriesNodes[index]);
        }
        private void ChangeNode(BatteryConfig config) {
            if(currentConfig != null) {
                currentConfig.gameObject.SetActive(false);
            }
            currentConfig = config;
            currentConfig.gameObject.SetActive(true);
        }
    }
}