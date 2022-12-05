using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : AliveUnit, IEnemy, ICanDropItem
{
    [SerializeField] EnemyType _enemyType;
    [SerializeField] LootBag _lootBag;
    public LootBag lootBag { get; set;}


    // Init enemy data after spawn
    public void init()
    {
        lootBag = _lootBag;
        InitEnemyDataFromConfig();
    }

    public void InitEnemyDataFromConfig()
    {
        // Get stats by enemy type
        EnemyStats stats = EnemyConfigManager.Instance.GetEnemyStats(_enemyType);
        
        // Check if stats is provided
        if (stats == null)
        {
            Debug.LogException(new System.Exception("Missing stats for enemy initialization."));
            return;
        }

        // Set stats data for entity
        SetEnemyStats(stats);
    }

    void SetEnemyStats(EnemyStats stats)
    {
        Health = stats.maxHealthPoints;
        Armour = stats.maxArmourPoints;
    }



    public override void TakeDamage(int damagePoints)
    {
    }

    public void Attack()
    {
    }

    public override void Die()
    {
    }

    public void DropLoot()
    {
        lootBag?.DropLoot();
    }
}