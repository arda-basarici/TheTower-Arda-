using System.Collections.Generic;

namespace Game
{
    static class UpgradeManager
    {
        private static List<StatUpgrade> ingameUpgrades = new List<StatUpgrade>();

        public static void AddInGameUpgrade(StatUpgrade upgrade)
        {
            ingameUpgrades.Add(upgrade);
        }

        private static StatUpgrade GetIngameUpgrade(InGameUpgradeNames name)
        {
            return ingameUpgrades.Find(upgrade => upgrade.Name == name);
        }

        public static int GetIngameUpgradeCost(InGameUpgradeNames name)
        {
            return GetIngameUpgrade(name).CurrentCost;
        }

        public static int GetIngameUpgradeEffect(InGameUpgradeNames name)
        {
            return GetIngameUpgrade(name).CurrentEffect;
        }

        public static bool IsIngameUpgradeAvailable(InGameUpgradeNames name)
        {
            return GetIngameUpgrade(name).IsAvailable;
        }

        public static void UnLockIngameUpgrade(InGameUpgradeNames name)
        {
            GetIngameUpgrade(name).IsAvailable = true;
        }

        public static void RegisterObserver(InGameUpgradeNames name, IUpgradeObserver observer)
        {
            GetIngameUpgrade(name).RegisterObserver(observer);
        }

        public static void UnregisterObserver(InGameUpgradeNames name, IUpgradeObserver observer)
        {
            GetIngameUpgrade(name).UnregisterObserver(observer);
        }

        public static void Upgrade(InGameUpgradeNames name)
        {
            GetIngameUpgrade(name).Upgrade();
        }
        
        public static bool CanAffordInGameUpgrade(InGameUpgradeNames name, int currentCurrency)
        {
            return currentCurrency >= GetIngameUpgradeCost(name);
        }

        public static void LoadAllInGameUpgrades()
        {
            
        }

        public static void SaveAllInGameUpgrades()
        {

        }
    }
}