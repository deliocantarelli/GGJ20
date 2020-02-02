
using UnityEngine;
using Zenject;
using GGJ20.Target;
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
        private Targetable targetable;
        private HitChecker hitChecker = new HitChecker();
        [SerializeField]
        private int maxHealth = 10;
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

        private void OnHealthChanged()
        {
            slider.value = targetable.Life / (float)maxHealth;
        }

        private void RegisterBattery() {
            targets.RegisterBattery(targetable);
        }

        private void OnTargetDestroyed() {
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
                targetable.SetInvulnerable();
            }
        }
        private void Update()
        {
            hitChecker.CheckReset();
        }
    }
}