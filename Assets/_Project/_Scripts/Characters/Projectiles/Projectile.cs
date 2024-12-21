using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(StatManager))]
    public class Projectile : MonoBehaviour, IPoolable
    {
        public void OnSpawn()
        {

        }

        public void OnReturn()
        {
        }

        public void ResetForPooling()
        {
        }
        protected void OnTriggerEnter2D(Collider2D collision)

        {
            collision.gameObject.GetComponent<Damagable>().TakeDamage(gameObject.GetComponent<StatManager>().GetCurrentValue(StatType.Damage));
            //Destroy(gameObject);
            PoolManager.ReturnToPool(gameObject);

        }
    }
}