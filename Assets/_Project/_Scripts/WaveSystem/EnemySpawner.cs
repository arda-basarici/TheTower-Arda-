using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField] 
        private Transform spawnParent;
        [SerializeField]
        private GameObject enemyPrefab;

        private Coroutine spawnRoutine;

        private const float spawnTime = 0.125f;

        private int tier = 1;
        private int wave = 1;

        private Dictionary<EnemyType, int> spawnRates = new Dictionary<EnemyType, int>();
        
        private int spawnRate = 10;

        private bool BossSpawned = false;
        private bool BossWave = false;

        private double baseHealth = 1;
        private double baseDamage = 1;

        public void Awake()
        {
            spawnParent = ReferenceResolver.Get<Transform>(ReferenceKeys.EnemyParent);
            enemyPrefab = AssetLoader.LoadAsset<GameObject>(ResourcePaths.EnemyPrefab);
        }


        public void StartWave(int waveNumber, int tierNumber)
        {

            tier = tierNumber;
            wave = waveNumber;
            BossSpawned = false;
            BossWave = waveNumber % 10 == 0;
            baseHealth = EnemyStatsCalculator.CalculateHealth(wave, tier);
            baseDamage = EnemyStatsCalculator.CalculateDamage(wave, tier);
            if (spawnRoutine != null)
            {
                StopCoroutine(spawnRoutine);
            }
            CalculateSpawnRates();
            spawnRoutine = StartCoroutine(SpawnEnemy());
        }

        public void StopWave()
        {
            if (spawnRoutine != null)
            {
                StopCoroutine(spawnRoutine);
            }
        }

        private IEnumerator SpawnEnemy()
        {
            bool b = false;
            while (true)
            {
                b = SpawnCheck();
                Debug.Log(b);
                if (b)
                {
                    Debug.Log("test");
                    GameObject enemyObject = PoolManager.Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity, spawnParent);
                    Enemy enemy = enemyObject.GetComponent<Enemy>();
                    enemy.EnemyInit(DecideEnemy(), wave, tier, baseHealth, baseDamage);
                   
                } 
                yield return new WaitForSeconds(spawnTime);
            }
        }

        private EnemyType DecideEnemy()
        {
            if (BossWave && !BossSpawned) { 
                return EnemyType.Boss;
            }
            float random = UnityEngine.Random.Range(0,101);
            float basic = spawnRates[EnemyType.Basic];
            float fast = spawnRates[EnemyType.Fast] + basic;
            float tank = spawnRates[EnemyType.Tank] + fast;
            float ranged = spawnRates[EnemyType.Ranged] + tank;
            if (random <= basic)
            {
                return EnemyType.Basic;
            }
            else if (random <= fast)
            {
                return EnemyType.Fast;
            }
            else if (random <= tank)
            {
                return EnemyType.Tank;
            }
            else if (random <= ranged) {
                return EnemyType.Ranged;
            }
            return EnemyType.Basic;
        }

        private bool SpawnCheck()
        {
            if (UnityEngine.Random.Range(0, 101) <= spawnRate)
            {
                return true;
            }
            return false;
        }

        private Vector3 GetRandomPosition()
        {
            var angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
            var position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * Constants.SPAWNRADIUS;
            return position;
        }

        private void CalculateSpawnRates()
        {
            spawnRates.Clear(); // Clear existing rates to avoid duplicates

            int fastRate, tankRate, rangedRate, protectorRate = 0;

            if (wave >= 6500)
            {
                fastRate = 24;
                tankRate = 22;
                rangedRate = 21;
                spawnRate = 56;
            }
            else if (wave >= 6000)
            {
                fastRate = 24;
                tankRate = 21;
                rangedRate = 20;
                spawnRate = 54;
            }
            else if (wave >= 5500)
            {
                fastRate = 23;
                tankRate = 21;
                rangedRate = 19;
                spawnRate = 52;
            }
            else if (wave >= 5000)
            {
                fastRate = 22;
                tankRate = 20;
                rangedRate = 19;
                spawnRate = 50;
            }
            else if (wave >= 4500)
            {
                fastRate = 21;
                tankRate = 20;
                rangedRate = 19;
                spawnRate = 49;
            }
            else if (wave >= 4000)
            {
                fastRate = 21;
                tankRate = 20;
                rangedRate = 18;
                spawnRate = 48;
            }
            else if (wave >= 3500)
            {
                fastRate = 20;
                tankRate = 19;
                rangedRate = 17;
                spawnRate = 46;
            }
            else if (wave >= 3000)
            {
                fastRate = 19;
                tankRate = 19;
                rangedRate = 16;
                spawnRate = 44;
            }
            else if (wave >= 2500)
            {
                fastRate = 18;
                tankRate = 18;
                rangedRate = 15;
                spawnRate = 42;
            }
            else if (wave >= 2000)
            {
                fastRate = 17;
                tankRate = 17;
                rangedRate = 14;
                spawnRate = 40;
            }
            else if (wave >= 1500)
            {
                fastRate = 15;
                tankRate = 16;
                rangedRate = 14;
                spawnRate = 39;
            }
            else if (wave >= 1250)
            {
                fastRate = 15;
                tankRate = 16;
                rangedRate = 11;
                spawnRate = 38;
            }
            else if (wave >= 1000)
            {
                fastRate = 14;
                tankRate = 15;
                rangedRate = 11;
                spawnRate = 37;
            }
            else if (wave >= 800)
            {
                fastRate = 13;
                tankRate = 14;
                rangedRate = 11;
                spawnRate = 36;
            }
            else if (wave >= 750)
            {
                fastRate = 13;
                tankRate = 14;
                rangedRate = 10;
                spawnRate = 34;
            }
            else if (wave >= 600)
            {
                fastRate = 13;
                tankRate = 14;
                rangedRate = 10;
                spawnRate = 34;
            }
            else if (wave >= 400)
            {
                fastRate = 13;
                tankRate = 13;
                rangedRate = 9;
                spawnRate = 32;
            }
            else if (wave >= 320)
            {
                fastRate = 12;
                tankRate = 13;
                rangedRate = 8;
                spawnRate = 30;
            }
            else if (wave >= 300)
            {
                fastRate = 12;
                tankRate = 13;
                rangedRate = 8;
                spawnRate = 30;
            }
            else if (wave >= 250)
            {
                fastRate = 12;
                tankRate = 12;
                rangedRate = 7;
                spawnRate = 28;
            }
            else if (wave >= 200)
            {
                fastRate = 11;
                tankRate = 11;
                rangedRate = 7;
                spawnRate = 26;
            }
            else if (wave >= 160)
            {
                fastRate = 11;
                tankRate = 10;
                rangedRate = 6;
                spawnRate = 24;
            }
            else if (wave >= 150)
            {
                fastRate = 11;
                tankRate = 10;
                rangedRate = 6;
                spawnRate = 24;
            }
            else if (wave >= 100)
            {
                fastRate = 10;
                tankRate = 9;
                rangedRate = 6;
                spawnRate = 22;
            }
            else if (wave >= 80)
            {
                fastRate = 10;
                tankRate = 8;
                rangedRate = 5;
                spawnRate = 20;
            }
            else if (wave >= 60)
            {
                fastRate = 9;
                tankRate = 8;
                rangedRate = 4;
                spawnRate = 19;
            }
            else if (wave >= 40)
            {
                fastRate = 8;
                tankRate = 7;
                rangedRate = 3;
                spawnRate = 17;
            }
            else if (wave >= 20)
            {
                fastRate = 7;
                tankRate = 6;
                rangedRate = 2;
                spawnRate = 15;
            }
            else if (wave >= 6)
            {
                fastRate = 6;
                tankRate = 4;
                rangedRate = 1;
                spawnRate = 13;
            }
            else if (wave >= 3)
            {
                fastRate = 5;
                tankRate = 2;
                rangedRate = 0;
                spawnRate = 11;
            }
            else
            {
                fastRate = 5;
                tankRate = 0;
                rangedRate = 0;
                spawnRate = 10;
            }

            if(tier == 1)
            {
                protectorRate = 0;
            }else
            {
                if(wave >= 750)
                {
                    protectorRate = 4;
                }else if(wave >= 320)
                {
                    protectorRate = 3;
                }else if(wave >= 160)
                {
                    protectorRate = 2;
                }else if (wave >= 80)
                {
                    protectorRate = 1;
                }
            }

            if (tier >= 12) protectorRate++;
            if (tier >= 13) protectorRate++;

            spawnRates.Add(EnemyType.Fast, fastRate);
            spawnRates.Add(EnemyType.Tank, tankRate);
            spawnRates.Add(EnemyType.Ranged, rangedRate);
            spawnRates.Add(EnemyType.Basic, 100 - fastRate - tankRate - rangedRate - protectorRate);
        }

    }
}
