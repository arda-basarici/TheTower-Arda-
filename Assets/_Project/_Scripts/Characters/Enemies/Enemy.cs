using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {   

        public EnemyType enemyType; 
        void Start()
        {
            EnemyManager.AddEnemy(this);
        }

        private void OnDestroy()
        {
            EnemyManager.RemoveEnemy(this);
        }
    
    }
}