
using UnityEngine;
using Zenject;

namespace GGJ20.Battery {
    public class BatteryController : MonoBehaviour {
        [Inject]
        private GameBatteries batteryManager;
        private void Start() {
            RegisterBattery();
        }

        private void RegisterBattery() {
            batteryManager.RegisterBattery(this);
        }
    }
}