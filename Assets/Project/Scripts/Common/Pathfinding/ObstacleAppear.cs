using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GGJ20.Obstacle {
    public class ObstacleAppear : MonoBehaviour
    {
        [Inject]
        GGJ20.PathFinding.PathFindingManager pathFindingManager;
        
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
