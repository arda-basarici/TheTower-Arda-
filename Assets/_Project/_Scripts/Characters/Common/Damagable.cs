using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace Game
{
    [RequireComponent(typeof(StatManager))]
    public class Damagable : MonoBehaviour
    {
        private StatManager statManager;

        private void Awake()
        {
            statManager = GetComponent<StatManager>();
        }

        public void TakeDamage(float damage)
        {
            statManager.DecreaseStat(StatType.Health, damage);

            if (statManager.GetStat(StatType.Health).CurrentValue <= 0)
            {
                Die();
            }
        }

        protected void Die()
        {
            Destroy(gameObject);
        }
    }
}