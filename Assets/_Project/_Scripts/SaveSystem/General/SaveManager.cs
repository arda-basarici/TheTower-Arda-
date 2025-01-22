using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class SaveManager
    {
        private static ISaveHandler _handler;

        private static readonly List<ISaveObserver> saveObservers = new List<ISaveObserver>();
        private static readonly List<ILoadObserver> loadObservers = new List<ILoadObserver>();
        public static void Initialize(ISaveHandler handler)
        {
            _handler = handler;
            Debug.Log($"SaveManager initialized with handler: {handler.GetType().Name}");
        }

        public static void Save<T>(string key, T data)
        {
            _handler.Save(key, data);
            NotifyObserversOnsave(key);

        }

        public static T LoadWithAutoMigration<T>(string key, int targetVersion) where T : class, new()
        {
            Debug.Log($"Attempting to load data for key: {key}, Type: {typeof(T)}");
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
            NotifyObserversOnLoad(key);
            return data;
        }

        public static bool Exists(string key) => _handler.Exists(key);

        public static void Delete(string key) => _handler.Delete(key);

        public static void RegisterSaveObserver(ISaveObserver observer)
        {
            saveObservers.Add(observer);
        }

        public static void UnregisterSaveObserver(ISaveObserver observer)
        {
            if (saveObservers.Contains(observer))
            {
                saveObservers.Remove(observer);
            }
        }

        public static void RegisterLoadObserver(ILoadObserver observer)
        {
            loadObservers.Add(observer);
        }

        public static void UnregisterLoadObserver(ILoadObserver observer)
        {
            if (loadObservers.Contains(observer))
            {
                loadObservers.Remove(observer);
            }
        }

        private static void NotifyObserversOnsave(string key)
        {
            foreach (var observer in saveObservers)
            {
                observer.OnSave(key);
            }
        }

        private static void NotifyObserversOnLoad(string key)
        {
            foreach (var observer in loadObservers) 
            {
                observer.OnLoad(key);
            }
        }
    }
}