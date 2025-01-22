using TMPro;
using UnityEngine;

namespace Game
{
    public class WalletUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI inGameCurrency;
        [SerializeField] private TextMeshProUGUI persistentCurrency;
        protected void OnEnable()
        {
            StateSystem.Get<WalletStateManager>(StateManagerId.wallet).RegisterStateObserver(OnStateUpdate);
        }

        protected void OnDisable()
        {
            StateSystem.Get<WalletStateManager>(StateManagerId.wallet).UnregisterObserver(OnStateUpdate);
        }

        public void OnStateUpdate(WalletState state)
        {
            if(inGameCurrency != null)
            {
                inGameCurrency.text = state.Money.ToString();
            }
            if (persistentCurrency != null)
            {
                persistentCurrency.text = state.Token.ToString();
            }
        }
    }
}