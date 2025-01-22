using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/StatUpgrade")]
    public class StatUpgrade : ScriptableObject, IMenuStateEnterListener
    {
        [SerializeField] private UpgradeType upgradeType;
        [SerializeField] private UpgradeName upgradeName;
        [SerializeField] private StatType affectedStat;
        [SerializeField] private bool isPersistent;
        [SerializeField] private string description;
        [SerializeField] private int baseCost;
        [SerializeField] private int baseEffect; // stat value without any upgrades
        [SerializeField] private int costMultiplier;
        [SerializeField] private int effectMultiplier;
        [SerializeField] private bool isAvailable;
        [SerializeField] private int MaxLevel;

        private int currentLevel;

        private int CurrentCost => baseCost + costMultiplier * currentLevel;
        private int CurrentEffect => baseEffect + effectMultiplier * (currentLevel + 1); // stat value if upgraded

        private bool MaxLevelReached => currentLevel >= MaxLevel;

        private UpgradeStateManager upgradeStateManager;

        public string StateManagerKey { get; private set; }

        public UpgradeState State { get; private set; }


        public bool IsAvailable
        {
            get => isAvailable;
            set
            {
                isAvailable = value;
                Update();
            }
        }

        public void SetLevel(int level)
        {
            currentLevel = level;
            Update();
        }
        public void Initialize()
        {
            StateManagerKey = upgradeName.ToString() + isPersistent;
            upgradeStateManager = StateSystem.Get<UpgradeStateManager>(StateManagerKey);
            LifecycleManager.Register(typeof(IMenuStateEnterListener), this);
            Update();

        }

        public void OnMenuStateEnter()
        {
            Clear();
        }

        private void Update()
        {
            State = new UpgradeState
            {
                upgradeType = upgradeType,
                name = upgradeName,
                affectedStat = affectedStat,
                description = description,
                currentLevel = currentLevel,
                isPersistent = isPersistent,
                isAvailable = isAvailable,
                currentCost = CurrentCost,
                currentEffect = CurrentEffect,
                maxLevelReached = MaxLevelReached,
                upgrade = Upgrade
            };

            upgradeStateManager.UpdateUpgrade(State);
        }

        public void Upgrade()
        {
            if (IsAvailable)
            {
                currentLevel++;
                Update();
            }
        }

        public void Clear()
        {
            if (!isPersistent)
            {
                currentLevel = 0;
                Update();
            }
        }

    }
}