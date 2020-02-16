using UnityEngine;

namespace PointNSheep.Mend.Battle
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
            target.BecameInvulnerable += OnTargetInvulnerable;
        }

        protected override void Begin()
        {
            StopMovement();
        }

        private void StopMovement()
        {
            Enemy.SetMovement(false);
        }

        private void OnTargetDestroyed()
        {
            ExitTo(new EnemyPursueState());
        }

        private void OnTargetInvulnerable(Targetable target)
        {
            ExitTo(new EnemyPursueState());
        }

        protected override void End()
        {
            RemoveListener();
        }

        private void RemoveListener()
        {
            if (target != null)
            {
                target.RemoveOnTargetDestroyed(OnTargetDestroyed);

                target.BecameInvulnerable += OnTargetInvulnerable;
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
            Enemy.Damage(1);
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
