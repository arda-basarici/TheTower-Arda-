using UnityEngine;

namespace Game
{
    public class WalletUI : MonoBehaviour
    {
        protected void OnEnable()
        {
            EventSystem.Get<WalletEventManager>(EventManagerId.wallet).RegisterStateObserver(OnStateUpdate);
        }

        protected void OnDisable()
        {
            EventSystem.Get<WalletEventManager>(EventManagerId.wallet).RegisterStateObserver(OnStateUpdate);
        }

        public void OnStateUpdate(WalletState state)
        {
            GameObject.Find("InGameCurrency").GetComponent<TMPro.TextMeshProUGUI>().text = state.Money.ToString();
            GameObject.Find("PersistentCurrency").GetComponent<TMPro.TextMeshProUGUI>().text = state.Token.ToString();
        }
    }
}