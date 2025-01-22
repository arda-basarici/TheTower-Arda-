using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class StateSystem
    {
        private static readonly Dictionary<string, object> stateManagers = new();

        public static TEventManager Get<TEventManager>(string id) where TEventManager : new()
        {
            if (!stateManagers.ContainsKey(id))
            {
                var manager = new TEventManager();
                stateManagers[id] = manager;
            }
            return (TEventManager)stateManagers[id];
        }

        public static TEventManager CreateAndRegisterEventManager<TEventManager>(string id) where TEventManager : EventManager, new()
        {
            if (!stateManagers.ContainsKey(id))
            {
                var manager = new TEventManager();
                stateManagers[id] = manager;
            }
            return (TEventManager)stateManagers[id];
        }

        public static void Remove(string id)
        {
            if (stateManagers.ContainsKey(id))
            {
                stateManagers.Remove(id);
            }
        }
    }
}