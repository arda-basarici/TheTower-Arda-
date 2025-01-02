using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EventManager<TState>
    {
        private readonly List<Action<TState>> stateObservers = new();
        private readonly List<Action<object>> eventObservers = new();
        private TState currentState = default;

        public void RegisterStateObserver(Action<TState> onStateUpdate)
        {
            if (!stateObservers.Contains(onStateUpdate))
            {
                stateObservers.Add(onStateUpdate);

                if (currentState != null)
                {
                    try
                    {
                        onStateUpdate.Invoke(currentState);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Error syncing state to observer: {ex.Message}");
                    }
                }
            }
        }

        public void RegisterEventObserver(Action<object> onEvent)
        {
            if (!eventObservers.Contains(onEvent))
            {
                eventObservers.Add(onEvent);
            }
        }

        public void UnregisterStateObserver(Action<TState> onStateUpdate)
        {
            if (stateObservers.Contains(onStateUpdate))
            {
                stateObservers.Remove(onStateUpdate);
            }
        }

        public void UnregisterEventObserver(Action<object> onEvent)
        {
            if (eventObservers.Contains(onEvent))
            {
                eventObservers.Remove(onEvent);
            }
        }

        public void NotifyStateObservers(TState state)
        {
            currentState = state;

            foreach (var observer in stateObservers)
            {
                try
                {
                    observer.Invoke(state);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error notifying state observer: {ex.Message}");
                }
            }
        }

        public void NotifyEventObservers(object eventData)
        {
            foreach (var observer in eventObservers)
            {
                try
                {
                    observer.Invoke(eventData);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error notifying event observer: {ex.Message}");
                }
            }
        }
    }
}
