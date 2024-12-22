using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Game
{
    public static class LifecycleManager
    {
        private static readonly Dictionary<Type, List<WeakReference<object>>> _listeners = new();

        public static void Initialize()
        {
        }


        public static void Register(Type interfaceType, object listener)
        {
            if (!_listeners.ContainsKey(interfaceType))
            {
                _listeners[interfaceType] = new List<WeakReference<object>>();
            }

            _listeners[interfaceType].Add(new WeakReference<object>(listener));
        }

        public static void Unregister(Type interfaceType, object listener)
        {
            if (_listeners.TryGetValue(interfaceType, out var listeners))
            {
                var weakReferenceToRemove = listeners.FirstOrDefault(
                    listenerRef => listenerRef.TryGetTarget(out var target) && ReferenceEquals(target, listener)
                );
                if (weakReferenceToRemove != null)
                {
                    listeners.Remove(weakReferenceToRemove);
                }
            }
        }


        public static void Call<T>(Action<T> action) where T : class
        {
            var interfaceType = typeof(T);
            if (_listeners.TryGetValue(interfaceType, out var listeners))
            {
                foreach (var listenerRef in listeners.ToList())
                {
                    if (listenerRef.TryGetTarget(out var target) && target is T listener)
                    {
                        action(listener);
                    }
                    else
                    {
                        listeners.Remove(listenerRef);
                    }
                }
            }
        }
    }
}