using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : AliveUnit, IEnemy, ICanDropItem
{
    [SerializeField] EnemyCategory _enemyCategory;
    [SerializeField] EnemyType _enemyType;
    [SerializeField] DamageType _damageType;
    [SerializeField] LootBag _lootBag;

    public DamageType damageType { get => _damageType; }
    public LootBag lootBag { get => _lootBag; }


    // Init enemy data after spawn
    public void Init()
    {
        InitEnemyStats();
    }

    public void InitEnemyStats()
    {
        // Get stats by enemy type
        EnemyStats stats = EnemySystemManager.Instance.GetEnemyStats(_enemyType);

        // Set stats data for entity
        SetEnemyStats(stats);
    }

    void SetEnemyStats(EnemyStats stats)
    {
        // Check if stats is provided
        if (stats == null)
        {
            Debug.LogException(new System.Exception("Missing stats for enemy initialization."));
            return;
        }

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