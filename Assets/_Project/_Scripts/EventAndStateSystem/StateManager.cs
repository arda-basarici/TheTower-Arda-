using System;
using UnityEngine; 
namespace Game
{
    public class StateManager<TState> : ObserverManager<Action<TState>>
    {
        private TState currentState;

        public void NotifyStateObservers(TState state)
        {
            currentState = state;
            NotifyObservers(observer => observer.Invoke(state));
        }

        public TState GetCurrentState()
        {
            return currentState;
        }

        public void RegisterStateObserver(Action<TState> onStateUpdate, bool syncState = true)
        {
            base.RegisterObserver(onStateUpdate);

            if (currentState != null && syncState == true)
            {
                try
                {
                    onStateUpdate.Invoke(currentState);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error syncing state to observer: {ex.Message}");
                }
            }
        }
    }
}
