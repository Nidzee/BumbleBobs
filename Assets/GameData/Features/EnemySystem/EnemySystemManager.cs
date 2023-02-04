using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    Type1 = 1,
}

public enum DamageType
{
    ReduceHealthOnly,
    ReduceArmourOnly,
    ReduceAll,
}


public class EnemySystemManager : MonoBehaviour
{
    // Class reference
    public static EnemySystemManager Instance;

    // Data customization in inspector
    [SerializeField] List<EnemyTypeStatsConfiguration> _enemyConfig = new List<EnemyTypeStatsConfiguration>();
    Dictionary<EnemyType, EnemyStats> _enemyTypeStatsCache = new Dictionary<EnemyType, EnemyStats>();



    void Awake()
    {
        Instance = this;
        BuildEnemySystemCache();
    }

    void BuildEnemySystemCache()
    {
        // Skip if no data provided
        if (_enemyConfig == null || _enemyConfig?.Count <= 0)
        {
            Debug.LogError("[EnemyConfigManager] No data provided.");
            return;
        }



        // Clear the cache
        _enemyTypeStatsCache = new Dictionary<EnemyType, EnemyStats>();

        // Build cache for easy use
        foreach (var config in _enemyConfig)
        {
            if (!config.IsConfigValid())
            {
                Debug.LogError("Invalid enemy config in enemy-config-list.");
                continue;
            }

            _enemyTypeStatsCache[config.enemyType] = config.stats;
        }
    }

    public EnemyStats GetEnemyStats(EnemyType type)
    {
        if (!_enemyTypeStatsCache.ContainsKey(type))
        {
            Debug.LogError("[EnemyConfigManager] Missing stats for type: " + type);
            return null;
        }
        
        return _enemyTypeStatsCache[type];
    }
}

[System.Serializable]
public class EnemyTypeStatsConfiguration
{
    public EnemyType enemyType;
    public EnemyStats stats;

    public bool IsConfigValid()
    {
        if (stats != null)
        {
            return true;
        }

        return false;
    }
}