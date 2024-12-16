using UnityEngine;

namespace Game
{
    public static class SaveManager
    {
        private static ISaveHandler _handler;

        public static void Initialize(ISaveHandler handler)
        {
            _handler = handler;
            Debug.Log($"SaveManager initialized with handler: {handler.GetType().Name}");
        }

        public static void Save<T>(string key, T data)
        {
            _handler.Save(key, data);
        }

        public static T Load<T>(string key)
        {
            return _handler.Exists(key) ? _handler.Load<T>(key) : default;
        }

        public static bool Exists(string key) => _handler.Exists(key);

        public static void Delete(string key) => _handler.Delete(key);
    }
}