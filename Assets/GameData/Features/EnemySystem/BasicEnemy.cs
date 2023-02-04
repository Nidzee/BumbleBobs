using UnityEngine;

public class BasicEnemy : AliveUnit, IEnemy, ICanDropItem
{
    [Header("Enemy config")]
    [SerializeField] EnemyType _enemyType;
    [SerializeField] DamageType _damageType;
    
    [Header("Info config")]    
    [SerializeField] InfoBar _healthInfoBar;
    [SerializeField] InfoBar _armourInfoBar;

    [Header("Loot drop config")]
    [SerializeField] LootBag _lootBag;

    [Header("Death sound")]
    [SerializeField] AudioClip _deathSound;

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

        _healthInfoBar.InitInfoBar(Health, Health);
        _armourInfoBar.InitInfoBar(Armour, Armour);
    }








    public override void TakeDamage(int damagePoints)
    {
        Health -= damagePoints;
        _healthInfoBar.ReduceValue(damagePoints);
        
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Attack(){}

    public override void Die()
    {
        PlayDeathSound();
        DropLoot();
        Destroy(gameObject);
    }

    public void DropLoot()
    {
        lootBag?.DropLoot();
    }
    
    void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(_deathSound, transform.position);
    }
}