using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public static class Wallet
    {
        private static readonly List<IWalletObserver> observers = new List<IWalletObserver>();
        private static int inGameCurrency;
        private static int persistentCurrency;

        public static void AddInGameCurrency(int amount)
        {
            inGameCurrency += amount;
            NotifyObservers(CurrencyType.InGame, inGameCurrency);
        }

        public static void AddPersistentCurrency(int amount)
        {
            persistentCurrency += amount;
            NotifyObservers(CurrencyType.Persistent, persistentCurrency);
        }

        public static void RemoveInGameCurrency(int amount)
        {
            if(inGameCurrency - amount < 0)
            {
                Debug.LogError("Not enough in-game currency to remove " + amount + " in-game currency.");
                return;
            }
            inGameCurrency -= amount;

            NotifyObservers(CurrencyType.InGame, inGameCurrency);
        }

        public static void RemovePersistentCurrency(int amount)
        {
            if (persistentCurrency - amount < 0)
            {
                Debug.LogError("Not enough persistent currency to remove " + amount + " persistent currency.");
                return;
            }
            persistentCurrency -= amount;
            
            NotifyObservers(CurrencyType.Persistent, persistentCurrency);
        }

        public static int GetInGameCurrency()
        {
            return inGameCurrency;
        }

        public static int GetPersistentCurrency() { 
            return persistentCurrency;
        }

        public static void SetInGameCurrency(int amount)
        {
            inGameCurrency = amount;
            NotifyObservers(CurrencyType.InGame, amount);
        }

        public static void SetPersistentCurrency(int amount)
        {
            persistentCurrency = amount;
            NotifyObservers(CurrencyType.Persistent, amount);
        }
        
        public static bool CanAffordInGameCurrency(int amount)
        {
            return inGameCurrency >= amount;
        }

        public static bool CanAffordPersistentCurrency(int amount)
        {
            return persistentCurrency >= amount;
        }

        private static void NotifyObservers(CurrencyType type, float value)
        {
            foreach (var observer in observers)
            {
                observer.OnCurrencyChange(type, value);
            }
        }

        public static void RegisterObserver(IWalletObserver observer)
        {
            observers.Add(observer);
        }

        public static void UnregisterObserver(IWalletObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        public static void ClearObservers()
        {
            observers.Clear();
        }
    }
}