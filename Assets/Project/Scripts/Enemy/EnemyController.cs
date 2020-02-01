
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

        [SerializeField]
        private float changeTargetDelay = 1;
        private Transform target;
        private AIDestinationSetter aiMovement;

        void Start() {
            aiMovement = GetComponent<AIDestinationSetter>();
            InvokeRepeating("FollowClosestBattery", 0, changeTargetDelay);
        }
        private void FollowClosestBattery()
        {
            Transform targetPosition = batteryManager.GetClosestBattery(transform);
            aiMovement.target = targetPosition;
        }
    }
}