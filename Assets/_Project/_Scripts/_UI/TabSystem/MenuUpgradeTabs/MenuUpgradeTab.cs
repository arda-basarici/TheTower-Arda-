using System.Linq;
using UnityEngine;

namespace Game
{
    public class MenuUpgradePanel : TabPanel
    {
        [SerializeField] private UpgradeType upgradeType;
        [SerializeField] private GameObject upgradeFramePrefab;
        [SerializeField] private GameObject upgradeFrameParentPrefab;
        [SerializeField] private Transform content;

        private GameObject currentUpgradeParent;

        public override string Title { get; set; }



        public override void Initialize()
        {
            FillUpgradeUI();
        }

        private void FillUpgradeUI()
        {
            var relevantUpgrades = UpgradeManager.persistentUpgrades
                .Where(upgrade => upgrade.State.upgradeType == upgradeType && upgrade.State.isAvailable)
                .ToList();
            Debug.Log(upgradeType.ToString() + " " + relevantUpgrades.Count);
            relevantUpgrades.ForEach(upgrade => AddUpgradeToUI(upgrade));
        }

        private void AddUpgradeToUI(StatUpgrade upgrade)
        {
            if (currentUpgradeParent == null)
            {
                currentUpgradeParent = InstantiationUtility.InstantiateWithCallback(upgradeFrameParentPrefab, content, instance =>
                {
                    currentUpgradeParent = instance;
                    if(instance == null)
                    {
                        Debug.LogError("Instance is null");
                    }
                    AddUpgradeToParent(upgrade, instance);
                });
            }
            else
            {
                AddUpgradeToParent(upgrade, currentUpgradeParent);
            }

        }

        private void AddUpgradeToParent(StatUpgrade upgrade, GameObject parent)
        {
            GameObject upgradeFrame = Instantiate(upgradeFramePrefab, parent.transform);
            upgradeFrame.GetComponent<UpgradeFrameUI>().SetUpgradeListener(upgrade.StateManagerKey);
            const int maxChildren = 2;
            if (currentUpgradeParent.transform.childCount >= maxChildren)
            {
                currentUpgradeParent = null;
            }
        }

    }
}
