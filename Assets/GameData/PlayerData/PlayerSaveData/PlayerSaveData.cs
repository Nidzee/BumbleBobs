using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    private PlayerSaveData_Health _healthData;
    private PlayerSaveData_Armour _armourData;
    private PlayerSaveData_Weapon _weaponData;
    private PlayerSaveData_Currency _currencyData;


    public PlayerSaveData_Health HealthData => _healthData;
    public PlayerSaveData_Armour ArmourData => _armourData;
    public PlayerSaveData_Weapon WeaponData => _weaponData;
    public PlayerSaveData_Currency CurrencyData => _currencyData;
}