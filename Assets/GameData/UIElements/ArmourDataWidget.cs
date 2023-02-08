using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmourDataWidget : MonoBehaviour
{
    [SerializeField] Text _levelLabel;
    [SerializeField] Text _stepLabel;
    [SerializeField] Text _priceLabel;

    [SerializeField] Button _upgradeButton;
    public Button UpgradeButton => _upgradeButton;


    public void Init()
    {
        // Set only level and step
        PlayerSaveData_Armour armourData = PlayerDataManager.Instance.PlayerData.ArmourData;
        _levelLabel.text = armourData.ArmourLevel.ToString();
        _stepLabel.text = armourData.ArmourLevelStep.ToString();
    }

    public void RefreshPurchaseButton()
    {
        // Check if top config reached
        PlayerSaveData_Armour armourData = PlayerDataManager.Instance.PlayerData.ArmourData;
        if (ArmourDataManager.Instance.IsTopConfig(armourData))
        {
            _upgradeButton.enabled = false;
            _priceLabel.text = "TOP REACHED";
            return;
        }



        int coinsAmount = PlayerDataManager.Instance.PlayerData.CurrencyData.CoinsAmount;
        int upgradePrice = ArmourDataManager.Instance.GetUpgradePrice();


        if (coinsAmount >= upgradePrice)
        {
            _upgradeButton.enabled = true;
            _priceLabel.text = upgradePrice + "$";
        }
        else
        {
            _upgradeButton.enabled = false;
            _priceLabel.text = "NOT ENOUGH";
        }
    }
}