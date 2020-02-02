
using UnityEngine;
using Zenject;
using GGJ20.Target;
using GGJ20.World;
using System;
using System.Collections.Generic;
using GGJ20.Utils;

namespace GGJ20.Battery {
    [RequireComponent(typeof(Targetable))]
    public class BatteryController : MonoBehaviour {
        [Inject]
        private GameTargets targets;
        private Targetable targetable;
        private HitChecker hitChecker = new HitChecker();

        private void Start() {
            targetable = GetComponent<Targetable>();
            targetable.RegisterOnTargetDestroyed(OnTargetDestroyed);
            RegisterBattery();
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
        }
        private void Update()
        {
            hitChecker.CheckReset();
        }
    }
}