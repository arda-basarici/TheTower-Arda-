using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/StatUpgrade")]
    public class StatUpgrade : ScriptableObject
    {

        private readonly List<IUpgradeObserver> observers = new List<IUpgradeObserver>();
        [SerializeField] private UpgradeType upgradeType;
        [SerializeField] private InGameUpgradeNames upgradeName;
        [SerializeField] private StatType affectedStat;
        [SerializeField] private bool isPersistent;
        [SerializeField] private string description; 
        [SerializeField] private int baseCost;
        [SerializeField] private int baseEffect;
        [SerializeField] private int costMultiplier;
        [SerializeField] private int effectMultiplier;
        [SerializeField] private bool isAvailable;
        
        private int currentLevel;
        public UpgradeType UpgradeType => upgradeType;
        public InGameUpgradeNames Name => upgradeName;
        public StatType AffectedStat => affectedStat;
        public string Description => description;
        public int BaseCost => baseCost;
        public int BaseEffect => baseEffect;
        public int CostMultiplier => costMultiplier;
        public int EffectMultiplier => effectMultiplier;
        public int CurrentLevel => currentLevel;
        public bool IsPersistent => isPersistent;


        public int CurrentCost => BaseCost + CostMultiplier * CurrentLevel;
        public int CurrentEffect => BaseEffect + EffectMultiplier * CurrentLevel;

        public bool IsAvailable
        {
            get => isAvailable;
            set
            {
                isAvailable = value;
                NotifyObservers();
            }
        }

        public void RegisterObserver(IUpgradeObserver observer)
        {
            observers.Add(observer);
        }

        public void UnregisterObserver(IUpgradeObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.OnUpgradeChange();
            }
        }

        public void SetBaseEffect(int value)
        {
            baseEffect = value;
            NotifyObservers();
        }

        public void IncreaseLevel()
        {
            currentLevel++;
            NotifyObservers();
        }

        public void ApplyEffect(StatManager statManager)
        {
            statManager.SetStat(affectedStat, CurrentEffect);
        }

        public void Upgrade()
        {
            if (IsAvailable)
            {
                IncreaseLevel();
            }
        }
    }
}