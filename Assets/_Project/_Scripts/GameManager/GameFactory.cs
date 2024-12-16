using UnityEngine;

namespace Game
{
    public static class GameFactory
    {

        public static IGameState GamePlayState { get; private set; } = new GamePlayState();
        public static void InitilizeAllSystemsForGamePlayState()
        {
            InitilizeEnemySystem();
            InitilizeWaveSystem();
        }

        public static void InitilizeSessionSystems()
        {
            // save , analytics etc etc
#if UNITY_WEBGL
        SaveManager.Initialize(new LocalStorageHandler());
#elif UNITY_EDITOR || UNITY_STANDALONE
            SaveManager.Initialize(new JsonFileHandler());
#elif UNITY_ANDROID || UNITY_IOS
        SaveManager.Initialize(new PlayerPrefsHandler());
#endif
        }

        private static void InitilizeWaveSystem()
        {

            WaveManager.Initialize(new ScriptableObjectsWaveDataLoader(), AssetLoader.LoadAsset<GameObject>(ResourcePaths.EnemySpawnerPrefab));
        }

        private static void InitilizeEnemySystem()
        {
            EnemyManager.Clear();
        }


    }

}