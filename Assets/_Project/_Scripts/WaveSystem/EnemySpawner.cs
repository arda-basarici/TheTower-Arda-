using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField] 
        private Transform spawnParent;         

        private Coroutine spawnRoutine;

        public void Awake()
        {
            spawnParent = ReferenceResolver.Get<Transform>(ReferenceKeys.EnemyParent);
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
        }

        private void SpawnEnemy(SpawnInfo spawnInfo)
        {   
            GameObject enemyObject = PoolManager.Instantiate(spawnInfo.EnemyData.prefab, spawnInfo.Position, Quaternion.identity, spawnParent);
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            
            enemy.moneyReward = spawnInfo.EnemyData.moneyReward;
            enemy.tokenReward = spawnInfo.EnemyData.tokenReward;
        }
    }
}
