
using System;
using System.Collections.Generic;
using GGJ20.Enemy;
using GGJ20.Game;
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

        [SerializeField]
        private SpawnerSettings config;

        private WaveEnemy[] enemiesConfig;


        private float currenDelay;
        private float currentTimeToWait;
        private bool started;
        [Inject]
        private BattleSceneController sceneController;

        [Inject]
        public void Init(LevelSettings settings) {
            enemiesConfig = settings.enemies;
            currentTimeToWait = config.startSeconds;

            sceneController.BattleOver += BattleOver;
        }

        private void BattleOver(GameResult gameResult)
        {
            enabled = false;
        }

        private Dictionary<EnemySettings, int> GetWave(WaveEnemy[] enemies) {
            
            return GetFromPercentage(enemies);
            
        }
        private Dictionary<EnemySettings, int> GetFromPercentage(WaveEnemy[] enemies) {
            float max = 0;
            foreach(WaveEnemy enemy in enemies) {
                max += enemy.probability;
            }

            Dictionary<EnemySettings, int> spawns = new Dictionary<EnemySettings, int>();

            float random = UnityEngine.Random.Range(0, max);
            float amount = 0;
            foreach(WaveEnemy enemy in enemies) {
                if(random <= amount + enemy.probability) {
                    if(!spawns.ContainsKey(enemy.enemy)) {
                        spawns.Add(enemy.enemy, 1);
                    } else {
                        spawns[enemy.enemy] += UnityEngine.Random.Range(enemy.quantity.x, enemy.quantity.y);
                    }
                }
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
            Dictionary<EnemySettings, int> toSpawn = GetWave(enemiesConfig);

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