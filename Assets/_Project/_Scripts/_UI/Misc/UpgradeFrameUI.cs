using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UpgradeFrameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Name;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private TextMeshProUGUI effect;
        [SerializeField] private Button upgradeButton;
        private UpgradeStateManager UpgradeStateManager;

        public void SetUpgradeListener(string stateManagerKey)
        {
            Debug.LogWarning("registering"+ stateManagerKey);
            UpgradeStateManager = StateSystem.Get<UpgradeStateManager>(stateManagerKey);
            UpgradeStateManager.RegisterStateObserver(SetFrameUI);
        }

        protected void OnDestroy()
        {
            if(UpgradeStateManager != null)
                UpgradeStateManager.UnregisterObserver(SetFrameUI);
            upgradeButton.onClick.RemoveAllListeners();
        }
        public void SetFrameUI(UpgradeState state)
        {
            Debug.Log("Setting frame UI");
            Name.text = state.name.ToString();
            price.text = state.currentCost.ToString();
            effect.text = state.currentEffect.ToString();
            SetUpgradeButton(state);
        }

        public void SetUpgradeButton(UpgradeState state)
        {
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener( ()  => state.upgrade());
        }
    }
}
