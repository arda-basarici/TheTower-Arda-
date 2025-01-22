using System.Collections.Generic;
using System.Linq;

namespace Game
{
    static class UpgradeManager
    {
        public static List<StatUpgrade> allUpgrades = new List<StatUpgrade>();
        public static List<StatUpgrade> ingameUpgrades = new List<StatUpgrade>();
        public static List<StatUpgrade> persistentUpgrades = new List<StatUpgrade>();

        public static void Initialize()
        {
            allUpgrades = AssetLoader.LoadAll<StatUpgrade>(ResourcePaths.StatUpgradeData).ToList();
            allUpgrades.ForEach(upgrade => upgrade.Initialize());
            persistentUpgrades = allUpgrades.FindAll(upgrade => upgrade.State.isPersistent);
            ingameUpgrades = allUpgrades.FindAll(upgrade => !upgrade.State.isPersistent);
            LoadPersistantUpgrades();
        }
      
        private static void LoadPersistantUpgrades()
        {
            UpgradesSaveData upgradesSaveData = new UpgradesSaveData();
            upgradesSaveData.Load();
            upgradesSaveData.upgrades.ToList().ForEach(upgrade =>
            {
                if (persistentUpgrades.Find(up => up.State.name == upgrade.Key) == null) return;
                persistentUpgrades.Find(up => up.State.name == upgrade.Key).SetLevel(upgrade.Value.level);
                persistentUpgrades.Find(up => up.State.name == upgrade.Key).IsAvailable = upgrade.Value.isAvailable;
            });
        }

        public static void SaveAllPersistantUpgrades()
        {
            UpgradesSaveData upgradesSaveData = new UpgradesSaveData();
            persistentUpgrades.ForEach(upgrade =>
            {
                upgradesSaveData.upgrades.Add(upgrade.State.name, new UpgradeData
                {
                    level = upgrade.State.currentLevel,
                    isAvailable = upgrade.State.isAvailable
                });
            });
            upgradesSaveData.Save();

        }
    }
}