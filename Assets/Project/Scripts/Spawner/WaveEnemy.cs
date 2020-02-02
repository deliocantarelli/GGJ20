
using System;
using GGJ20.Enemy;

namespace GGJ20.Spawner{
    [Serializable]
    public class WaveEnemy {
        public EnemySettings enemy;
        public int quantity = -1;
        public float probability = -1;
    }
}