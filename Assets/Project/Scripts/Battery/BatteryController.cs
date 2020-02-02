
using UnityEngine;
using Zenject;
using GGJ20.Target;
using UnityEngine.SceneManagement;
using GGJ20.World;
using System;
using System.Collections.Generic;
using GGJ20.Utils;
using UnityEngine.UI;

namespace GGJ20.Battery {
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
        private Slider slider;
        private bool filled;

        private void Start() {
            targetable = GetComponent<Targetable>();
            targetable.RegisterOnTargetDestroyed(OnTargetDestroyed);
            targetable.RegisterOnHealthChanged(OnHealthChanged);
            RegisterBattery();
            OnHealthChanged();
        }

        public void RegisterOnHealthChanged(HealthChanged healthChangedEvent) {
            onHealthChanged += healthChangedEvent;
        }

        private void OnHealthChanged()
        {
            slider.value = targetable.Life / (float)maxHealth;
            onHealthChanged?.Invoke(this);
        }

        private void RegisterBattery() {
            targets.RegisterBattery(targetable);
            batteryManager.AddBattery(this);
        }

        private void OnTargetDestroyed() {
            GameResult.Result = false;
            SceneManager.LoadScene("Endgame");
            targets.RemoveBattery(targetable);
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
                targets.RemoveBattery(targetable);
            }
        }
        private void Update()
        {
            hitChecker.CheckReset();
        }
    }
}