using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemManager : MonoBehaviour
{
    // Class reference
    public static WeaponSystemManager Instance;

    // Data customization in inspector
    [SerializeField] List<WeaponTypeConfiguration> _weaponDataConfiguration;
    Dictionary<WeaponType, WeaponTypeGameData> _weaponTypeDataCache = new Dictionary<WeaponType, WeaponTypeGameData>();



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


        // Clear the cache
        _weaponTypeDataCache = new Dictionary<WeaponType, WeaponTypeGameData>();

        // Build cache for easy use
        foreach (var config in _weaponDataConfiguration)
        {
            if (!config.IsConfigValid())
            {
                Debug.LogError("Invalid weapon config.");
                continue;
            }

            WeaponTypeGameData data = new WeaponTypeGameData(config.weaponType, config.dataConfiguration);

            _weaponTypeDataCache[config.weaponType] = data;
        }
    }
}

public enum WeaponType
{
    None = 0,
    AssaultRifle = 1,
    GrenadeLauncher = 2,
    LaserGun = 3,
}