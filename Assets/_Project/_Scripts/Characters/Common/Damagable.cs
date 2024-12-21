using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(StatManager))]
    public class Damagable : MonoBehaviour
    {
        private StatManager statManager;
        private Enemy enemy;
        private TowerManager towerManager;


        protected void Awake()
        {
            statManager = GetComponent<StatManager>();
            enemy = GetComponent<Enemy>();
            towerManager = GetComponent<TowerManager>();
        }

        public void TakeDamage(float damage)
        {
            statManager.DecreaseStat(StatType.Health, damage);

            if (statManager.GetCurrentValue(StatType.Health) <= 0)
            {
                Die();
            }
        }

        

        protected void Die()
        {
            if (enemy != null)
            {
                PoolManager.ReturnToPool(gameObject);
                enemy.OnDeath();
            }
            else if(towerManager != null)
            {
                towerManager.OnDeath();
            }
        }
    }
}