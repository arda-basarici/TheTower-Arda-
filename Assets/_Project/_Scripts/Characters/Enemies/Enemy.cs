using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {   

        public EnemyType enemyType;
        protected void Start()
        {
            EnemyManager.AddEnemy(this);
        }

        protected void OnDestroy()
        {
            EnemyManager.RemoveEnemy(this);
        }
    
    }
}