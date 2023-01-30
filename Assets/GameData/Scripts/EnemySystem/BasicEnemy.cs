using UnityEngine;

public class BasicEnemy : AliveUnit, IEnemy, ICanDropItem
{
    [SerializeField] EnemyType _enemyType;
    [SerializeField] DamageType _damageType;
    [SerializeField] LootBag _lootBag;

    public DamageType damageType { get => _damageType; }
    public LootBag lootBag { get => _lootBag; }



    public void Start()
    {
        InitEnemyStats();
    }

    public void InitEnemyStats()
    {
        EnemyStats stats = EnemySystemManager.Instance.GetEnemyStats(_enemyType);
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
        if (Armour > 0 )
        {
            Armour -= damagePoints;
            if (Armour <= 0)
            {
                Armour = 0;
            }
            return;
        }


        Health -= damagePoints;
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Attack(){}

    public override void Die()
    {
        DropLoot();
        Destroy(gameObject);
    }

    public void DropLoot()
    {
        lootBag?.DropLoot();
    }



    
    public void OnCollisionEnter(Collision collision)
    {
        var bulletData = collision.gameObject.GetComponent<Bullet>();
        if (bulletData != null)
        {
            TakeDamage(10);
            bulletData.DestroyBullet();
        }
    }
}