using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDataWidget : MonoBehaviour
{
    [SerializeField] Text _levelLabel;
    [SerializeField] Text _stepLabel;
    [SerializeField] Text _priceLabel;

    [SerializeField] Button _upgradeButton;
    public Button UpgradeButton => _upgradeButton;


    public void Init()
    {
        // Set only level and step
        PlayerSaveData_Health healthData = PlayerDataManager.Instance.PlayerData.HealthData;
        _levelLabel.text = healthData.HelathLevel.ToString();
        _stepLabel.text = healthData.HelathLevelStep.ToString();
    }

    public void RefreshPurchaseButton()
    {
        // Check if top config reached
        PlayerSaveData_Health healthData = PlayerDataManager.Instance.PlayerData.HealthData;
        if (HealthDataManager.Instance.IsTopConfig(healthData))
        {
            _upgradeButton.enabled = false;
            _priceLabel.text = "TOP REACHED";
            return;
        }



        int coinsAmount = PlayerDataManager.Instance.PlayerData.CurrencyData.CoinsAmount;
        int upgradePrice = HealthDataManager.Instance.GetUpgradePrice();


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
