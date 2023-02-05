using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;
    public PlayerSaveData PlayerData;

    public UnityEvent OnDataChanged;



    public void Awake()
    {
        Instance = this;
        ReadPlayerData();
    }

    // Read player data from backend or file
    public void ReadPlayerData()
    {
        PlayerData = new PlayerSaveData();
    }

    // Save player data to backend or file
    public void SavePlayerData()
    {

    }








}