
using UnityEngine;

namespace Game
{
    public class WalletStateManager : StateManager<WalletState>
    {
        private WalletState currentState = new WalletState();

        public void UpdateWallet(WalletState walletState)
        {
            currentState.Money = walletState.Money;
            currentState.Token = walletState.Token;
            NotifyStateObservers(currentState);
        }
    }
}
