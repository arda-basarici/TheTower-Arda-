using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public static class WaveManager
    {
        public static int CurrentWave { get; private set; }
        public static int CurrentTier { get; private set; }

        private static EnemySpawner _enemySpawner;
        private static GameObject _spawnerPrefab;

        private static float waveTimer = 0f;

        public static void Initialize(GameObject spawnerPrefabReference)
        {
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
            _enemySpawner.StartWave(CurrentWave, CurrentTier);
        }

        public static void NextWave()
        {
            CurrentWave++;
        }

        public static void ResetWaves()
        {
            CurrentWave = 1;
        }
    }
}