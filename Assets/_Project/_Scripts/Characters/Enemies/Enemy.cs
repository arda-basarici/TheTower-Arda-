using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour, IPoolable
    {

        public EnemyType enemyType;
        public int inGameCurrReward;
        public int persistentCurrReward;


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



        public void OnDeath()
        {
            Wallet.AddInGameCurrency(inGameCurrReward);
            Wallet.AddPersistentCurrency(persistentCurrReward);
        }

    }
}