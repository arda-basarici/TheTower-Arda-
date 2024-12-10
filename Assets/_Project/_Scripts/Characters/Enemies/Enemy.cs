using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {   

        public EnemyType enemyType;
        public int inGameCurrReward;
        public int persistentCurrReward;
        protected void Start()
        {
            EnemyManager.AddEnemy(this);
        }

        protected void OnDestroy()
        {
            EnemyManager.RemoveEnemy(this);
        }

        public void OnDeath()
        {
            Wallet.AddInGameCurrency(inGameCurrReward);
            Wallet.AddPersistentCurrency(persistentCurrReward);
        }

    }
}