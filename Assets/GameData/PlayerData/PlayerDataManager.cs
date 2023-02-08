using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;
    public PlayerSaveData PlayerData;

    [HideInInspector] public UnityEvent OnDataChanged;



    void Awake()
    {
        Instance = this;
    }

    public void InitManager()
    {
        ReadPlayerData();
        ShrinkTheData();
    }

    public void ShrinkTheData()
    {
        bool isShrinked = false;
        
        // Try to shrink armour data
        PlayerSaveData_Armour savedData = PlayerData.ArmourData.GetCopy();
        PlayerSaveData_Armour shrinkedData = ArmourDataManager.Instance.ShrinkToConfigBounds(savedData);
        if (!savedData.IsEqual(shrinkedData))
        {
            PlayerData.ArmourData = shrinkedData;
            isShrinked = true;
        }



        // Save to backend shrinked version
        if (isShrinked)
        {
            Debug.Log("[Player-Data-Manager] Data was shrinked on start.");
            SavePlayerData();
        }
    }

    // Read player data from backend or file
    public void ReadPlayerData()
    {
        PlayerData = new PlayerSaveData();
        PlayerData.ArmourData = new PlayerSaveData_Armour();
        PlayerData.CurrencyData = new PlayerSaveData_Currency() {CoinsAmount = 350, CrystalsAmount = 10};
        PlayerData.HealthData = new PlayerSaveData_Health();
        PlayerData.WeaponData = new PlayerSaveData_Weapon();
    }

    // Save player data to backend or file
    public void SavePlayerData()
    {

    }






    // Operations
    public void TryToAddCoins(int addAmount)
    {
        PlayerData.CurrencyData.CoinsAmount += addAmount;

        // Save to backend

        OnDataChanged.Invoke();
    }

    public void TryToRemoveCoins(int removeAmount)
    {
        if (PlayerData.CurrencyData.CoinsAmount < removeAmount)
        {
            Debug.Log("[Player Data Manager] Can not remove coins. Not enough coins.");
            return;
        }

        PlayerData.CurrencyData.CoinsAmount -= removeAmount;
        
        // Save to backend

        OnDataChanged.Invoke();
    }

    public void TryToUpgradeArmour(int price)
    {
        if (PlayerData.CurrencyData.CoinsAmount < price)
        {
            Debug.Log("[Player Data Manager] Can not upgrade armour. Not enough coins.");
            return;
        }


        Debug.Log("[Player Data Manager] Upgrade armour operation.");
        PlayerData.CurrencyData.CoinsAmount -= price;
        PlayerData.ArmourData = ArmourDataManager.Instance.GetUpgradedData();


        // Save to backend


        OnDataChanged.Invoke();
    }
}