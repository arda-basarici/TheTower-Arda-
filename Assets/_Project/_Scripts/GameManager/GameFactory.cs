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
            SaveManager.Initialize(SaveHandlerFactory.CreateSaveHandler());
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