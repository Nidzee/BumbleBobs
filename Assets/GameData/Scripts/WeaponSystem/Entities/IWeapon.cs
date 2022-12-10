using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public WeaponType weaponType {get;}
    public void ShootTheGun();
    public void SetGunStats();
}