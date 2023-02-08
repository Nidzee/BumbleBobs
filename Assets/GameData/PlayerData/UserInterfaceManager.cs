using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance;

    [SerializeField] ArmourTab _armourTab;
    [SerializeField] CurrencyPanel _currencyPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void InitManager()
    {
        _armourTab.InitTab();
        _currencyPanel.InitPanel();
    }
}