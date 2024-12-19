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



        public void OnDeath()
        {
            Wallet.AddInGameCurrency(moneyReward);
            Wallet.AddPersistentCurrency(tokenReward);
        }

    }
}