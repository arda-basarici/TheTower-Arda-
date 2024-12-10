namespace Game
{
    public interface IWalletObserver
    {
        void OnCurrencyChange(CurrencyType type, float value);
    }
}