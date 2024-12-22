using UnityEngine;

namespace Game
{
    public class TowerManager : MonoBehaviour
    {

        public void OnDeath()
        {
            Wallet.Save();
        }
    
    }

    
}
