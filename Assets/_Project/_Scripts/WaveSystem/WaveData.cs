using System.Collections.Generic;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class WaveData
    {
        public int WaveNumber { get; private set; }
        public List<EnemyBulk> EnemyBulks { get; private set; }
        public List<SpawnInfo> EnemiesToSpawn { get; private set; }

        public WaveData(int waveNumber, List<EnemyBulk> enemyBulks, List<SpawnInfo> enemiesToSpawn)
        {
            WaveNumber = waveNumber;
            EnemyBulks = enemyBulks;
            EnemiesToSpawn = enemiesToSpawn;
        }
    }
}