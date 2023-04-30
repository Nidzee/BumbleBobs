using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyCrystalsWidget : MonoBehaviour
{
    [SerializeField] Text _crystalsLabel;


    public void SetCurrencyDisplay()
    {
        int crystals = PlayerDataManager.Instance.PlayerData.CurrencyData.CrystalsAmount;
        _crystalsLabel.text = crystals.ToString();
    }
}
