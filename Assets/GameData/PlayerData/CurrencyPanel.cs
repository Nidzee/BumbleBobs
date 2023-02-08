using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyPanel : MonoBehaviour
{
    [SerializeField] CurrencyCoinsWidget _coinsWidget;
    [SerializeField] int _removeAmount;
    [SerializeField] int _addAmount;
    [SerializeField] Button _addCoinsButton;
    [SerializeField] Button _removeCoinsButton;


    public void InitPanel()
    {
        _addCoinsButton.GetComponentInChildren<Text>().text = "Add: " + _addAmount;
        _removeCoinsButton.GetComponentInChildren<Text>().text = "Remove: " + _removeAmount;

        _addCoinsButton.onClick.AddListener(AddCurrency_Test);
        _removeCoinsButton.onClick.AddListener(RemoveCurrency_Test);

        RefreshCurrencyPanel();

        CurrencyDataManager.Instance.OnDataChanged_Currency.AddListener(RefreshCurrencyPanel);
    }

    void AddCurrency_Test()
    {
        CurrencyDataManager.Instance.TryToAddCurrency(_addAmount);
    }

    void RemoveCurrency_Test()
    {
        CurrencyDataManager.Instance.TryToRemoveCurrency(_removeAmount);
    }

    void RefreshCurrencyPanel()
    {
        _coinsWidget.SetCoinsAmount();
    }
}