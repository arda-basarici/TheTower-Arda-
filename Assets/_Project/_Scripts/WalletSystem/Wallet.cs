using UnityEngine;

namespace Game
{
    public static class Wallet
    {
        private static WalletState walletState;
        public static float Money
        {
            get => walletState.Money;
            set
            {
                if (walletState.Money != value)
                {
                    walletState.Money = Mathf.Max(0, value);
                    StateSystem.Get<WalletStateManager>(StateManagerId.wallet).UpdateWallet(walletState);
                }
            }
        }

        public static float Token
        {
            get => walletState.Token;
            set
            {
                if(walletState.Token != value)
                {
                    walletState.Token = Mathf.Max(0, value);
                    StateSystem.Get<WalletStateManager>(StateManagerId.wallet).UpdateWallet(walletState);
                }
            }
        }


        public static void AddMoney(float amount)
        {
            Money += amount;
        }

        public static void AddToken(float amount)
        {
            Token += amount;
        }

        public static void RemoveMoney(float amount)
        {
            if(Money - amount < 0)
            {
                Debug.LogError("Not enough in-game currency to remove " + amount + " in-game currency.");
                return;
            }
            Money -= amount;
        }

        public static void RemoveToken(int amount)
        {
            if (Token - amount < 0)
            {
                Debug.LogError("Not enough persistent currency to remove " + amount + " persistent currency.");
                return;
            }
            Token -= amount;
        }
        
        public static bool CanAffordMoney(int amount)
        {
            return Money >= amount;
        }

        public static bool CanAffordToken(int amount)
        {
            return Token >= amount;
        }

        public static void Save()
        {
            WalletSaveData walletData = new WalletSaveData();
            walletData.money = Money;
            walletData.token = Token;
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