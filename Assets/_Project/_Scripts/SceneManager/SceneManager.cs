using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;

namespace Game
{
    public static class SceneManager
    {
        private static SceneRegistry _sceneRegistry;

        public static void Initialize(SceneRegistry registry)
        {
            _sceneRegistry = registry;
        }

        public static async Task LoadSceneAsync(SceneName sceneName, System.Action onLoaded = null, bool additive = false)
        {
            if (_sceneRegistry == null)
            {
                Debug.LogError("SceneRegistry not initialized.");
                return;
            }

            var sceneData = _sceneRegistry.GetSceneData(sceneName);
            if (sceneData == null)
            {
                Debug.LogError($"SceneData not found for scene: {sceneName}");
                return;
            }

            var loadMode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            AsyncOperation loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName.ToString(), loadMode);

            if (loadOperation == null)
            {
                Debug.LogError($"Failed to load scene: {sceneName}");
                return;
            }

            while (!loadOperation.isDone)
            {
                await Task.Yield();
            }


            await Task.Yield();

            InitializeSceneSystems(sceneData);

            onLoaded?.Invoke();
        }

        public static void InitializeCurrentScene()
        {
            if (_sceneRegistry == null)
            {
                Debug.LogError("SceneRegistry not initialized.");
                return;
            }

            string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            SceneData sceneData = null;
            if (Enum.TryParse(currentSceneName, out Game.SceneName sceneEnum))
            {
                sceneData = _sceneRegistry.GetSceneData(sceneEnum);
                if (sceneData == null)
                {
                    Debug.LogWarning($"No SceneData configured for scene: {currentSceneName}");
                    return;
                }

                Debug.Log($"Automatically initializing systems for scene: {currentSceneName}");
                InitializeSceneSystems(sceneData);
            }
            else
            {
                Debug.LogError($"Scene name '{currentSceneName}' does not match any value in Game.SceneName enum.");
                return;
            }

        }

        private static void InitializeSceneSystems(SceneData sceneData)
        {
            Debug.Log($"Initializing systems for scene: {sceneData.sceneName}");

            GameStateMachine.ChangeState(GameStateFactory.GetState(sceneData.targetState));

            /// if necessary , add more initialization logic here
        }
    }
}