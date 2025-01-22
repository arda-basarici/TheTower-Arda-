using System;
using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour, IPoolable
    {


        private int wave;
        private int tier;

        private float moneyReward = 0;
        private float tokenReward = 0;

        private EnemyType type = EnemyType.Basic;

        public void EnemyInit(EnemyType type, int wave, int tier, double baseHealth, double baseDamage)
        {
            this.type = type;
            this.wave = wave;
            this.tier = tier;
            SetEnemy((float)Math.Min(baseHealth, float.MaxValue), (float)Math.Min(baseDamage, float.MaxValue));
        }

        public void OnSpawn()
        {
            EnemyManager.AddEnemy(this);
            EventSystem.Get<DeathEventManager>(EventManagerId.death + GetComponent<Identifier>().ID).RegisterObserver(OnDeath);
        }

        public void OnReturn()
        {
            EventSystem.Get<DeathEventManager>(EventManagerId.death + GetComponent<Identifier>().ID).UnregisterObserver(OnDeath);
            EnemyManager.RemoveEnemy(this);
        }

        public void ResetForPooling()
        {
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision != null)
            {
                TowerManager towerManager = collision.gameObject.GetComponent<TowerManager>();
                if (towerManager != null)
                {
                    towerManager.OnDeath();
                }
            }
        }
        public void OnDeath(object sender)
        {
            PoolManager.ReturnToPool(gameObject);
            Wallet.AddMoney(moneyReward);
            Wallet.AddToken(tokenReward);
        }

        private void SetEnemy(float baseHealth, float baseDamage)
        {
            StatManager statManager = GetComponent<StatManager>();
            if (type == EnemyType.Basic)
            {
                statManager.SetStat(StatType.Health, baseHealth);
                statManager.SetStat(StatType.Damage, baseDamage);
                statManager.SetStat(StatType.Speed, Constants.ENEMYBASICSPEED);
                moneyReward = 1;
                tokenReward = 0;
            }
            else if (type == EnemyType.Tank)
            {
                statManager.SetStat(StatType.Health, baseHealth * 5);
                statManager.SetStat(StatType.Damage, baseDamage);
                statManager.SetStat(StatType.Speed, Constants.ENEMYBASICSPEED / 2);
                moneyReward = 0;
                tokenReward = 5;
            }
            else if (type == EnemyType.Fast)
            {
                statManager.SetStat(StatType.Health, baseHealth);
                statManager.SetStat(StatType.Damage, baseDamage);
                statManager.SetStat(StatType.Speed, Constants.ENEMYBASICSPEED * 2);
                moneyReward = 0;
                tokenReward = 2;
            }
            else if (type == EnemyType.Boss)
            {
                statManager.SetStat(StatType.Health, baseHealth);
                statManager.SetStat(StatType.Damage, baseDamage);
                statManager.SetStat(StatType.Speed, Constants.ENEMYBASICSPEED);
                moneyReward = 0;
                tokenReward = 2;
            }
            else if (type == EnemyType.Ranged)
            {
                statManager.SetStat(StatType.Health, baseHealth * 20);
                statManager.SetStat(StatType.Damage, baseDamage);
                statManager.SetStat(StatType.Speed, Constants.ENEMYBASICSPEED * (3/10));
                moneyReward = 0;
                tokenReward = 5;
            }
            else if(type == EnemyType.Protector)
            {
                statManager.SetStat(StatType.Health, baseHealth);
                statManager.SetStat(StatType.Damage, baseDamage);
                statManager.SetStat(StatType.Speed, Constants.ENEMYBASICSPEED);
                moneyReward = 0;
                tokenReward = 3;
            }

        }
    }
}