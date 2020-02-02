
using UnityEngine;

namespace GGJ20.Target {
    public class Targetable : MonoBehaviour {
        public delegate void TargetDestroyed();
        private event TargetDestroyed targetDestroyed;
        public delegate void TargetHealthChanged();
        private event TargetHealthChanged targetHealthChanged;

        [SerializeField]
        private int life = 5;
        public bool isAlive {get { return life > 0; }}

        public void DealDamage(int damage) {
            if(!isAlive) {
                return;
            }
            life -= damage;
            if(life <= 0) {
                TriggerTargetDestroyed();
            } 
            TriggerHealthChanged();
        }
        public void Heal(int value)
        {
            life += value;
            TriggerHealthChanged();
        }

        public void RegisterOnTargetDestroyed(TargetDestroyed callback) {
            targetDestroyed += callback;
        }
        public void RemoveOnTargetDestroyed(TargetDestroyed callback) {
            targetDestroyed -= callback;
        }
        public void RegisterOnTargetDamaged(TargetHealthChanged callback) {
            targetHealthChanged += callback;
        }
        public void RemoveOnTargetDamaged(TargetHealthChanged callback) {
            targetHealthChanged -= callback;
        }
        private void TriggerTargetDestroyed() {
            Debug.Log("target destroyed");
            targetDestroyed?.Invoke();
        }
        private void TriggerHealthChanged() {
            targetHealthChanged?.Invoke();
        }
    }
}