using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(StatManager))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour, IStatObserver, IPoolable
    {
        //private Transform target;
        private float speed = 0f;
        private Rigidbody2D rb;


        protected void Awake()
        {       
            rb = GetComponent<Rigidbody2D>();
            speed = gameObject.GetComponent<StatManager>().GetCurrentValue(StatType.Speed);
        }

        protected void OnEnable()
        {
            gameObject.GetComponent<StatManager>().RegisterObserver(StatType.Speed, this);
        }

        protected void OnDisable()
        {
            gameObject.GetComponent<StatManager>().UnregisterObserver(StatType.Speed,this);
        }

        private void Move()
        {
            Vector2 direction = Vector2.zero - (Vector2)transform.position;
            rb.linearVelocity = direction.normalized * speed;

        }

        public void OnStatChange(StatType type, float value)
        {
            if (type == StatType.Speed)
            {
                speed = value;
            }
        }


        protected void Update()
        {
            Move();
        }
    }
}