
using UnityEngine;
using Zenject;
using GGJ20.Target;
using UnityEngine.SceneManagement;
using GGJ20.World;
using System;
using System.Collections.Generic;
using GGJ20.Utils;
using UnityEngine.UI;
using UnityEngine.Assertions;
using DG.Tweening;
using GGJ20.Game;

namespace GGJ20.Battery {
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
    public class BatteryController : MonoBehaviour {
        [Inject]
        private GameTargets targets;
        [Inject]
        private BatteryManager batteryManager;
        public delegate void HealthChanged(BatteryController battery);
        public event HealthChanged onHealthChanged;
        public Targetable targetable;
        private HitChecker hitChecker = new HitChecker();
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

        private void Start() {
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

        public void RegisterOnHealthChanged(HealthChanged healthChangedEvent) {
            onHealthChanged += healthChangedEvent;
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
            onHealthChanged?.Invoke(this);
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
                BatteryFilled?.Invoke();

                targetable.SetInvulnerable();
                targets.OnBatterySaved(targetable);
            }
        }
        private void Update()
        {
            hitChecker.CheckReset();
        }

        private void UpdateBatteryNode() {
            BatteryConfig minConfig = batteriesNodes[0];
            int minLife = int.MaxValue;

            foreach(BatteryConfig config in batteriesNodes) {
                if(minLife > config.minLife && config.minLife > targetable.Life) {
                    minConfig = config;
                    minLife = config.minLife;
                }
            }
            ChangeNode(minConfig);
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