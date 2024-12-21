using System.Collections;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        protected void Start()
        {
           

            SystemInitializer.InitializeSystemsByPhase(InitializationPhase.Global);
            
            Wallet.Load();

            SystemInitializer.InitializeSystemsByPhase(InitializationPhase.Gameplay);
        }

        protected void Update()
        {
            GameStateMachine.Update();
        }
    }
}