using System;

namespace Game
{
    [Serializable]
    public class WalletData : IData
    {
        public int money = 0; // in game currency
        public int tokens = 0; // persistent currency

        public const int currentVersion = Versions.WalletData;
        public int Version { get; set; } = currentVersion;

        public WalletData()
        {
            Version = Versions.WalletData;
            money = 0;
            tokens = 0;
        }
    }
}