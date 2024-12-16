using Game;
using UnityEngine;

public class WalletUI : MonoBehaviour, IWalletObserver
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Awake()
    {
        Wallet.RegisterObserver(this);
        
    }

    protected void Start()
    {
        GameObject.Find("InGameCurrency").GetComponent<TMPro.TextMeshProUGUI>().text = Wallet.GetInGameCurrency().ToString();
        GameObject.Find("PersistentCurrency").GetComponent<TMPro.TextMeshProUGUI>().text = Wallet.GetPersistentCurrency().ToString();
    }

    public void OnCurrencyChange(CurrencyType type, float value)
    {
        if (type == CurrencyType.InGame)
        {
            GameObject.Find("InGameCurrency").GetComponent<TMPro.TextMeshProUGUI>().text = value.ToString();
        }
        else if (type == CurrencyType.Persistent)
        {
            GameObject.Find("PersistentCurrency").GetComponent<TMPro.TextMeshProUGUI>().text = value.ToString();
        }
    }

   
}
