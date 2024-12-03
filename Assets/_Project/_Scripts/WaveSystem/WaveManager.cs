using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public static class WaveManager
    {

        public static List<WaveData> WaveData { get; private set; }
        public static int CurrentWave { get; private set; }

        private static EnemySpawner _enemySpawner;
        private static GameObject _spawnerPrefab;

        public static void Initialize(IWaveDataLoader waveDataLoader, WaveDataProcessor waveDataProcessor, GameObject spawnerPrefabReference)
        {
            var rawWaveData = waveDataLoader.LoadWaveData();
            WaveData = new List<WaveData>();

            foreach (var rawData in rawWaveData)
            {
                var waveNumber = rawData.WaveNumber;
                var enemyBulks = rawData.EnemyBulks;
                var enemiesToSpawn = waveDataProcessor.GenerateCircularSpawnInfo(enemyBulks);
                WaveData.Add(new WaveData(waveNumber, enemyBulks, enemiesToSpawn));
            }
            CurrentWave = 0;
            _spawnerPrefab = spawnerPrefabReference;    
        }   

        private static void CheckSpawner()
        {
            if (_enemySpawner == null)
            {
                _enemySpawner = GameObject.Instantiate(_spawnerPrefab).GetComponent<EnemySpawner>();
            }
        }   

        public static void StartCurrentWave()
        {
            CheckSpawner();
            var waveData = GetCurrentWaveData();
            _enemySpawner.StartWave(waveData.EnemiesToSpawn);
        }

        public static WaveData GetCurrentWaveData()
        {
            if (WaveData == null || CurrentWave >= WaveData.Count - 1)
            {
                Debug.LogError("Invalid wave data or out of bounds wave index");
                return null;
            }
            return WaveData[CurrentWave];
        }

        public static void NextWave()
        {
            if (WaveData == null || CurrentWave >= WaveData.Count - 1)
            {
                Debug.LogError("No more waves");
                return;
            }
            CurrentWave++;
        }

        public static void ResetWaves()
        {
            CurrentWave = 0;
        }
    }
}