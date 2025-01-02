using System.Collections.Generic;

namespace Game
{
    public static class EventSystem
    {
        private static readonly Dictionary<string, object> eventManagers = new();

        public static TEventManager Get<TEventManager>(string id) where TEventManager : new()
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