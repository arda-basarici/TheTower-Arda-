using System.Collections.Generic;
using UnityEngine.TextCore.Text;

namespace Game
{
    public static class EventSystem
    {
        private static readonly Dictionary<string, object> eventManagers = new();

        public static TEventManager Get<TEventManager>(string id) where TEventManager : EventManager, new()
        {
            if (!eventManagers.ContainsKey(id))
            {
                var manager = new TEventManager();
                eventManagers[id] = manager;
            }
            return (TEventManager)eventManagers[id];
        }

        public static TEventManager CreateAndRegisterEventManager<TEventManager>(string id) where TEventManager : EventManager, new()
        {
            if (!eventManagers.ContainsKey(id))
            {
                var manager = new TEventManager();
                eventManagers[id] = manager;
            }
            return (TEventManager)eventManagers[id];
        }

        public static void Remove(string id)
        {
            if (eventManagers.ContainsKey(id))
            {
                eventManagers.Remove(id);
            }
        }
    }
}