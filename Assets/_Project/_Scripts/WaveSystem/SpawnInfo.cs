using UnityEngine;

namespace Game
{
    public struct SpawnInfo
    {
        public EnemyData EnemyData { get; private set; }
        public Vector3 Position { get; private set; }

        public float SpawnTime { get; private set; }

        public SpawnInfo(EnemyData enemyData, Vector3 position, float spawnTime)
        {
            EnemyData = enemyData;
            Position = position;
            SpawnTime = spawnTime;
        }
    }
}