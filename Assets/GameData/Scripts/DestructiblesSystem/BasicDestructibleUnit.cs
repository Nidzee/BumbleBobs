using UnityEngine;

public class BasicDestructibleUnit : AliveUnit, ICanDropItem
{
    [SerializeField] LootBag _lootBag;
    [SerializeField] DestructibleUnitType _unitType;
    public LootBag lootBag { get => _lootBag; }


    public void Start()
    {
        // Init health data from stats
        DestructibleUnitStats stats = DestructibleUnitsSystemManager.Instance.GetEnemyStats(_unitType);
        Health = stats.maxHealthPoints;
        Armour = 0;
    }

    public override void TakeDamage(int damagePoints)
    {
        Health -= damagePoints;
        if (Health <= 0)
        {
            Die();
        }
    }
    
    public override void Die()
    {
        DropLoot();
        Destroy(this.gameObject);
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