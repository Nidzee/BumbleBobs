using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponType _weaponType;
    [SerializeField] GameObject _bulletPrefab;

    public WeaponType weaponType => _weaponType;

    // Config data
    float _coolDown;
    float _damagePoints;


    // Private data
    bool _isShootingContinuesly;
    float _currentCoolDown;
    

    // Eevnt trigger [PRESS BUTTON] - shoot once
    // Event trigger [JOYSTICK SHOOT POSITION] - start shooting continuesly
    // Event trigger [JOYSTICK NOT SHOOT POSITION] - end shooting continuesly



    public void Init()
    {
        // Reset data
        _isShootingContinuesly = false;
        _currentCoolDown = 0;


        // Set from config
        _coolDown = 1f;
        _damagePoints = 5f;
    }

    public void StartShootingContinuesly()
    {
        _isShootingContinuesly = true;
    }

    public void StopShootingContinuesly()
    {
        _isShootingContinuesly = false;
    }

    void Update()
    {
        if (_isShootingContinuesly)
        {
            if (_currentCoolDown <= 0)
            {
                ShootTheGun();
                ResetCoolDown();
            }
        }



        if (_currentCoolDown > 0)
        {
            _currentCoolDown -= Time.deltaTime;
        }
    }

    public void ShootOnce()
    {
        // Skip if we are shooting
        if (_isShootingContinuesly)
        {
            Debug.LogException(new System.Exception("Trying to shoot once while shooting continuesly. Shooting aborted."));
            return;
        }


        // If no cooldown - shoot
        if (_currentCoolDown <= 0)
        {
            ShootTheGun();
            ResetCoolDown();
        }
    }

    void ResetCoolDown()
    {
        _currentCoolDown = _coolDown;
    }

    public void ShootTheGun()
    {
        // Create 3 projectiles and throw them
    }
}