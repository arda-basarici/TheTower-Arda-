using System;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Game
{
    [Serializable]
    public class EnemyBulk
    {
        [HorizontalGroup("Enemy Type")]
        [LabelWidth(80)]
        [SerializeField]
        private EnemyType _enemyType;

        [HorizontalGroup("Amount")]
        [SerializeField]
        private int _minAmount;
        [SerializeField]    
        private int _maxAmount;

        [HorizontalGroup("Spawn Time")]
        [SerializeField]
        private float _minSpawnTime;
        [SerializeField]
        private float _maxSpawnTime;


        public List<SpawnInfo> EnemiesToSpawn { get; private set; }


        private bool IsValid()
        {
            return _minAmount > 0 && _maxAmount > 0 && _minAmount <= _maxAmount && _minSpawnTime > 0 && _maxSpawnTime > 0 && _minSpawnTime <= _maxSpawnTime;
        }

        public void GenerateEnemiesToSpawn()
        {
            if (!IsValid())
            {
                Debug.LogWarning("EnemyBulk is not valid");
                return;
            }
            EnemiesToSpawn = new List<SpawnInfo>();
            var amount = UnityEngine.Random.Range(_minAmount, _maxAmount);
            for (int i = 0; i < amount; i++)
            {
                var spawnTime = UnityEngine.Random.Range(_minSpawnTime, _maxSpawnTime);
                EnemiesToSpawn.Add(new SpawnInfo(_enemyType, GetRandomPosition(), spawnTime));
            }
        }

        private Vector3 GetRandomPosition()
        {
            var angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
            var position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * Constants.SpawnRadius;
            return position;
        }
    }
}