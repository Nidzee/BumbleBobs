using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTab : MonoBehaviour
{
    [SerializeField] HealthDataWidget _healthDataWidget;


    public void InitTab()
    {
        SetWidgetData();
        ConnectSignals();
    }

    void ConnectSignals()
    {
        _healthDataWidget.UpgradeButton.onClick.AddListener(HealthDataManager.Instance.TryToUpgradeHealth);
        HealthDataManager.Instance.OnDataChanged_Health.AddListener(SetWidgetData);
        CurrencyDataManager.Instance.OnDataChanged_Currency.AddListener(SetWidgetData);
    }

    void SetWidgetData()
    {
        _healthDataWidget.Init();
        _healthDataWidget.RefreshPurchaseButton();
    }
}
