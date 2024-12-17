using UnityEngine;

namespace Game
{
    public class EnemySystemInitializer : IInitializable
    {
        public InitializationPhase Phase => InitializationPhase.Gameplay;
        public void Initialize()
        {
            EnemyManager.Clear();
            Debug.Log("Enemy system initialized");
        }
    }
}