using UnityEngine;
namespace Game
{
    public class SceneManagerInitializer : IInitializable
    {
        public InitializationPhase Phase => InitializationPhase.Global;
        public void Initialize()
        {
            var sceneRegistry = AssetLoader.LoadAsset<SceneRegistry>(ResourcePaths.SceneRegistry);
            SceneManager.Initialize(sceneRegistry);
            Debug.Log("Scene manager initialized.");
        }
    }
}