
using UnityEngine;
using Zenject;
using GGJ20.Battery;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using GGJ20.Common;

namespace GGJ20.UI{
    public class LifeBarUI : MonoBehaviour {
        [Inject] 
        private BatteryManager batteryManager;
        [SerializeField]
        private Image image;

        List<BatteryController> batteries = new List<BatteryController>();

        void Start()
        {
            BatteryController[] batteries = batteryManager.RegisterBatterySpawned(OnBatterySpawned);

            foreach(BatteryController battery in batteries) {
                OnBatterySpawned(battery);
            }
        }

        private void OnBatterySpawned(BatteryController battery) {
            battery.HealthChanged += OnBatteryHealthChanged;
            batteries.Add(battery);
            UpdateBatteriesGoal();
        }

        private void OnBatteryHealthChanged(IHealth health, int change)
        {
            UpdateBatteriesGoal();
        }

        private void UpdateBatteriesGoal()
        {
            float maxLife = 0;
            float currentLife = 0;
            foreach(BatteryController battery in batteries) {
                currentLife += battery.targetable.Life;
                maxLife += battery.maxHealth;
            }
            image.fillAmount = currentLife / maxLife;
        }

    }
}