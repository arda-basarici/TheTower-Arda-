using System;

namespace Game
{
    [Serializable]
    public class WalletSaveData : ISaveable
    {
        private readonly string saveKey = SaveKeys.WalletData;
        public float money = 0;
        public float token = 0;

        public const int currentVersion = Versions.WalletData;
        public int Version { get; set; } = currentVersion;

        public WalletSaveData()
        {
            Version = Versions.WalletData;
            money = 0;
            token = 0;
        }

        public void Load()
        {
            WalletSaveData loadedData = SaveManager.LoadWithAutoMigration<WalletSaveData>(saveKey, currentVersion);
            if (loadedData != null)
            {
                money = loadedData.money;
                token = loadedData.token;
            }
        }

        public void Save()
        {
            SaveManager.Save(saveKey, this);
        }
    }
}