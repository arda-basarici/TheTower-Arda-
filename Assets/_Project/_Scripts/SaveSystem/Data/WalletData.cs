using System;

namespace Game
{
    [Serializable]
    public class WalletData
    {
        public int money = 0; // in game currency
        public int tokens = 0; // persistent currency
    }
}