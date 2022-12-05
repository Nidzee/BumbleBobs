using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConfigManager : MonoBehaviour
{
    [SerializeField] List<EnemyConfig> _enemyConfigList = new List<EnemyConfig>();
    Dictionary<EnemyType, EnemyStats>  _enemyTypeStats = new Dictionary<EnemyType, EnemyStats>();
    public static EnemyConfigManager Instance;



    void Start()
    {
        Instance = this;

        // Skip if no data provided
        if (_enemyConfigList == null)
        {
            Debug.LogException(new System.Exception("Missing enemy-config-list."));
            return;
        }

        // Skip if no data provided
        if (_enemyConfigList.Count <= 0)
        {
            Debug.LogException(new System.Exception("Missing data for enemy-config-list."));
            return;
        }


        // Form a dictionary for easy use
        foreach (var config in _enemyConfigList)
        {
            if (!config.IsConfigValid())
            {
                Debug.LogError("Invalid enemy config in enemy-config-list.");
                continue;
            }

            _enemyTypeStats[config.enemyType] = config.stats;
        }
    }

    public EnemyStats GetEnemyStats(EnemyType type)
    {
        if (!_enemyTypeStats.ContainsKey(type))
        {
            // TODO: return default value
            Debug.LogError("Error! Missing enemy-stats data for type: " + type);
            return null;
        }
        
        return _enemyTypeStats[type];
    }
}

public enum EnemyType
{
    None = -1,
    Type0 = 0,
    Type1 = 1,
    Type2 = 2,
    Type3 = 3,
}