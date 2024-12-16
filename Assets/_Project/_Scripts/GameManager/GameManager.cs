using System.Collections;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        protected void Start()
        {
            GameFactory.InitilizeSessionSystems(); 
            GameFactory.InitilizeAllSystemsForGamePlayState();
        }

        private IEnumerator DelayedStart()
        {
          yield return new WaitForSeconds(2);
           
        }
        protected void Update()
        {
            GameStateMachine.Update();
        }
    }
}