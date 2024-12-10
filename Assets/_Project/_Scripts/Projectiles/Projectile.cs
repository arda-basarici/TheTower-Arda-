using UnityEngine;

namespace Game
{
    public class Projectile : MonoBehaviour
    {
        protected void OnTriggerEnter2D(Collider2D collision)

        {
            collision.gameObject.GetComponent<Damagable>().TakeDamage(gameObject.GetComponent<StatManager>().GetCurrentValue(StatType.Damage));
            Destroy(gameObject);
        }
    }
}