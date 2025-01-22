using UnityEngine;

namespace Game
{
    public class UpgradeStateManager : StateManager<UpgradeState>
    {
        private UpgradeState currentState;
        public void UpdateUpgrade(UpgradeState upgradeState)
        {
            Debug.LogWarning("Updating upgrade state");
            currentState = upgradeState;
            NotifyStateObservers(currentState);
        }
    }
}
