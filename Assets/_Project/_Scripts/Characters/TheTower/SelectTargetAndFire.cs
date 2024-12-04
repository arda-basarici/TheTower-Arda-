using UnityEngine;

namespace Game
{
    public class SelectTargetAndFire : MonoBehaviour, IStatObserver
    {
        [SerializeField]
        private Rigidbody2D projectile; 
        private float fireRate;
        private Transform target;
        private float nextFireTime = 0f;

        
        private StatManager statManager;

        protected void Awake()
        {
            statManager = GetComponent<StatManager>();
            fireRate = statManager.GetCurrentValue(StatType.FireRate); 
        }

        protected void OnEnable()
        {
            statManager.RegisterObserver(StatType.FireRate, this);
        }

        protected void OnDisable()
        {
            statManager.UnregisterObserver(StatType.FireRate, this);
        }

        public void OnStatChange(float value)
        {
            fireRate = value;
        }

        private void SelectTarget()
        {
            var closestEnemy = EnemyManager.GetClosestEnemy(transform.position);
            if (closestEnemy != null)
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
                Rigidbody2D projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);
                projectileInstance.gameObject.GetComponent<StatManager>().SetStat(StatType.Damage, statManager.GetCurrentValue(StatType.Damage));
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
    }
}