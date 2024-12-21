using UnityEngine;

namespace Game
{
    public class GamePlayState : IGameState
    {
        public GameStateType Type => GameStateType.GamePlay;

        private enum SubStates
        {
            Playing,
            Paused
        }

        private SubStates _subState = SubStates.Playing;

        public void OnEnter()
        {
            SystemInitializer.InitializeSystemsByPhase(InitializationPhase.Gameplay);
        }
        public void OnExit()
        {
            Debug.Log("GamePlayState: OnExit");
        }
        public void OnUpdate()
        {
            switch (_subState)
            {
                case SubStates.Playing:
                    UpdatePlaying();
                    break;
                case SubStates.Paused:
                    UpdatePaused();
                    break;
            }


            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }

        }

        private void UpdatePlaying()
        {

        }

        private void UpdatePaused()
        {
        }



        private void TogglePause()
        {
            if (_subState == SubStates.Playing)
            {
                _subState = SubStates.Paused;
                Time.timeScale = 0;
            }
            else if (_subState == SubStates.Paused)
            {
                _subState = SubStates.Playing;
                Time.timeScale = 1;
            }
        }
    }
}