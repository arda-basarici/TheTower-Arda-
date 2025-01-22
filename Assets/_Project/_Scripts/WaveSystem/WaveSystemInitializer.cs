using UnityEngine;

namespace Game
{
    public class WaveSystemInitializer : IInitializable
    {
        public InitializationPhase Phase => InitializationPhase.Gameplay;
        public void Initialize()
        {
            WaveManager.Initialize(AssetLoader.LoadAsset<GameObject>(ResourcePaths.EnemySpawnerPrefab));
            Debug.Log("Wave system initialized.");
        }
    }
}