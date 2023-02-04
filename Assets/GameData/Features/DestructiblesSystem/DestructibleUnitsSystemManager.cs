using System.Collections.Generic;
using UnityEngine;

public class DestructibleUnitsSystemManager : MonoBehaviour
{
    // Class reference
    public static DestructibleUnitsSystemManager Instance;

    // Data customization in inspector
    [SerializeField] List<DestructibleUnitTypeStatsConfiguration> _unitTypeStatsConfiguration = new List<DestructibleUnitTypeStatsConfiguration>();
    Dictionary<DestructibleUnitType, DestructibleUnitStats> _unitTypeStatsCache = new Dictionary<DestructibleUnitType, DestructibleUnitStats>();


    public void Awake()
    {
        Instance = this;
        BuildEnemyTypeStatsCache();
    }

    
    void BuildEnemyTypeStatsCache()
    {
        // Skip if no data provided
        if (_unitTypeStatsConfiguration == null || _unitTypeStatsConfiguration?.Count <= 0)
        {
            Debug.LogException(new System.Exception("DestructibleUnitsSystemManager. No data provided: _unitTypeStatsConfiguration."));
            return;
        }



        // Clear the cache
        _unitTypeStatsCache = new Dictionary<DestructibleUnitType, DestructibleUnitStats>();

        // Build cache for easy use
        foreach (var config in _unitTypeStatsConfiguration)
        {
            _unitTypeStatsCache[config.unitType] = config.stats;
        }
    }

    public DestructibleUnitStats GetEnemyStats(DestructibleUnitType type)
    {
        if (!_unitTypeStatsCache.ContainsKey(type))
        {
            Debug.LogError("Error! Missing DestructibleUnitType-stats data for type: " + type);
            return null;
        }
        
        return _unitTypeStatsCache[type];
    }
}

public enum DestructibleUnitType
{
    Crate,
    LootBox,
}

[System.Serializable]
public class DestructibleUnitTypeStatsConfiguration
{
    public DestructibleUnitType unitType;
    public DestructibleUnitStats stats;
}

[System.Serializable]
public class DestructibleUnitStats
{
    public int maxHealthPoints;
}