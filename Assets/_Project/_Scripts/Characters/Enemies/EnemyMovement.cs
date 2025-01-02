using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour, IGamePlayStatePlayingUpdateListener
    {
        private float speed = 0f;
        private Rigidbody2D rb;


        protected void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        protected void OnEnable()
        { 
            EventSystem.Get<StatEventManager>(StatType.Speed.ToString() + GetComponent<Identifier>().ID).RegisterStateObserver(OnStatUpdated);
            LifecycleManager.Register(typeof(IGamePlayStatePlayingUpdateListener), this);
        }

        protected void OnDisable()
        {
            EventSystem.Get<StatEventManager>(StatType.Speed.ToString() + GetComponent<Identifier>().ID).UnregisterStateObserver(OnStatUpdated);
            LifecycleManager.Unregister(typeof(IGamePlayStatePlayingUpdateListener), this);
        }

        private void Move()
        {
            Vector2 direction = Vector2.zero - (Vector2)transform.position;
            rb.linearVelocity = direction.normalized * speed;
        }

        public void OnStatUpdated(StatState statState)
        {
            speed = statState.CurrentValue;
        }

        public void GamePlayStatePlayingUpdate()
        {
            Move();
        }

    }
}