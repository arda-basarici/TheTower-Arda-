using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "StatData", menuName = "Scriptable Objects/StatData")]
    public class StatData : ScriptableObject
    {
        public float health;
        public float damage;
        public float speed;
        public float fireRate;
    }
}