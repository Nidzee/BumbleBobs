using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemManager : MonoBehaviour
{
    // Class reference
    public static WeaponSystemManager Instance;

    // Data customization in inspector
    [SerializeField] List<WeaponTypeConfiguration> _weaponDataConfiguration;
    Dictionary<WeaponType, WeaponTypeData> _weaponTypeDataCache = new Dictionary<WeaponType, WeaponTypeData>();



    void Start()
    {
        Instance = this;

        BuildWeaponTypeDataCache();
    }

    void BuildWeaponTypeDataCache()
    {
        // Skip if no data provided
        if (_weaponDataConfiguration == null || _weaponDataConfiguration?.Count <= 0)
        {
            Debug.LogException(new System.Exception("WeaponSystemManager. No data provided: _weaponDataConfiguration."));
            return;
        }


        _weaponTypeDataCache = new Dictionary<WeaponType, WeaponTypeData>();

        foreach (var config in _weaponDataConfiguration)
        {
            if (!config.IsConfigValid())
            {
                Debug.LogError("Invalid weapon config.");
                continue;
            }

            config.BuildCache();
            _weaponTypeDataCache[config.weaponType] = config.weaponTypeConfiguration;
        }
    }

    public WeaponStats GetWeaponStats(WeaponType type, int level, int step)
    {
        if (!_weaponTypeDataCache.ContainsKey(type))
        {
            Debug.LogException(new System.Exception("No data-type in cache."));
            return null;
        }

        WeaponTypeData weaponData = _weaponTypeDataCache[type];
        WeaponStats stats = weaponData.GetWeaponStats(level, step);

        if (stats == null)
        {
            Debug.LogException(new System.Exception("No stats by level and step in cache."));
            return null;
        }

        return stats;
    }
}

public enum WeaponType
{
    None = 0,
    AssaultRifle = 1,
    GrenadeLauncher = 2,
    LaserGun = 3,
}