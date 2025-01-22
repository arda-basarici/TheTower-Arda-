using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
    public abstract class ObserverManager<TObserver> where TObserver : Delegate
    {
        private readonly List<WeakReference<TObserver>> observers = new();
        private readonly List<TObserver> strongReferences = new(); // temporary solution for garbage collection

        public void RegisterObserver(TObserver observer)
        {
            if (!observers.Any(wr => wr.TryGetTarget(out var o) && o.Equals(observer)))
            {
                strongReferences.Add(observer); 
                observers.Add(new WeakReference<TObserver>(observer));
            }          
            
        }

        public void UnregisterObserver(TObserver observer)
        {
            observers.RemoveAll(wr => wr.TryGetTarget(out var o) && o.Equals(observer));
            strongReferences.Remove(observer);
        }
        protected void NotifyObservers(Action<TObserver> notifyAction)
        {
            foreach (var weakObserver in observers.ToList())
            {
                if (weakObserver.TryGetTarget(out var observer))
                {
                    try
                    {
                        
                        notifyAction(observer);
                    }
                    catch (Exception ex)
                    {
                        Debug.Log($"Error notifying observer: {ex.Message}");
                    }
                }
                else
                {
                    Debug.Log("observer is null");
                    observers.Remove(weakObserver);
                }
            }
        }

        public void CleanupObservers()
        {
            observers.RemoveAll(wr => !wr.TryGetTarget(out _));
        }
    }
}
