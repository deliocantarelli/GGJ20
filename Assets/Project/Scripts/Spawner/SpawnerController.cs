
using System;
using System.Collections.Generic;
using GGJ20.Enemy;
using UnityEngine;
using Zenject;

namespace GGJ20.Spawner {
    public class SpawnConfig {
        public EnemySettings enemy;
        public int amount;
    }
    public class SpawnerController : MonoBehaviour {
        [Inject]
        LevelSettings levelSettings;
        [Inject]
        EnemyController.Factory factory;
        private WaveEnemy[] enemiesConfig;
        private Vector2Int maxQuantity;
        private SpawnerSettings config;


        private float currenDelay;
        private float currentTimeToWait;
        private bool started;

        void Start()
        {
            levelSettings.GetSpawnerConfig(this);
        }
        public void Setup(SpawnerSettings spawnerSettings, LevelSettings settings) {
            enemiesConfig = settings.enemies;
            maxQuantity = settings.enemiesQuantity;
            config = spawnerSettings;
            currentTimeToWait = config.startSeconds;
        }

        private Dictionary<EnemySettings, int> GetWave(bool usePercentage, WaveEnemy[] enemies, Vector2Int quantity) {
            if(usePercentage) {
                return GetFromPercentage(enemies, quantity);
            } else {
                return GetFromAbsolute(enemies, quantity);
            }
            
        }
        private Dictionary<EnemySettings, int> GetFromPercentage(WaveEnemy[] enemies, Vector2Int quantity) {
            float max = 0;
            foreach(WaveEnemy enemy in enemies) {
                max += enemy.probability;
            }
            int maxAmount = UnityEngine.Random.Range(quantity.x, quantity.y);

            Dictionary<EnemySettings, int> spawns = new Dictionary<EnemySettings, int>();

            for(int i = 0; i < maxAmount; i ++) {
                float random = UnityEngine.Random.Range(0, max);
                float amount = 0;
                foreach(WaveEnemy enemy in enemies) {
                    if(random <= amount + enemy.probability) {
                        if(!spawns.ContainsKey(enemy.enemy)) {
                            spawns.Add(enemy.enemy, 1);
                        } else {
                            spawns[enemy.enemy] ++;
                        }
                    }
                }
            }
            return spawns;
        }

        private Dictionary<EnemySettings, int> GetFromAbsolute(WaveEnemy[] enemies, Vector2Int quantity) {
            Dictionary<EnemySettings, int> spawns = new Dictionary<EnemySettings, int>();
            foreach(WaveEnemy enemy in enemies) {
                spawns.Add(enemy.enemy, enemy.quantity);
            }
            return spawns;
        }


        void Update()
        {
            if(config == null) {
                return;
            }
            currenDelay += Time.deltaTime;
            if(currenDelay > currentTimeToWait) {
                Spawn();
            }
        }

        private void Spawn()
        {
            currenDelay = 0;
            currentTimeToWait = UnityEngine.Random.Range(levelSettings.waitTime.x, levelSettings.waitTime.y);
            Dictionary<EnemySettings, int> toSpawn = GetWave(levelSettings.usePercentage, enemiesConfig, maxQuantity);

            foreach(EnemySettings enemy in toSpawn.Keys) {
                int amount = toSpawn[enemy];

                for(int i = 0; i < amount; i ++) {
                    SpawnEnemy(enemy);
                }
            }
        }

        private void SpawnEnemy(EnemySettings enemy) {
            EnemyController enemyObject = factory.Create(enemy.prefab);
            enemyObject.transform.position = transform.position;
            enemyObject.Setup(enemy);
        }
    }
}