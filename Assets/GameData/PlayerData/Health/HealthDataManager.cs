using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HealthDataManager : MonoBehaviour
{
    // Static reference
    public static HealthDataManager Instance;
    
    void Awake()
    {
        Instance = this;
    }


    // Manager data
    [SerializeField] HealthDataConfig _healthDataConfig;
    Dictionary<int, HealthLevelDataConfig> _healthLevelCache;

    // Copy of actual player data -> used to detect if data was updated -> to refresh UI
    PlayerSaveData_Health _healthSaveDataCopy;

    // Manager events
    [HideInInspector] public UnityEvent OnDataChanged_Health;



    public void InitManager()
    {
        BuildManagerCache();
        _healthSaveDataCopy = PlayerDataManager.Instance.PlayerData.HealthData.GetCopy();
        PlayerDataManager.Instance.OnDataChanged.AddListener(OnPlayerGameDataChanged);
    }

    void BuildManagerCache()
    {
        // Skip if no data provided
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
            config.BuildCache();
            _healthLevelCache[i] = config;
        }
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