using UnityEditor;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public StatData stats;
        public GameObject prefab;
    }
}