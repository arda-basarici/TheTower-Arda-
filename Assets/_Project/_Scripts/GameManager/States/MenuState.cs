using UnityEngine;
namespace Game
{
    public class MenuState : IGameState
    {

        public GameStateType Type => GameStateType.Menu;

        public void OnEnter()
        {
            SystemInitializer.InitializeSystemsByPhase(InitializationPhase.Menu);
            LifecycleManager.Call<IMenuStateEnterListener>(listener => listener.OnMenuStateEnter());
            Debug.Log("MenuState OnEnter");
        }
        public void OnExit()
        {
            LifecycleManager.Call<IMenuStateExitListener>(listener => listener.OnMenuStateExit());
        }
        public void Update()
        {
            LifecycleManager.Call<IMenuStateUpdateListener>(listener => listener.MenuStateUpdate());
        }
    }
}