using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyTypeStatsConfiguration
{
    public EnemyType enemyType;
    public EnemyStats stats;

    public bool IsConfigValid()
    {
        if (enemyType != EnemyType.None && stats != null)
        {
            return true;
        }

        return false;
    }
}