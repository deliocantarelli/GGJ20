using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PointNSheep.Common.Pathfinding
{
    public class ObstacleAppear : MonoBehaviour
    {
        [Inject]
        PathFindingManager pathFindingManager;
        
        void OnEnable()
        {
            Debug.Assert(gameObject.layer == LayerMask.NameToLayer("Obstacle"));
            UpdatePathFinding();
        }
        void OnDisable()
        {
            UpdatePathFinding();
        }
        void UpdatePathFinding() {
            pathFindingManager.TriggerRefresh();
        }
    }
}
