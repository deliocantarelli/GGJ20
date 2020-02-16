using System.Collections.Generic;
using UnityEngine;

namespace PointNSheep.Mend.Battle
{
    public class BatteryManager : MonoBehaviour{
        public delegate void BatterySpawned(BatteryController battery);
        private BatterySpawned onBatterySpawned;
        List<BatteryController> batteries = new List<BatteryController>();
        public void OnBatteryDestroyed() {
            Debug.Log("GAME OVER!!");
        }

        public void AddBattery(BatteryController battery) {
            batteries.Add(battery);

            onBatterySpawned?.Invoke(battery);
        }

        public BatteryController[] RegisterBatterySpawned(BatterySpawned batterySpawnedEvent) {
            onBatterySpawned += batterySpawnedEvent;

            return batteries.ToArray();
        }
        public void RemoveBatterySpawnedEvent(BatterySpawned batterySpawnedEvent) {
            onBatterySpawned -= batterySpawnedEvent;
        }
    }
}
