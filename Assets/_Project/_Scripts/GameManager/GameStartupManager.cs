using UnityEngine;

namespace Game
{
    public class GameStartupManager : MonoBehaviour
    {
        private static GameStartupManager _instance;
        private static bool _hasInitialized = false;

        protected void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
            if (!_hasInitialized)
            {
                _hasInitialized = true;
                PerformFirstTimeSetup();
            }  

        }

        protected void Start()
        {
            SceneManager.InitializeCurrentScene();
        }

        private void PerformFirstTimeSetup()
        {
            Debug.Log("Game loaded for the first time!");
            InitializeGlobalSystems();
        }

        private void InitializeGlobalSystems()
        {
            Debug.Log("Initializing global systems...");
            SystemInitializer.InitializeSystemsByPhase(InitializationPhase.Global);
            Wallet.Load();
        }

        protected void Update()
        {
            GameStateMachine.Update();
        }
    }
}