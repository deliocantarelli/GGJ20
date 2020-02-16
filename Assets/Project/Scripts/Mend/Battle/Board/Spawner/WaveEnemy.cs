
using PointNSheep.Mend.Battle;
using System;
using UnityEngine;

namespace PointNSheep.Mend.Battle
{
    [Serializable]
    public class WaveEnemy {
        public EnemySettings enemy;
        public Vector2Int quantity = new Vector2Int(1, 1);
        public float startToSpawnTime = 0;
        public float spawnInterval = 10;
        [NonSerialized]
        public float currentWait = 0;
        [NonSerialized]
        public bool started = false;
    }
}