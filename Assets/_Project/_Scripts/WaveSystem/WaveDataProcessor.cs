using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class WaveDataProcessor
    {
        private readonly float _spawnRadius = 10;

        public List<SpawnInfo> GenerateCircularSpawnInfo(List<EnemyBulk> enemyBulks)
        {
            var spawnInfos = new List<SpawnInfo>();
            foreach (var enemyBulk in enemyBulks)
            {
                var amount = UnityEngine.Random.Range(enemyBulk.AmountRange.min, enemyBulk.AmountRange.max);
                var spawnTime = UnityEngine.Random.Range(enemyBulk.SpawnTimeRange.min, enemyBulk.SpawnTimeRange.max);
                var angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
                var position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * _spawnRadius;
                for (int i = 0; i < amount; i++)
                {
                    spawnInfos.Add(new SpawnInfo(enemyBulk.EnemyType, position, spawnTime));
                }
            }
            return spawnInfos;
        }
    }
}