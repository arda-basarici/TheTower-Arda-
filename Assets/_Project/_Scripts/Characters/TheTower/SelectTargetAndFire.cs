using UnityEngine;

namespace Game
{
    public class SelectTargetAndFire : MonoBehaviour, IGamePlayStatePlayingUpdateListener
    {
        [SerializeField]
        private Rigidbody2D projectile;
        private float fireRate;
        private float range;
        private float damage;
        private Transform target;
        private float nextFireTime = 0f;

        protected void OnEnable()
        {
            EventSystem.Get<StatEventManager>(StatType.FireRate.ToString() + GetComponent<Identifier>().ID).RegisterStateObserver(OnFireRateChange);
            EventSystem.Get<StatEventManager>(StatType.Range.ToString() + GetComponent<Identifier>().ID).RegisterStateObserver(OnRangeChange);
            EventSystem.Get<StatEventManager>(StatType.Damage.ToString() + GetComponent<Identifier>().ID).RegisterStateObserver(OnDamageChange);
            LifecycleManager.Register(typeof(IGamePlayStatePlayingUpdateListener), this);
        }

        protected void OnDisable()
        {
            EventSystem.Get<StatEventManager>(StatType.FireRate.ToString() + GetComponent<Identifier>().ID).UnregisterStateObserver(OnFireRateChange);
            EventSystem.Get<StatEventManager>(StatType.Range.ToString() + GetComponent<Identifier>().ID).UnregisterStateObserver(OnRangeChange);
            EventSystem.Get<StatEventManager>(StatType.Damage.ToString() + GetComponent<Identifier>().ID).UnregisterStateObserver(OnDamageChange);
            LifecycleManager.Unregister(typeof(IGamePlayStatePlayingUpdateListener), this);
        }

        public void OnFireRateChange(StatState statstate)
        {
            fireRate = statstate.CurrentValue;
        }

        public void OnRangeChange(StatState statstate)
        {
            range = statstate.CurrentValue;
        }

        public void OnDamageChange(StatState statstate)
        {
            damage = statstate.CurrentValue;
        }
  
        private void SelectTarget()
        {
            Enemy closestEnemy = EnemyManager.GetClosestEnemy(transform.position);
            if (closestEnemy != null && range >= Vector2.Distance(transform.position, closestEnemy.transform.position))
            {
                target = closestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }

        private void Fire()
        {
            if (Time.time >= nextFireTime)
            {
                if (target == null) return;
                GameObject gameObject = PoolManager.Instantiate(projectile.gameObject, transform.position, Quaternion.identity);
                Rigidbody2D projectileInstance = gameObject.GetComponent<Rigidbody2D>();
                projectileInstance.gameObject.GetComponent<StatManager>().SetStat(StatType.Damage, damage);
                Vector2 direction = (target.position - transform.position).normalized;
                projectileInstance.linearVelocity = direction * 8.5f;
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        public void GamePlayStatePlayingUpdate()
        {
            SelectTarget();
            Fire();
        }

    }
}