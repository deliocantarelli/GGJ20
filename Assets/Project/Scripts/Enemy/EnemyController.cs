
using System;
using Zenject;
using GGJ20.Battery;
using UnityEngine;
using Pathfinding;
using GGJ20.Target;
using GGJ20.World;
using UnityEngine.UI;
using GGJ20.Game;
using UnityEngine.Events;

namespace GGJ20.Enemy
{
    public enum Direction
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3
    }
    [Serializable]
    public class EnemyAnimation
    {
        public AnimationClip walkDown;
        public AnimationClip walkLateral;
        public AnimationClip walkUp;
        public AnimationClip attackUp;
        public AnimationClip attackLateral;
        public AnimationClip attackDown;
        public AnimationClip death;
    }
    [RequireComponent(typeof(Animation))]
    [RequireComponent(typeof(AIDestinationSetter))]
    [RequireComponent(typeof(AILerp))]
    public class EnemyController : MonoBehaviour {
        [Inject]
        private TargetsManager targetsManager;
        public EnemySettings settings;
        [SerializeField]
        public float changeTargetDelay = 1;
        [SerializeField]
        private Image imageSlider;
        [SerializeField]
        public EnemyAnimation enemyAnimation;
        public GameObject endGameObject;
        [NonSerialized]
        public Animation playAnimation;
        [NonSerialized]
        public Animator animator;
        private Transform target;
        private AIDestinationSetter aiMovement;
        private AILerp aILerp;
        private EnemyStateMachine stateMachine = new EnemyStateMachine();
        private int currentLife = 0;
        private HitChecker hitChecker = new HitChecker();
        [Inject]
        private BattleSceneController sceneController;
        public UnityEvent OnDamage;

        public bool isAlive { get { return currentLife <= 0; } }

        private Vector3 lastPosition;
        [NonSerialized]
        public Vector3 directionVector;
        [NonSerialized]
        public Direction direction;

        public class Factory : PlaceholderFactory<UnityEngine.Object, EnemyController>
        {
        }

        void Start() {
            lastPosition = transform.position;
            aiMovement = GetComponent<AIDestinationSetter>();
            playAnimation = GetComponent<Animation>();
            animator = GetComponent<Animator>();

            Setup(settings);
            stateMachine.Begin(this);
        }

        public void Setup(EnemySettings enemySettings) {
            settings = enemySettings;
            aILerp = GetComponent<AILerp>();
            aILerp.speed = settings.speed + UnityEngine.Random.Range(0, settings.speed / 10);
            currentLife = settings.life;
            sceneController.BattleOver += BattleOver;
        }

        private void BattleOver(GameResult gameResult)
        {
            stateMachine.OnGameOver();
        }

        void OnDestroy()
        {
            stateMachine.OnDestroy();
        }
        public Transform GetClosestBattery() {
            Transform targetPosition = targetsManager.GetClosestTarget(transform);
            return targetPosition;
        }
        public void SetTargetMovement(Transform targetPosition)
        {
            aiMovement.target = targetPosition;
        }

        void Update()
        {
            ComputeDirection();
            stateMachine.Update();
            hitChecker.CheckReset();

        }

        public void SetMovement(bool movement) {
            aILerp.canMove = movement;
        }
        void OnCollisionEnter2D(Collision2D other)
        {
            stateMachine.OnCollisionEnter2D(other);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            stateMachine.OnCollisionExit2D(collision);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            stateMachine.OnTriggerEnter2D(other);
        }

        public void Damage(int damage) {
            OnDamage.Invoke();
            currentLife -= damage;
            imageSlider.fillAmount = currentLife / (float)settings.life;
            if (currentLife <= 0) {
                Die();
            }
        }
        public void OnGameWon() {
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            //All Logic is now on component EnemyGameWonAnimation
        }
        void Die() {
            Destroy(gameObject);
        }

        public void TryHit(Spell.Hit hit)
        {
            if (hitChecker.CheckHit(hit, out int dmg))
            {
                Damage(dmg);
            }
        }
        private void ComputeDirection()
        {
            directionVector = transform.position - lastPosition;
            lastPosition = transform.position;

            if(Math.Abs(directionVector.x) > Math.Abs(directionVector.y))
            {
                if(directionVector.x > 0)
                {
                    direction = Direction.RIGHT;
                }
                else
                {
                    direction = Direction.LEFT;
                }
            }
            else
            {
                if(directionVector.y > 0)
                {
                    direction = Direction.UP;
                } else {
                    direction = Direction.DOWN;
                }
            }

        }
    }
}