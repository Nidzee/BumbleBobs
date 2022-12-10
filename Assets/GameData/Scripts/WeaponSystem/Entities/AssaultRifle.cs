using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AssaultRifle : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponType _weaponType;
    [SerializeField] GameObject _bulletPrefab;

    public WeaponType weaponType => _weaponType;

    // Config data
    float _coolDown;
    float _damagePoints;
    float _emission;


    // Private data
    bool _isShootingContinuesly;
    bool _isShootingOnce;
    float _currentCoolDown;
    

    // Event trigger [PRESS BUTTON] - shoot once
    // Event trigger [JOYSTICK SHOOT POSITION] - start shooting continuesly
    // Event trigger [JOYSTICK NOT SHOOT POSITION] - end shooting continuesly



    public void Init()
    {
        SetGunStats();
    }

    public void SetGunStats()
    {
        WeaponStats stats = WeaponSystemManager.Instance.GetWeaponStats(_weaponType, 1, 1);

        _coolDown = stats.cooldown;
        _damagePoints = stats.damagePoints;
        _emission = stats.emission;
    }

    public void StartShootingContinuesly() => _isShootingContinuesly = true;

    public void StopShootingContinuesly() => _isShootingContinuesly = false;

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
            ShootOnceAction();
        }
    }



    void Update()
    {
        if (_isShootingContinuesly && !_isShootingOnce)
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

    async void ShootOnceAction()
    {
        _isShootingOnce = true;

        for (int i = 0; i < 3; i++)
        {
            ShootTheGun();
            ResetCoolDown();
            
            await ShootOnceCoolDown();
        }

        _isShootingOnce = false;
    }

    async Task ShootOnceCoolDown()
    {
        while (_currentCoolDown > 0)
        {
            _currentCoolDown -= Time.deltaTime;
            await Task.Yield();
        }
    }

    void ResetCoolDown()
    {
        _currentCoolDown = _coolDown;
    }




    public void ShootTheGun()
    {
        // Create projectile and throw it
    }
}