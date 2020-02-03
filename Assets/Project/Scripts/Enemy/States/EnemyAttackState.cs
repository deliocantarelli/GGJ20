
using GGJ20.Target;
using UnityEngine;

namespace GGJ20.Enemy
{
    public class EnemyAttackState : EnemyStateBase
    {
        private Targetable target;
        private string lastAnimation = "";

        private float currentDelay = 0;
        public EnemyAttackState(Targetable targetable)
        {
            target = targetable;
            target.RegisterOnTargetDestroyed(OnTargetDestroyed);
        }
        protected override void Begin()
        {
            StopMovement();
        }

        private void StopMovement()
        {
            Enemy.SetMovement(false);
        }

        public override void OnDestroy()
        {
            RemoveListener();
        }

        public void OnTargetDestroyed()
        {
            Exit(new EnemyPursueState());
        }

        private void Exit(EnemyStateBase state)
        {
            RemoveListener();
            ExitTo(state);
        }

        private void RemoveListener()
        {
            if (target != null)
            {
                target.RemoveOnTargetDestroyed(OnTargetDestroyed);
            }
        }

        public override void Update()
        {
            CheckAttack();
        }

        private void CheckAttack()
        {
            currentDelay += Time.deltaTime;
            if (currentDelay > Enemy.settings.attackTimeDelay)
            {
                currentDelay = 0;
                Attack();
            }
        }

        private void Attack()
        {
            target.DealDamage(Enemy.settings.damage);
        }

        public override void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.tag == "target")
            {
                Targetable _target = other.gameObject.GetComponent<Targetable>();
                if (_target != null && target == _target)
                {
                    ExitTo(new EnemyPursueState());
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
                    return Enemy.enemyAnimation.attackUp;
                case Direction.LEFT:
                    SetFlip();
                    return Enemy.enemyAnimation.attackLateral;
                case Direction.RIGHT:
                    Unflip();
                    return Enemy.enemyAnimation.attackLateral;
                case Direction.DOWN:
                default:
                    return Enemy.enemyAnimation.attackDown;
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
        private void AddAnimation(AnimationClip animation) {
            if(Enemy.playAnimation.GetClip(animation.name) == null) {
                Enemy.playAnimation.AddClip(animation, animation.name);   
            }
        }

    }
}
