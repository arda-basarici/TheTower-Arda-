using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour, IPoolable
    {
        public int moneyReward;
        public int tokenReward;


        public void OnSpawn()
        {
            EnemyManager.AddEnemy(this);
        }

        public void OnReturn()
        {
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
        public void OnDeath()
        {
            Wallet.AddMoney(moneyReward);
            Wallet.AddToken(tokenReward);
        }

    }
}