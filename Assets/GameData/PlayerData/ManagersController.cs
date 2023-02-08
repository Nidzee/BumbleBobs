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
        PlayerDataManager.Instance.InitManager(); // Read and shrink data
        ArmourDataManager.Instance.InitManager(); // Save copy and subscribe to events
        CurrencyDataManager.Instance.InitManager();

        // Temporary
        UserInterfaceManager.Instance.InitManager();
    }
}