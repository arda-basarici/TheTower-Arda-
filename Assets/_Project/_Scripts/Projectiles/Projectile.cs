using UnityEngine;

namespace Game
{
    public class Projectile : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        void OnTriggerEnter2D(Collider2D collision) 
           
        {                   
                collision.gameObject.GetComponent<Damagable>().TakeDamage(10);
                Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}