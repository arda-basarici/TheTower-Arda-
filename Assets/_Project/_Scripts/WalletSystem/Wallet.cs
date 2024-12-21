using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class Wallet
    {
        private static readonly List<IWalletObserver> observers = new List<IWalletObserver>();
        private static int money = 0;
        private static int token = 0;
        public static int Money
        {
            get => money;
            set
            {
                if (money != value)
                {
                    money = Mathf.Max(0, value);
                    NotifyObservers(CurrencyType.InGame, money);
                }
            }
        }

        public static int Token
        {
            get => token;
            set
            {
                if(token != value)
                {
                    token = Mathf.Max(0, value); 
                    NotifyObservers(CurrencyType.Persistent, token);
                }
            }
        }


        public static void AddMoney(int amount)
        {
            Money += amount;
        }

        public static void AddToken(int amount)
        {
            Token += amount;
        }

        public static void RemoveMoney(int amount)
        {
            if(money - amount < 0)
            {
                Debug.LogError("Not enough in-game currency to remove " + amount + " in-game currency.");
                return;
            }
            Money -= amount;
        }

        public static void RemoveToken(int amount)
        {
            if (token - amount < 0)
            {
                Debug.LogError("Not enough persistent currency to remove " + amount + " persistent currency.");
                return;
            }
            Token -= amount;
        }
        
        public static bool CanAffordMoney(int amount)
        {
            return money >= amount;
        }

        public static bool CanAffordToken(int amount)
        {
            return token >= amount;
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

        public static void Save()
        {
            WalletSaveData walletData = new WalletSaveData();
            walletData.money = money;
            walletData.token = token;
            walletData.Save();
        }

        public static void Load()
        {
            WalletSaveData walletData = new WalletSaveData();
            walletData.Load();
            Money = walletData.money;
            Token = walletData.token;  
            
        }
    }
}