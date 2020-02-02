using UnityEngine;

namespace GGJ20.Battery {
    public class BatteryManager : MonoBehaviour{
        public void OnBatteryDestroyed() {
            Debug.Log("GAME OVER!!");
        }
    }
}
