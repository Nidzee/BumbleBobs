using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HealthDataManager : MonoBehaviour
{
    // Static reference
    public static HealthDataManager Instance;
    

    // Manager data
    [SerializeField] HealthDataConfig _healthDataConfig;
    PlayerSaveData_Health _healthSaveDataCopy;
    Dictionary<int, HealthLevelDataConfig> _healthLevelCache;
    PlayerSaveData_Health _topSaveConfig;

    // Manager events
    [HideInInspector] public UnityEvent OnDataChanged_Health;


    void Awake()
    {
        Instance = this;
        BuildManagerCache();
    }

    void BuildManagerCache()
    {
        if (_healthDataConfig == null ||  _healthDataConfig.HealthLevelsConfigCollection == null ||  _healthDataConfig.HealthLevelsConfigCollection.Count <= 0)
        {
            Debug.LogError("[Health-Data-Manager] Data is not provided.");
            return;
        }


        // Clear the cache
        _healthLevelCache = new Dictionary<int, HealthLevelDataConfig>();

        // Build the cache
        for (int i = 0; i < _healthDataConfig.HealthLevelsConfigCollection.Count; i++)
        {
            // Check if config is valid
            var config = _healthDataConfig.HealthLevelsConfigCollection[i];
            if (!config.IsValid())
            {
                Debug.LogError("[Health-Data-Manager] Config is invalid.");
                i--;
                continue;
            }

            config.BuildCache();
            _healthLevelCache[i] = config;
        }

        var topConfig = _healthLevelCache.LastOrDefault();
        _topSaveConfig = new PlayerSaveData_Health()
        {
            HelathLevel = topConfig.Key,
            HelathLevelStep = (topConfig.Value.GetStepsAmount()-1)
        };
    }

    public PlayerSaveData_Health ShrinkToConfigBounds(PlayerSaveData_Health healthData)
    {
        // Check if cache was build
        if (_healthLevelCache == null || _healthLevelCache.Count <= 0)
        {
            Debug.LogError("[Health-Data-Manager] Cannot shrink data. No cache builded.");
            return new PlayerSaveData_Health();
        }



        int playerLevel = healthData.HelathLevel;
        int playerSteps = healthData.HelathLevelStep;


        if (!_healthLevelCache.ContainsKey(playerLevel))
        {
            int topLevelNumber = _healthLevelCache.LastOrDefault().Key;

            Debug.Log("[Health-Data-Manager] [1] Shrink the level number.");

            if (playerLevel < 0)
            {
                Debug.Log("[Health-Data-Manager] [1] Level lower than 0. Set as 0.");
                playerLevel = 0;
            }
            else if (playerLevel > topLevelNumber)
            {
                Debug.Log("[Health-Data-Manager] [1] Level greater than top. Set as top.");
                playerLevel = topLevelNumber;
            } 
            else
            {
                Debug.LogError("[Health-Data-Manager] Impossible bug. Level number must be inside bounds.");
            }
        }


        // Gect player level config data
        var levelData = _healthLevelCache[playerLevel];


        int playerLevelConfigStepsAmount = levelData.GetStepsAmount();
        int topIndex = playerLevelConfigStepsAmount-1;
        if (playerSteps < 0 || playerSteps > topIndex)
        {
            Debug.Log("[Health-Data-Manager] [1] Shrink the step number.");

            if (playerSteps < 0)
            {
                Debug.Log("[Health-Data-Manager] [1] Step lower than 0. Set as 0.");
                playerSteps = 0;
            }
            else if (playerSteps > topIndex)
            {
                Debug.Log("[Health-Data-Manager] [1] Step greater than top. Set as top.");
                playerSteps = topIndex;
            } 
            else
            {
                Debug.LogError("[Health-Data-Manager] Impossible bug. Step number must be inside bounds.");
            }
        }


        // Return shrink data
        return new PlayerSaveData_Health()
        {
            HelathLevel = playerLevel,
            HelathLevelStep = playerSteps,
        };
    }











    public void InitManager()
    {
        _healthSaveDataCopy = PlayerDataManager.Instance.PlayerData.HealthData.GetCopy();
        PlayerDataManager.Instance.OnDataChanged.AddListener(OnPlayerGameDataChanged);
    }

    void OnPlayerGameDataChanged()
    {
        Debug.Log("[Health-Data-Manager] OnDataChanged triggered.");
        var actualData = PlayerDataManager.Instance.PlayerData.HealthData;
        bool isDataEqual = actualData.IsEqual(_healthSaveDataCopy);

        if (!isDataEqual)
        {
            _healthSaveDataCopy = actualData.GetCopy();
            Debug.Log("[Health-Data-Manager] Data updated. OnDataChanged_Health() triggered.");
            OnDataChanged_Health.Invoke();
        }
    }











    public bool IsTopConfig(PlayerSaveData_Health data)
    {
        return data.IsEqual(_topSaveConfig);
    }

    public PlayerSaveData_Health GetUpgradedData()
    {
        int currentLevel = _healthSaveDataCopy.HelathLevel;
        int currentStep = _healthSaveDataCopy.HelathLevelStep;
        int newLevel = 0;
        int newStep = 0;


        if (!_healthLevelCache.ContainsKey(currentLevel))
        {
            Debug.LogError("[Health-Data-Manager] Player-Level is out of config bounds.");
            // Shrink and resave here
            return null;
        }


        var levelData = _healthLevelCache[currentLevel];
        int maxLevel = _healthLevelCache.LastOrDefault().Key;
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
                Debug.LogError("Try to increase health. We are on top value.");
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



        return new PlayerSaveData_Health()
        {
            HelathLevel = newLevel,
            HelathLevelStep = newStep
        };
    }











    public int GetUpgradePrice()
    {
        return 100;
    } 

    public void TryToUpgradeHealth()
    {
        Debug.Log("[Health-Data-Manager] Try to upgrade health.");
        PlayerDataManager.Instance.TryToUpgradeHealth(GetUpgradePrice());
    }
}