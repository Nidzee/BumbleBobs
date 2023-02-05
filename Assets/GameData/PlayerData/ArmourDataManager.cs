using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmourDataManager : MonoBehaviour
{
    public static ArmourDataManager Instance;
    public UnityEvent OnDataChanged_Armour;
    PlayerSaveData_Armour _armourSaveDataCopy;

    void Start()
    {
        InitManager();
    }

    void InitManager()
    {
        Instance = this;
        _armourSaveDataCopy = PlayerDataManager.Instance.PlayerData.ArmourData.GetCopy();
        PlayerDataManager.Instance.OnDataChanged.AddListener(OnPlayerGameDataChanged);
    }





    void OnPlayerGameDataChanged()
    {
        var actualData = PlayerDataManager.Instance.PlayerData.ArmourData;
        bool isDataChanged = actualData.IsEqual(_armourSaveDataCopy);

        if (isDataChanged)
        {
            _armourSaveDataCopy = actualData.GetCopy();
            OnDataChanged_Armour.Invoke();
        }
    }
}