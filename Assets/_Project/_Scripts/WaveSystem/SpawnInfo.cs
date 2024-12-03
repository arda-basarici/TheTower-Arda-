using UnityEngine;

namespace Game
{
    public class SpawnInfo
    {
        public EnemyType EnemyType { get; private set; }
        public Vector3 Position { get; private set; }

        public float SpawnTime { get; private set; }

        public SpawnInfo(EnemyType enemyType, Vector3 position, float spawnTime)
        {
            EnemyType = enemyType;
            Position = position;
            SpawnTime = spawnTime;
        }
    }
}