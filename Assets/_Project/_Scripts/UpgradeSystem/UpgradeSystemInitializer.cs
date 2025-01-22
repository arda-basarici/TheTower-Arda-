using UnityEngine;

namespace Game
{
    public class UpgradeSystemInitializer : IInitializable
    {
        public InitializationPhase Phase => InitializationPhase.Global;
        public void Initialize()
        {
            UpgradeManager.Initialize();
            Debug.Log("Upgrade system initialized.");
        }
    }
}