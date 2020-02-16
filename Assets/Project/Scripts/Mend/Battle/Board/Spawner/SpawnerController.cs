
using PointNSheep.Mend.Battle;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PointNSheep.Mend.Battle
{
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
        private float ellapsedTime = 0;

        [Inject]
        public void Init(LevelSettings settings, BattleSceneController scene) {
            enemiesConfig = settings.enemies;
            currentTimeToWait = config.startSeconds;
            scene.BattleOver += BattleOver;
        }

        private void BattleOver(GameResult obj)
        {
            gameObject.SetActive(false);
        }

        private Dictionary<EnemySettings, int> SpawnEnemies(WaveEnemy[] enemies) {
            Dictionary<EnemySettings, int> spawns = new Dictionary<EnemySettings, int>();

            foreach(WaveEnemy enemy in enemies) {
                if(ellapsedTime < enemy.startToSpawnTime) {
                    continue;
                } else if (!enemy.started) {
                    enemy.currentWait = float.PositiveInfinity;
                    enemy.started = true;
                }
                enemy.currentWait += Time.deltaTime;
                if(enemy.currentWait > enemy.spawnInterval) {
                    enemy.currentWait = 0;
                    int amount = UnityEngine.Random.Range(enemy.quantity.x, enemy.quantity.y);
                    spawns.Add(enemy.enemy, amount);
                }
            }

            return spawns;
        }


        void Update()
        {
            if(config == null) {
                return;
            }
            if(currentTimeToWait > 0) {
                currentTimeToWait -= Time.deltaTime;
                return;
            }
            ellapsedTime += Time.deltaTime;

            Dictionary<EnemySettings, int> spawns = SpawnEnemies(enemiesConfig);
            Spawn(spawns);
        }

        private void Spawn(Dictionary<EnemySettings, int> toSpawn)
        {
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