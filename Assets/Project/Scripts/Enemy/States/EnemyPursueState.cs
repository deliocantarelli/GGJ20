
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
            UpdateAnimation();
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
        private void UpdateAnimation()
        {
            Direction direction = Enemy.direction;

            AnimationClip animation = GetAnimation(direction);

            Enemy.animator.Play(animation.name);
        }

        private AnimationClip GetAnimation(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    return Enemy.enemyAnimation.walkUp;
                case Direction.LEFT:
                    SetFlip();
                    return Enemy.enemyAnimation.walkLateral;
                case Direction.RIGHT:
                    Unflip();
                    return Enemy.enemyAnimation.walkLateral;
                case Direction.DOWN:
                default:
                    return Enemy.enemyAnimation.walkDown;
            }
        }
        private void SetFlip()
        {
            if (Enemy.transform.localScale.x > 0)
            {
                Vector2 scale = Enemy.transform.localScale;
                scale.x = -scale.x;
                Enemy.transform.localScale = scale;
            }
        }
        private void Unflip()
        {
            if (Enemy.transform.localScale.x < 0)
            {
                Vector2 scale = Enemy.transform.localScale;
                scale.x = -scale.x;
                Enemy.transform.localScale = scale;
            }
        }
        private void AddAnimation(AnimationClip animation)
        {
            if (Enemy.playAnimation.GetClip(animation.name) == null)
            {
                Enemy.playAnimation.AddClip(animation, animation.name);
            }
        }


    }
}
