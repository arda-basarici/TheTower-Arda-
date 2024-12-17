using System.Collections;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        protected void Start()
        {
            //GameFactory.InitilizeSessionSystems(); 
            //GameFactory.InitilizeAllSystemsForGamePlayState();

            SystemInitializer.InitializeSystemsByPhase(InitializationPhase.Global);
            SystemInitializer.InitializeSystemsByPhase(InitializationPhase.Gameplay);
        }

        protected void Update()
        {
            GameStateMachine.Update();
        }
    }
}