using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericGameDataSystemManager : MonoBehaviour
{
    // Class reference
    public static GenericGameDataSystemManager Instance;

    // Data customization in inspector
    [SerializeField] List<ProgressDataCollection> _healthDataConfiguration;
    Dictionary<int, ProgressDataCollection> _healtDataCache = new Dictionary<int, ProgressDataCollection>();

    // Data customization in inspector
    [SerializeField] List<ProgressDataCollection> _armourDataConfiguration;
    Dictionary<int, ProgressDataCollection> _armourDataCache = new Dictionary<int, ProgressDataCollection>();



    void Start()
    {
        Instance = this;

        BuildHealthDataCache();
        BuildArmourDataCache();
    }

    void BuildHealthDataCache()
    {
        // Skip if no data provided
        if (_healthDataConfiguration == null || _healthDataConfiguration?.Count <= 0)
        {
            Debug.LogException(new System.Exception("GenericGameDataSystemManager. No data provided: _healthDataConfiguration."));
            return;
        }


        // Clear the cache
        _healtDataCache = new Dictionary<int, ProgressDataCollection>();

        // Build cache for easy use
        foreach (var config in _healthDataConfiguration)
        {
            _healtDataCache[config.levelNumber] = config;
        }
    }
    
    void BuildArmourDataCache()
    {
        // Skip if no data provided
        if (_armourDataConfiguration == null || _armourDataConfiguration?.Count <= 0)
        {
            Debug.LogException(new System.Exception("GenericGameDataSystemManager. No data provided: _armourDataConfiguration."));
            return;
        }


        // Clear the cache
        _armourDataCache = new Dictionary<int, ProgressDataCollection>();

        // Build cache for easy use
        foreach (var config in _armourDataConfiguration)
        {
            _armourDataCache[config.levelNumber] = config;
        }
    }
}