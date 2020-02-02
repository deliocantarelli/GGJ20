
using UnityEngine;
using GGJ20.Target;

namespace GGJ20.Enemy {
    public class EnemyPursueState : EnemyStateBase
    {
        private float currentWait = 0;
        private Transform currentTarget;
        protected override void Begin() {
            SetTarget();
            Enemy.SetMovement(true);
        }
        public override void Update()
        {
            currentWait += Time.deltaTime;
            if(currentWait > Enemy.changeTargetDelay) {
                currentWait = 0;
                SetTarget();
            }
        }

        private void SetTarget() {
            currentTarget = Enemy.GetClosestBattery();
            if(currentTarget != null) {
                Enemy.SetTargetMovement(currentTarget);
            }
        }

        public override void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.tag == "target") {
                Targetable target = other.gameObject.GetComponent<Targetable>();
                if(target != null && target.isAlive) {
                    ExitTo(new EnemyAttackState(target));
                }
            }
        }
    }
}
