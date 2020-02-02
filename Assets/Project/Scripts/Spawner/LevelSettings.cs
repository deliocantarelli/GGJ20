
using System.Collections.Generic;
using UnityEngine;


namespace GGJ20.Spawner {
    public class LevelSettings : MonoBehaviour {
        public bool usePercentage = false;
        public SpawnerSettings[] spawnersStart;

        public WaveEnemy[] enemies;
        public Vector2 waitTime;
        public Vector2Int enemiesQuantity;
    }
}