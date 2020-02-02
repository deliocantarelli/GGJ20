
using System;
using GGJ20.Enemy;
using UnityEngine;

namespace GGJ20.Spawner{
    [Serializable]
    public class WaveEnemy {
        public EnemySettings enemy;
        public Vector2Int quantity = new Vector2Int(1, 1);
        public float probability = -1;
        public float startToSpawnTime = 0;
    }
}