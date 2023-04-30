using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance;

    private void Awake()
    {
        Instance = this;
    }



    [SerializeField] ArmourTab _armourTab;
    [SerializeField] HealthTab _healthTab;
    [SerializeField] CurrencyPanel _currencyPanel;

    public void InitManager()
    {
        _armourTab.InitTab();
        _healthTab.InitTab();
        _currencyPanel.InitPanel();
    }
}