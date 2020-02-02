
using System;
using Zenject;
using GGJ20.Battery;
using UnityEngine;
using Pathfinding;

namespace GGJ20.Enemy
{
    [RequireComponent(typeof(AIDestinationSetter))]
    public class EnemyController : MonoBehaviour {
        [Inject]
        private BatteryManager batteryManager;
        public EnemySettings settings;
        [SerializeField]
        public float changeTargetDelay = 1;
        private Transform target;
        private AIDestinationSetter aiMovement;
        private AILerp aILerp;
        private EnemyStateMachine stateMachine = new EnemyStateMachine();

        void Start() {
            aiMovement = GetComponent<AIDestinationSetter>();
            aILerp = GetComponent<AILerp>();

            stateMachine.Begin(this);
        }
        void OnDestroy()
        {
            stateMachine.OnDestroy();
        }
        public Transform GetClosestBattery() {
            Transform targetPosition = batteryManager.GetClosestBattery(transform);
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
    }
}