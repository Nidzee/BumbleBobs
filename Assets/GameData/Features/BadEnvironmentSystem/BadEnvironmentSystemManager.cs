using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AgressiveEnvironmentType
{
    ToxicGas,
    Acid,
    Radiation,
}

public class BadEnvironmentSystemManager : MonoBehaviour
{
    // Class reference
    public static BadEnvironmentSystemManager Instance;
    
    
    // Data customization in inspector
    [SerializeField] List<BadEnvironmentConfiguration> _badEnvironmentConfigCollection = new List<BadEnvironmentConfiguration>();
    Dictionary<AgressiveEnvironmentType, BadEnvironmentStats> _badEnvironmentTypeStatsCache = new Dictionary<AgressiveEnvironmentType, BadEnvironmentStats>();

    
    
    
    void Awake()
    {
        Instance = this;   
        BuildEnemyTypeStatsCache(); 
    }

    void BuildEnemyTypeStatsCache()
    {
        // Skip if no data provided
        if (_badEnvironmentConfigCollection == null || _badEnvironmentConfigCollection?.Count <= 0)
        {
            Debug.LogException(new System.Exception("BadEnvironmentSystemManager. No data provided: _badEnvironmentConfigCollection."));
            return;
        }



        // Clear the cache
        _badEnvironmentTypeStatsCache = new Dictionary<AgressiveEnvironmentType, BadEnvironmentStats>();

        // Build cache for easy use
        foreach (var config in _badEnvironmentConfigCollection)
        {
            _badEnvironmentTypeStatsCache[config.Type] = config.Stats;
        }
    }

    public BadEnvironmentStats GetBadEnvironmentStats(AgressiveEnvironmentType type)
    {
        if (!_badEnvironmentTypeStatsCache.ContainsKey(type))
        {
            Debug.LogError("Error! Missing DestructibleUnitType-stats data for type: " + type);
            return null;
        }
        
        return _badEnvironmentTypeStatsCache[type];
    }
}


[System.Serializable]
public class BadEnvironmentConfiguration
{
    public AgressiveEnvironmentType Type;
    public BadEnvironmentStats Stats;
}


[System.Serializable]
public class BadEnvironmentStats
{
    public DamageType DamageType;
    public float DamagePoints;
    public int DamageIntervas_Miliseconds;
}