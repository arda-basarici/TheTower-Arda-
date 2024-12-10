using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public int inGameCurrReward;
        public int persistentCurrReward;
        public StatData stats;
        public GameObject prefab;
        
    }
}