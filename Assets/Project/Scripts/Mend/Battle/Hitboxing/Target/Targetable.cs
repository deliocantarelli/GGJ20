
using System;
using UnityEngine;
using UnityEngine.Events;

namespace PointNSheep.Mend.Battle
{
    public class Targetable : MonoBehaviour {
        public delegate void TargetDestroyed();
        private event TargetDestroyed targetDestroyed;
        public delegate void TargetHealthChanged(int damage);
        private event TargetHealthChanged targetHealthChanged;

        public event Action<Targetable> BecameInvulnerable;


        public UnityEvent OnHeal;
        public UnityEvent OnDamage;

        [SerializeField]
        private int life = 5;
        private bool invulnerable;

        public int Life { get { return life; } }

        public bool isAlive {get { return life > 0; }}

        public bool IsInvulnerable { get => invulnerable; }

        public void DealDamage(int damage) {
            if(!isAlive || invulnerable) {
                return;
            }
            life -= damage;
            if(life <= 0) {
                TriggerTargetDestroyed();
            } 
            TriggerHealthChanged(-damage);
        }
        public void Heal(int value)
        {
            if (invulnerable)
                return;
            life += value;
            TriggerHealthChanged(value);
        }

        public void RegisterOnTargetDestroyed(TargetDestroyed callback) {
            targetDestroyed += callback;
        }
        public void RemoveOnTargetDestroyed(TargetDestroyed callback) {
            targetDestroyed -= callback;
        }
        public void RegisterOnHealthChanged(TargetHealthChanged callback) {
            targetHealthChanged += callback;
        }
        public void RemoveOnTargetDamaged(TargetHealthChanged callback) {
            targetHealthChanged -= callback;
        }
        private void TriggerTargetDestroyed() {
            Debug.Log("target destroyed");
            targetDestroyed?.Invoke();
        }

        public void SetInvulnerable()
        {
            invulnerable = true;
            BecameInvulnerable?.Invoke(this);
        }

        private void TriggerHealthChanged(int change) {
            targetHealthChanged?.Invoke(change);
            if (change > 0)
            {
                OnHeal.Invoke();
            }
            else if (change < 0)
            {
                OnDamage.Invoke();
            }
        }
    }
}