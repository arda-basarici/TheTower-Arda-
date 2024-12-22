using System.Collections.Generic;
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
            LifecycleManager.Call<IGamePlayStateEnterListener>(listener => listener.OnGamePlayStateEnter());
        }
        public void OnExit()
        {
            Debug.Log("GamePlayState: OnExit");
            LifecycleManager.Call<IGamePlayStateExitListener>(listener => listener.OnGamePlayStateExit());
        }
        public void Update()
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
            LifecycleManager.Call<IGamePlayStatePlayingUpdateListener>(listener => listener.GamePlayStatePlayingUpdate());

        }

        private void UpdatePaused()
        {
            LifecycleManager.Call<IGamePlayStatePausedUpdateListener>(listener => listener.GamePlayStatePausedUpdate());
        }
        private void TogglePause()
        {
            if (_subState == SubStates.Playing)
            {
                _subState = SubStates.Paused;
                //Time.timeScale = 0;
                LifecycleManager.Call<IGamePlayStatePauseListener>(listener => listener.OnGamePlayStatePause());
            }
            else if (_subState == SubStates.Paused)
            {
                _subState = SubStates.Playing;
                //Time.timeScale = 1;
                LifecycleManager.Call<IGamePlayStateResumeListener>(listener => listener.OnGamePlayStateResume());
            }
        }
    }
}