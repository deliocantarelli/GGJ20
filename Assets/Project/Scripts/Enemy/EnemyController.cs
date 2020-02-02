
using System;
using Zenject;
using GGJ20.Battery;
using UnityEngine;
using Pathfinding;
using GGJ20.Target;

namespace GGJ20.Enemy
{
    [RequireComponent(typeof(AIDestinationSetter))]
    public class EnemyController : MonoBehaviour {
        [Inject]
        private TargetsManager targetsManager;
        public EnemySettings settings;
        [SerializeField]
        public float changeTargetDelay = 1;
        private Transform target;
        private AIDestinationSetter aiMovement;
        private AILerp aILerp;
        private EnemyStateMachine stateMachine = new EnemyStateMachine();
        private int currentLife = 0;
        public bool isAlive {get{ return currentLife <= 0; }}

        public class Factory : PlaceholderFactory<UnityEngine.Object, EnemyController>
        {
        }

        void Start() {
            aiMovement = GetComponent<AIDestinationSetter>();

            stateMachine.Begin(this);
        }

        public void Setup(EnemySettings enemySettings) {
            settings = enemySettings;
            aILerp = GetComponent<AILerp>();
            aILerp.speed = settings.speed;
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
            stateMachine.Update();
        }

        public void SetMovement(bool movement) {
            aILerp.canMove = movement;
        }
        void OnCollisionEnter2D(Collision2D other)
        {
            stateMachine.OnCollisionEnter2D(other);
        }

        public void Damage(int damage) {
            currentLife -= damage;
            if(currentLife <= 0) {
                Die();
            }
        }

        void Die() {
            Destroy(gameObject);
        }
    }
}