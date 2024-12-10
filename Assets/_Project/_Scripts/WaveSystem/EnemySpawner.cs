using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        private List<EnemyData> enemyData; 
        [SerializeField] private Transform spawnParent;         

        private Coroutine spawnRoutine;

        public void Awake()
        {
            enemyData = new List<EnemyData>(AssetLoader.LoadAll<EnemyData>(ResourcePaths.EnemyData));
        }
        

        public void StartWave(List<SpawnInfo> enemiesToSpawn)
        {
            if (spawnRoutine != null)
            {
                StopCoroutine(spawnRoutine);
            }
            spawnRoutine = StartCoroutine(SpawnEnemies(enemiesToSpawn));
        }

        private IEnumerator SpawnEnemies(List<SpawnInfo> enemiesToSpawn)
        {
            foreach (var spawnInfo in enemiesToSpawn)
            {
                SpawnEnemy(spawnInfo);
                yield return new WaitForSeconds(spawnInfo.SpawnTime);
            }

            Debug.Log("Wave complete!");
        }

        private void SpawnEnemy(SpawnInfo spawnInfo)
        {
            
            EnemyData enemyDatum = enemyData.Find(e => e.enemyName == spawnInfo.EnemyType.ToString());
            if (enemyDatum == null)
            {
                Debug.LogError($"EnemyData not found for type: {spawnInfo.EnemyType}");
                return;
            }

            GameObject enemyObject = Instantiate(enemyDatum.prefab, spawnInfo.Position, Quaternion.identity, spawnParent);
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            enemy.enemyType = spawnInfo.EnemyType;
            enemy.inGameCurrReward = enemyDatum.inGameCurrReward;
            enemy.persistentCurrReward = enemyDatum.persistentCurrReward;

        }
    }
}
