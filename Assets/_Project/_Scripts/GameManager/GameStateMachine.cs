using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    public static class GameStateMachine
    {
        private static IGameState _currentState;

        public static void ChangeState(IGameState newState)
        {
            _currentState?.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }

        public static void Update()
        {
            _currentState?.OnUpdate();
        }
    }
}