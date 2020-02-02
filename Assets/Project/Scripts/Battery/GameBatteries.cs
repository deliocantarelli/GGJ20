
using System;
using System.Collections.Generic;

namespace GGJ20.Battery {
    public class GameBatteries
    {
        List<BatteryController> batteries = new List<BatteryController>();

        public BatteryController[] Batteries { get; private set; }
        public void RegisterBattery(BatteryController batteryController)
        {
            batteries.Add(batteryController);
            Batteries = batteries.ToArray();
        }
        public void RemoveBattery(BatteryController batteryController) {
            batteries.Remove(batteryController);
            Batteries = batteries.ToArray();
        }
    }
}