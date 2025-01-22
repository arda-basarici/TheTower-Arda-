using System;
using UnityEngine.Rendering;

namespace Game
{
    [Serializable]
    public class UpgradesSaveData : ISaveable
    {
        private readonly string saveKey = SaveKeys.UpgradesData;

        public const int currentVersion = Versions.UpgradesSaveData;

        public SerializedDictionary<UpgradeName, UpgradeData> upgrades = new SerializedDictionary<UpgradeName, UpgradeData>();

        public int Version { get; set; } = currentVersion;

        public UpgradesSaveData()
        {
            Version = Versions.UpgradesSaveData;
        }

        public void Load()
        {
            UpgradesSaveData loadedData = SaveManager.LoadWithAutoMigration<UpgradesSaveData>(saveKey, currentVersion);
            if (loadedData != null)
            {
                upgrades = loadedData.upgrades;
            }
        }

        public void Save()
        {
            SaveManager.Save(saveKey, this);
        }


    }
    public struct UpgradeData
    {
        public int level;
        public bool isAvailable;
    }
}