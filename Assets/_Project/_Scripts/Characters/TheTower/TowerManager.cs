using UnityEngine;

namespace Game
{
    public class TowerManager : MonoBehaviour
    {
        private SelectTargetAndFire selectTargetAndFire;

        protected void Awake()
        {
            selectTargetAndFire = GetComponent<SelectTargetAndFire>();
        }

        protected void Start()
        {
            selectTargetAndFire.StartFiring();
        }

        public void OnDeath()
        {
            selectTargetAndFire.StopFiring();
            Wallet.Save();
            Debug.Log("WAVE FAILED!");
        }
    
    }

    
}
