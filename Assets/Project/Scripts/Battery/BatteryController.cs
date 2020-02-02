
using UnityEngine;
using Zenject;
using GGJ20.Target;

namespace GGJ20.Battery {
    [RequireComponent(typeof(Targetable))]
    public class BatteryController : MonoBehaviour {
        [Inject]
        private GameTargets targets;
        private Targetable targetable;
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
    }
}