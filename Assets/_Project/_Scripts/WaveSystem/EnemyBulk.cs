using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class EnemyBulk
    {
        public EnemyType EnemyType { get; private set; }
        public (int min, int max) AmountRange { get; private set; }
        public (float min, float max) SpawnTimeRange { get; private set; }

        public EnemyBulk(EnemyType enemyType, (int min, int max) amountRange, (float min, float max) spawnTimeRange)
        {
            EnemyType = enemyType;
            AmountRange = amountRange;
            SpawnTimeRange = spawnTimeRange;
        }
    }
}