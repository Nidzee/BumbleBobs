using UnityEngine;

public class BasicDestructibleUnit : AliveUnit, ICanDropItem
{
    [Header("Main components config")]
    [SerializeField] LootBag _lootBag;
    [SerializeField] DestructibleUnitType _unitType;

    [Header("Destroy sound")]
    [SerializeField] AudioClip _destroySound;


    public LootBag lootBag { get => _lootBag; }



    public void Start()
    {
        // Init health data from stats
        DestructibleUnitStats stats = DestructibleUnitsSystemManager.Instance.GetDestructibleUnitStats(_unitType);
        Health = stats.maxHealthPoints;
    }

    public override void TakeDamage(int damagePoints)
    {
        if (Health <= 0)
        {
            return;
        }


        Health -= damagePoints;
        if (Health <= 0)
        {
            Die();
        }
    }
    
    public void DropLoot()
    {
        lootBag?.DropLoot();
    }

    void PlayDestroySound()
    {
        AudioSource.PlayClipAtPoint(_destroySound, transform.position);
    }

    
    
    public override void Die()
    {
        PlayDestroySound();
        DropLoot();
        
        Destroy(this.gameObject);
    }
}