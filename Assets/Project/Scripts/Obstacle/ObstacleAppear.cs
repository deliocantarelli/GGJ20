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
            UpdatePathFinding();
        }
        void OnDisable()
        {
            UpdatePathFinding();
        }
        void UpdatePathFinding() {
            pathFindingManager.Refresh();
        }
    }
}
