using UnityEngine;

namespace Game
{
    public class SelectTargetAndFire : MonoBehaviour, IStatObserver
    {
        [SerializeField]
        private Rigidbody2D projectile; 
        private float fireRate;
        private float range;
        private float damage;
        private Transform target;
        private float nextFireTime = 0f;

        
        private StatManager statManager;

        #region Initialization
        protected void Awake()
        {
            statManager = GetComponent<StatManager>();


        }

        protected void Start()
        {
            fireRate = statManager.GetCurrentValue(StatType.FireRate);
            range = statManager.GetCurrentValue(StatType.Range);
            damage = statManager.GetCurrentValue(StatType.Damage);
        }
        #endregion

        #region Observer methods
        protected void OnEnable()
        {
            statManager.RegisterObserver(StatType.FireRate, this);
            statManager.RegisterObserver(StatType.Range, this);
            statManager.RegisterObserver(StatType.Damage, this);
        }

        protected void OnDisable()
        {
            statManager.UnregisterObserver(StatType.FireRate, this);
            statManager.UnregisterObserver(StatType.Range, this);
            statManager.UnregisterObserver(StatType.Damage, this);
        }

        public void OnStatChange(StatType type,float value)
        {
            switch (type)
            {
                case StatType.FireRate:
                    fireRate = value;
                    break;
                case StatType.Range:
                    range = value;
                    break;
                case StatType.Damage:
                    damage = value;
                    break;
                default:
                    Debug.LogError($"Unhandled StatType: {type}");
                    break;
            }
        }
        #endregion

        private void SelectTarget()
        {
            var closestEnemy = EnemyManager.GetClosestEnemy(transform.position);
            if (closestEnemy != null && range >= Vector2.Distance(transform.position, closestEnemy.transform.position))
            {
                target = closestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }


        #region In game Logic 
        private void Fire()
        {
            if (Time.time >= nextFireTime)
            {   
                if (target == null) return;
                GameObject gameObject = PoolManager.Instantiate(projectile.gameObject, transform.position, Quaternion.identity);
                Rigidbody2D projectileInstance = gameObject.GetComponent<Rigidbody2D>();
                projectileInstance.gameObject.GetComponent<StatManager>().SetStat(StatType.Damage, damage);
                Vector2 direction = (target.position - transform.position).normalized;
                projectileInstance.linearVelocity = direction * 10f;
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        protected void Update()
        {
            SelectTarget();
            Fire();    
        }
        #endregion
    }
}