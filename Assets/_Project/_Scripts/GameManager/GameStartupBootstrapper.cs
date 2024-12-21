using Game;
using UnityEngine;

public class GameStartupBootstrapper : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeOnGameStart()
    {
        if (GameObject.FindFirstObjectByType<GameStartupManager>() == null)
        {
            var startupManager = new GameObject("GameStartupManager");
            startupManager.AddComponent<GameStartupManager>();
        }
    }
}
