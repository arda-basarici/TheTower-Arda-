using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(StatManager))]
    public class Damagable : MonoBehaviour
    {
        private StatManager statManager;


        protected void Awake()
        {
            statManager = GetComponent<StatManager>();
        }

        public void TakeDamage(float damage)
        {
            statManager.DecreaseStat(StatType.Health, damage);
        }
    }
}