using System;
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

        public static T LoadWithAutoMigration<T>(string key, int targetVersion, Func<T, T> manualMigration = null) where T : class, new()
        {
            T data = null;
            try
            {
                data = _handler.Exists(key) ? _handler.Load<T>(key) : new T();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data for key '{key}': {e.Message}");
            }
            if (data == null) data = new T(); 
            data = AutoMigration.MigrateMissingFields(data, targetVersion);
            return data;
        }

        public static bool Exists(string key) => _handler.Exists(key);

        public static void Delete(string key) => _handler.Delete(key);
    }
}