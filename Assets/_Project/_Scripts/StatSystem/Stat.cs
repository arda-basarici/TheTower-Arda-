using System;
using System.Collections.Generic;

namespace Game
{
    public abstract class Stat
    {

        private readonly List<IStatObserver> observers = new List<IStatObserver>();
        public abstract StatType Type { get; }
        public float BaseValue { get; protected set; }
        public float CurrentValue;


        protected Stat(float baseValue)
        {
            BaseValue = baseValue;
            CurrentValue = baseValue;
        }

        public void RegisterObserver(IStatObserver observer)
        {
            observers.Add(observer);
        }

        public void UnregisterObserver(IStatObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        protected void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.OnStatChange(CurrentValue);
            }
        }
        public virtual void IncreaseStat(float value)
        {
            CurrentValue += value;
            NotifyObservers();
        }
        public virtual void DecreaseStat(float value)
        {
            CurrentValue -= value;
            NotifyObservers();
        }

        public virtual void SetStat(float value)
        {
            CurrentValue = value;
            NotifyObservers();
        }
    }
}