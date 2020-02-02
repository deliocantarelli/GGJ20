
using UnityEngine;

namespace GGJ20.Target {
    public class Targetable : MonoBehaviour {
        public delegate void OnTargetDestroyed();
        private event OnTargetDestroyed onTargetDestroyed;
        public delegate void OnTargetDamaged();
        private event OnTargetDamaged onTargetDamaged;
        private float life = 5;
        public bool isAlive {get { return life > 0; }}

        public void DealDamage(float damage) {
            if(!isAlive) {
                return;
            }
            life -= damage;
            if(life <= 0) {
                TriggerTargetDestroyed();
            } else {
                TriggerTargetDamaged();
            }
        }

        public void RegisterOnTargetDestroyed(OnTargetDestroyed callback) {
            onTargetDestroyed += callback;
        }
        public void RemoveOnTargetDestroyed(OnTargetDestroyed callback) {
            onTargetDestroyed -= callback;
        }
        public void RegisterOnTargetDamaged(OnTargetDamaged callback) {
            onTargetDamaged += callback;
        }
        public void RemoveOnTargetDamaged(OnTargetDamaged callback) {
            onTargetDamaged -= callback;
        }
        private void TriggerTargetDestroyed() {
            onTargetDestroyed?.Invoke();
        }
        private void TriggerTargetDamaged() {
            onTargetDamaged?.Invoke();
        }
    }
}