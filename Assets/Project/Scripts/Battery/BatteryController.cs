
using UnityEngine;
using Zenject;
using GGJ20.Target;
using UnityEngine.SceneManagement;

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
            GameResult.Result = false;
            SceneManager.LoadScene("Endgame");
            targets.RemoveBattery(targetable);
        }
    }
}