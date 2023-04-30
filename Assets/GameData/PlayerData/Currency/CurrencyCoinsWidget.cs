using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyCoinsWidget : MonoBehaviour
{
    [SerializeField] Text _coinsLabel;


    public void SetCurrencyDisplay()
    {
        int coinsAmount = PlayerDataManager.Instance.PlayerData.CurrencyData.CoinsAmount;
        _coinsLabel.text = coinsAmount.ToString();
    }
}