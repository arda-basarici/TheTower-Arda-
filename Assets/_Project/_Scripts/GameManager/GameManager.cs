using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        protected void Start()
        {
            GameStateMachine.ChangeState(GameFactory.GamePlayState);
        }
        protected void Update()
        {
            GameStateMachine.Update();
        }
    }
}