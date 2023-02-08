using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyDataManager : MonoBehaviour
{
    public static CurrencyDataManager Instance;
    PlayerSaveData_Currency _currencyDataCopy;
    [HideInInspector] public UnityEvent OnDataChanged_Currency;

    void Awake()
    {
        Instance = this;
    }

    public void InitManager()
    {
        _currencyDataCopy = PlayerDataManager.Instance.PlayerData.CurrencyData.GetCopy();
        PlayerDataManager.Instance.OnDataChanged.AddListener(OnPlayerGameDataChanged);
    }

    void OnPlayerGameDataChanged()
    {
        Debug.Log("[Currency-Data-Manager] OnDataChanged triggered.");
        var actualData = PlayerDataManager.Instance.PlayerData.CurrencyData;
        bool isDataEqual = actualData.IsEqual(_currencyDataCopy);

        if (!isDataEqual)
        {
            _currencyDataCopy = actualData.GetCopy();
            Debug.Log("[Currency-Data-Manager] Data updated. OnDataChanged_Currency() triggered.");
            OnDataChanged_Currency.Invoke();
        }
    }

    public void TryToAddCurrency(int addAmount)
    {
        PlayerDataManager.Instance.TryToAddCoins(addAmount);
    }
    
    public void TryToRemoveCurrency(int removeAmount)
    {
        PlayerDataManager.Instance.TryToRemoveCoins(removeAmount);
    }
}