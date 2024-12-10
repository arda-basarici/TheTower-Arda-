using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(StatManager))]
    public class Damagable : MonoBehaviour
    {
        private StatManager statManager;
        private Enemy enemy;


        protected void Awake()
        {
            statManager = GetComponent<StatManager>();
            enemy = GetComponent<Enemy>();
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
            Destroy(gameObject);
            if (enemy != null)
            {
                enemy.OnDeath();
            }
        }
    }
}