using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersController : MonoBehaviour
{
    public static ManagersController Instance;

    public void Awake()
    {
        Instance = this;
    }





    public void Start()
    {
        InitManagers();
    }

    void InitManagers()
    {
        // Read player data
        PlayerDataManager.Instance.InitManager();
        

        // Build cache
        // Save actual data
        // Subscribe to events
        ArmourDataManager.Instance.InitManager();

        
        CurrencyDataManager.Instance.InitManager();

        
        PlayerDataManager.Instance.ShrinkPlayerData();

        






        
        // Build cache
        // Save actual data
        // Subscribe to events
        HealthDataManager.Instance.InitManager();

        // Temporary
        UserInterfaceManager.Instance.InitManager();
    }
}