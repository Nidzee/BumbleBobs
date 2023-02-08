using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourTab : MonoBehaviour
{
    [SerializeField] ArmourDataWidget _armourDataWidget;


    public void InitTab()
    {
        SetWidgetData();
        ConnectSignals();
    }

    void ConnectSignals()
    {
        _armourDataWidget.UpgradeButton.onClick.AddListener(ArmourDataManager.Instance.TryToUpgradeArmour);
        ArmourDataManager.Instance.OnDataChanged_Armour.AddListener(SetWidgetData);
        CurrencyDataManager.Instance.OnDataChanged_Currency.AddListener(SetWidgetData);
    }

    void SetWidgetData()
    {
        _armourDataWidget.Init();
        _armourDataWidget.RefreshPurchaseButton();
    }
}