using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace Game
{
    [RequireComponent(typeof(StatManager))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour, IStatObserver
    {
        //private Transform target;
        private float speed = 0f;
        private Rigidbody2D rb;


        private void Awake()
        {       
            Debug.Log("EnemyMovement Awake");
            rb = GetComponent<Rigidbody2D>();
            speed = gameObject.GetComponent<StatManager>().GetStat(StatType.Speed).CurrentValue;
        }

        private void OnEnable()
        {
            gameObject.GetComponent<StatManager>().GetStat(StatType.Speed).RegisterObserver(this);
        }

        private void OnDisable()
        {
            gameObject.GetComponent<StatManager>().GetStat(StatType.Speed).UnregisterObserver(this);
        }

        private void Move()
        {
            Vector2 direction = Vector2.zero - (Vector2)transform.position;
            rb.linearVelocity = direction.normalized * speed;

        }

        public void OnStatChange(float value)
        {
            speed = value;
        }


        void Update()
        {
            Move();
        }
    }
}