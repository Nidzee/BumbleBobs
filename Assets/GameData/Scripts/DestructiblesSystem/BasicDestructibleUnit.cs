using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDestructibleUnit : AliveUnit, ICanDropItem
{
    [SerializeField] DestructibleUnitType _destructibleUnitType;
    [SerializeField] LootBag _lootBag;

    public LootBag lootBag { get => _lootBag; }


    public override void TakeDamage(int damagePoints)
    {
        if (_destructibleUnitType == DestructibleUnitType.InstantDestroy)
        {
            Die();
        } 
        else if (_destructibleUnitType == DestructibleUnitType.NotInstantDestroy) 
        {
            Health -= damagePoints;
            if (Health <= 0)
            {
                Die();
            }
        } 
        else
        {
            // Unknown type passed
            Debug.LogException(new System.Exception("Unknown destructible-unit-type set. Damage logic aborted."));
            return;
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
}