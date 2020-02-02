

using UnityEngine;
using System;
using Zenject;
using System.Collections.Generic;

namespace GGJ20.Battery
{
    public class BatteryManager : MonoBehaviour {
        [Inject]
        private GameBatteries gameBatteries;

        public Transform GetClosestBattery(Transform otherTransform)
        {
            Vector2 pos = otherTransform.position;

            float distance = float.PositiveInfinity;
            BatteryController batteryController = null;

            BatteryController[] batteries = gameBatteries.Batteries;
            if(batteries == null) {
                return null;
            }
            foreach (BatteryController battery in batteries) {
                float currentDis = Vector2.Distance(battery.transform.position, pos);
                if(currentDis < distance) {
                    distance = currentDis;
                    batteryController = battery;
                }
            }
            return batteryController.transform;
        }
    }
}