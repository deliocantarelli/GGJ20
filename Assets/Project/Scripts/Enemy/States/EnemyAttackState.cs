
using GGJ20.Target;
using UnityEngine;

namespace GGJ20.Enemy {
    public class EnemyAttackState : EnemyStateBase
    {
        private Targetable target;

        private float currentDelay = 0;
        public EnemyAttackState(Targetable targetable) {
            target = targetable;
            target.RegisterOnTargetDestroyed(OnTargetDestroyed);
        }
        protected override void Begin() {
            StopMovement();
        }

        private void StopMovement() {
            Enemy.SetMovement(false);
        }

        public override void OnDestroy() {
            RemoveListener();
        }

        public void OnTargetDestroyed() {
            Exit(new EnemyPursueState());
        }

        private void Exit(EnemyStateBase state) {
            RemoveListener();
            ExitTo(state);
        }

        private void RemoveListener() {
            if(target != null) {
                target.RemoveOnTargetDestroyed(OnTargetDestroyed);
            }
        }

        public override void Update()
        {
            CheckAttack();
        }

        private void CheckAttack() {
            currentDelay += Time.deltaTime;
            if(currentDelay > Enemy.settings.attackTimeDelay) {
                currentDelay = 0;
                Attack();
            }
        }

        private void Attack() {
            target.DealDamage(Enemy.settings.damage);
        }
    }
}
