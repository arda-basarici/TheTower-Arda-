using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Game
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
    public class WaveData : ScriptableObject
    {
        [BoxGroup("Wave Information")]
        [SerializeField]
        private int _waveNumber;


        [BoxGroup("Enemy Bulks")]
        [TableList]
        [SerializeField]
        private List<EnemyBulk> _enemyBulks = new List<EnemyBulk>();


        public List<SpawnInfo> EnemiesToSpawn;

        public int WaveNumber => _waveNumber;

        public void AddEnemyBulk(EnemyBulk enemyBulk)
        {
            if (_enemyBulks == null)
            {
                Debug.LogError("EnemyBulks list is null");
                return;
            }
            _enemyBulks.Add(enemyBulk);
        }

        public bool IsValid()
        {
            return _enemyBulks.Count > 0 && _waveNumber > 0;
        }

        public void GenerateEnemiesToSpawn()
        {
            if (!IsValid())
            {
                Debug.LogWarning("WaveData is not valid");
                return;
            }
            EnemiesToSpawn = new List<SpawnInfo>();
            foreach (var enemyBulk in _enemyBulks)
            {
                enemyBulk.GenerateEnemiesToSpawn();
                EnemiesToSpawn.AddRange(enemyBulk.EnemiesToSpawn);
            }


        }


    }
}