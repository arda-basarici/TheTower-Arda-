using System;

namespace Game
{
    public struct UpgradeState
    {
        public UpgradeType upgradeType;
        public UpgradeName name;
        public StatType affectedStat;
        public string description;
        public int currentLevel;
        public bool isPersistent;
        public bool isAvailable;
        public int currentCost;
        public int currentEffect;
        public bool maxLevelReached;
        public Action upgrade;
    }

}