using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ArmourDataManager : MonoBehaviour
{
    // Static reference
    public static ArmourDataManager Instance;
    

    // Manager data
    [SerializeField] ArmourDataConfig _armourDataConfig;
    PlayerSaveData_Armour _armourSaveDataCopy;
    Dictionary<int, ArmourLevelDataConfig> _armourLevelCache;
    PlayerSaveData_Armour _topSaveConfig;

    // Manager events
    [HideInInspector] public UnityEvent OnDataChanged_Armour;


    void Awake()
    {
        Instance = this;
        BuildManagerCache();
    }

    void BuildManagerCache()
    {
        if (_armourDataConfig == null ||  _armourDataConfig.ArmourLevelsConfigCollection == null ||  _armourDataConfig.ArmourLevelsConfigCollection.Count <= 0)
        {
            Debug.LogError("[Armour-Data-Manager] Data is not provided.");
            return;
        }


        // Clear the cache
        _armourLevelCache = new Dictionary<int, ArmourLevelDataConfig>();

        // Build the cache
        for (int i = 0; i < _armourDataConfig.ArmourLevelsConfigCollection.Count; i++)
        {
            // Check if config is valid
            var config = _armourDataConfig.ArmourLevelsConfigCollection[i];
            if (!config.IsValid())
            {
                Debug.LogError("[Armour-Data-Manager] Config is invalid.");
                i--;
                continue;
            }

            config.BuildCache();
            _armourLevelCache[i] = config;
        }

        var topConfig = _armourLevelCache.LastOrDefault();
        _topSaveConfig = new PlayerSaveData_Armour()
        {
            ArmourLevel = topConfig.Key,
            ArmourLevelStep = (topConfig.Value.GetStepsAmount()-1)
        };
    }

    public PlayerSaveData_Armour ShrinkToConfigBounds(PlayerSaveData_Armour armourData)
    {
        // Check if cache was build
        if (_armourLevelCache == null || _armourLevelCache.Count <= 0)
        {
            Debug.LogError("[Armour-Data-Manager] Cannot shrink data. No cache builded.");
            return new PlayerSaveData_Armour();
        }



        int playerLevel = armourData.ArmourLevel;
        int playerSteps = armourData.ArmourLevelStep;


        if (!_armourLevelCache.ContainsKey(playerLevel))
        {
            int topLevelNumber = _armourLevelCache.LastOrDefault().Key;

            Debug.Log("[Armour-Data-Manager] [1] Shrink the level number.");

            if (playerLevel < 0)
            {
                Debug.Log("[Armour-Data-Manager] [1] Level lower than 0. Set as 0.");
                playerLevel = 0;
            }
            else if (playerLevel > topLevelNumber)
            {
                Debug.Log("[Armour-Data-Manager] [1] Level greater than top. Set as top.");
                playerLevel = topLevelNumber;
            } 
            else
            {
                Debug.LogError("[Armour-Data-Manager] Impossible bug. Level number must be inside bounds.");
            }
        }


        // Gect player level config data
        var levelData = _armourLevelCache[playerLevel];


        int playerLevelConfigStepsAmount = levelData.GetStepsAmount();
        int topIndex = playerLevelConfigStepsAmount-1;
        if (playerSteps < 0 || playerSteps > topIndex)
        {
            Debug.Log("[Armour-Data-Manager] [1] Shrink the step number.");

            if (playerSteps < 0)
            {
                Debug.Log("[Armour-Data-Manager] [1] Step lower than 0. Set as 0.");
                playerSteps = 0;
            }
            else if (playerSteps > topIndex)
            {
                Debug.Log("[Armour-Data-Manager] [1] Step greater than top. Set as top.");
                playerSteps = topIndex;
            } 
            else
            {
                Debug.LogError("[Armour-Data-Manager] Impossible bug. Step number must be inside bounds.");
            }
        }


        // Return shrink data
        return new PlayerSaveData_Armour()
        {
            ArmourLevel = playerLevel,
            ArmourLevelStep = playerSteps,
        };
    }











    public void InitManager()
    {
        _armourSaveDataCopy = PlayerDataManager.Instance.PlayerData.ArmourData.GetCopy();
        PlayerDataManager.Instance.OnDataChanged.AddListener(OnPlayerGameDataChanged);
    }

    void OnPlayerGameDataChanged()
    {
        Debug.Log("[Armour-Data-Manager] OnDataChanged triggered.");
        var actualData = PlayerDataManager.Instance.PlayerData.ArmourData;
        bool isDataEqual = actualData.IsEqual(_armourSaveDataCopy);

        if (!isDataEqual)
        {
            _armourSaveDataCopy = actualData.GetCopy();
            Debug.Log("[Armour-Data-Manager] Data updated. OnDataChanged_Armour() triggered.");
            OnDataChanged_Armour.Invoke();
        }
    }











    public bool IsTopConfig(PlayerSaveData_Armour data)
    {
        return data.IsEqual(_topSaveConfig);
    }

    public PlayerSaveData_Armour GetUpgradedData()
    {
        int currentLevel = _armourSaveDataCopy.ArmourLevel;
        int currentStep = _armourSaveDataCopy.ArmourLevelStep;
        int newLevel = 0;
        int newStep = 0;


        if (!_armourLevelCache.ContainsKey(currentLevel))
        {
            Debug.LogError("[Armour-Data-Manager] Player-Level is out of config bounds.");
            // Shrink and resave here
            return null;
        }


        var levelData = _armourLevelCache[currentLevel];
        int maxLevel = _armourLevelCache.LastOrDefault().Key;
        int levelStepsAmount = levelData.GetStepsAmount();
        int maxStepIndex = levelStepsAmount-1;
        
        currentStep++;

        if (currentStep > maxStepIndex)
        {
            int nextLevel = currentLevel + 1;
            
            if (nextLevel <= maxLevel)
            {
                // Move to next level with step 0
                newLevel = nextLevel;
                newStep = 0;
            }
            else
            {
                Debug.LogError("Try to increase armour. We are on top value.");
                newLevel = maxLevel;
                newStep = maxStepIndex;
            }
        }
        else
        {
            // Same level, increase step
            newLevel = currentLevel;
            newStep = currentStep;
        }



        return new PlayerSaveData_Armour()
        {
            ArmourLevel = newLevel,
            ArmourLevelStep = newStep
        };
    }











    public int GetUpgradePrice()
    {
        return 100;
    } 

    public void TryToUpgradeArmour()
    {
        Debug.Log("[Armour-Data-Manager] Try to upgrade armour.");
        PlayerDataManager.Instance.TryToUpgradeArmour(GetUpgradePrice());
    }
}