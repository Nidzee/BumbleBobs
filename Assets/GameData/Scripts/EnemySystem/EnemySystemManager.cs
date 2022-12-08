using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystemManager : MonoBehaviour
{
    // Class reference
    public static EnemySystemManager Instance;

    // Data customization in inspector
    [SerializeField] List<EnemyTypeStatsConfiguration> _enemyTypeStatsConfiguration = new List<EnemyTypeStatsConfiguration>();
    Dictionary<EnemyType, EnemyStats> _enemyTypeStatsCache = new Dictionary<EnemyType, EnemyStats>();



    void Start()
    {
        Instance = this;

        BuildEnemyTypeStatsCache();
    }

    void BuildEnemyTypeStatsCache()
    {
        // Skip if no data provided
        if (_enemyTypeStatsConfiguration == null || _enemyTypeStatsConfiguration?.Count <= 0)
        {
            Debug.LogException(new System.Exception("EnemyConfigManager. No data provided: _enemyTypeStatsList."));
            return;
        }


        // Clear the cache
        _enemyTypeStatsCache = new Dictionary<EnemyType, EnemyStats>();

        // Build cache for easy use
        foreach (var config in _enemyTypeStatsConfiguration)
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
            // TODO: return default value
            Debug.LogError("Error! Missing enemy-stats data for type: " + type);
            return null;
        }
        
        return _enemyTypeStatsCache[type];
    }
}

public enum EnemyCategory
{
    None = 0,
    Category1 = 1,
    Category2 = 2,
    Category3 = 3,
}

public enum EnemyType
{
    None = 0,
    Type1 = 1,
    Type2 = 2,
    Type3 = 3,
}

public enum DamageType
{
    ReduceHealthOnly,
    ReduceArmourOnly,
    ReduceAll,
}