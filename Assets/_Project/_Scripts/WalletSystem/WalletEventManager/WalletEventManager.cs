
namespace Game
{
    public class WalletEventManager : EventManager<WalletState>
    {
        private WalletState currentState = new WalletState();

        public void UpdateWallet(CurrencyType type, float value)
        {
            if (type == CurrencyType.InGame)
            {
                currentState.Money = value;
            }
            else if (type == CurrencyType.Persistent)
            {
                currentState.Token = value;
            }

            NotifyStateObservers(currentState);
        }
    }
}
