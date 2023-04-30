using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;
    
    void Awake()
    {
        Instance = this;
    }



    public PlayerSaveData PlayerData;

    [HideInInspector] public UnityEvent OnDataChanged;



    public void InitManager()
    {
        ReadPlayerData();
    }

    // Read player data from backend or file
    void ReadPlayerData()
    {
        PlayerData = new PlayerSaveData();

        PlayerData.ArmourData = new PlayerSaveData_Armour() {ArmourLevel = 1, ArmourLevelStep = 6} ;
        PlayerData.CurrencyData = new PlayerSaveData_Currency() {CoinsAmount = 350, CrystalsAmount = 10};

        PlayerData.HealthData = new PlayerSaveData_Health();
        PlayerData.WeaponData = new PlayerSaveData_Weapon();
    }


    // Save player data to backend or file
    public void SavePlayerData()
    {

    }






    // Currency operations
    public void TryToAddCurrency(CurrencyType type, int addAmount)
    {
        if (type == CurrencyType.Coins)
        {
            PlayerData.CurrencyData.CoinsAmount += addAmount;
        } 
        else if (type == CurrencyType.Crystals)
        {
            PlayerData.CurrencyData.CrystalsAmount += addAmount;
        }

        // Save to backend
        SavePlayerData();

        OnDataChanged.Invoke();
    }

    public void TryToRemoveCurrency(CurrencyType type, int removeAmount)
    {
        if (type == CurrencyType.Coins)
        {
            if (PlayerData.CurrencyData.CoinsAmount < removeAmount)
            {
                Debug.Log("[Player Data Manager] Can not remove coins. Not enough coins.");
                return;
            }

            PlayerData.CurrencyData.CoinsAmount -= removeAmount;
        } 
        else if (type == CurrencyType.Crystals)
        {
            if (PlayerData.CurrencyData.CrystalsAmount < removeAmount)
            {
                Debug.Log("[Player Data Manager] Can not remove crystals. Not enough coins.");
                return;
            }
            
            PlayerData.CurrencyData.CrystalsAmount -= removeAmount;
        }
        
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

        var upgradedData = ArmourDataManager.Instance.GetUpgradedData();
        if (upgradedData == null)
        {
            Debug.LogError("[Player Data Manager] Error Armour Upgrading. Aborted.");
            return;
        }


        Debug.Log("[Player Data Manager] Upgrade armour operation.");
        PlayerData.CurrencyData.CoinsAmount -= price;
        PlayerData.ArmourData = upgradedData;


        // Save to backend
        SavePlayerData();


        OnDataChanged.Invoke();
    }

    public void TryToUpgradeHealth(int price)
    {
       
    }












    public void ShrinkPlayerData()
    {
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
}