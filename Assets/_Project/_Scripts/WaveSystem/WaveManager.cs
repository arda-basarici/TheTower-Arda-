using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class WaveManager
    {
        public static List<WaveData> WaveDataList { get; private set; }
        public static int CurrentWave { get; private set; }

        private static EnemySpawner _enemySpawner;
        private static GameObject _spawnerPrefab;

        public static void Initialize(IWaveDataLoader waveDataLoader,GameObject spawnerPrefabReference)
        {

            WaveDataList = waveDataLoader.LoadWaveDataAndReturn();
            Debug.Log("WaveDataList.Count: " + WaveDataList.Count);
            WaveDataList.ForEach(wave => {
                wave.GenerateEnemiesToSpawn();
            });
            CurrentWave = 1;
            _spawnerPrefab = spawnerPrefabReference;
            StartCurrentWave();
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
            if (WaveDataList == null || CurrentWave < WaveDataList.Count - 1)
            {
                Debug.LogError("Invalid wave data or out of bounds wave index");
                return null;
            }
            return WaveDataList[CurrentWave -1];
        }

        public static void NextWave()
        {
            if (WaveDataList == null || CurrentWave + 1 < WaveDataList.Count - 1)
            {
                Debug.LogError("No more waves");
                return;
            }
            CurrentWave++;
        }

        public static void ResetWaves()
        {
            CurrentWave = 1;
        }
    }
}