using UnityEngine;

public class AssaultRifle : Weapon
{
    [SerializeField] WeaponType _weaponType;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletSpawnPoint;

    public override WeaponType weaponType => _weaponType;

    // Config data
    float _coolDown;
    float _damagePoints;
    float _emission;

    // Private data
    bool _isShootingContinuesly;
    float _currentCoolDown;
    


    public override void SetGunStats()
    {
        _coolDown = 0.15f;
        _damagePoints = 5;
        _emission = 0.1f;
    }

    public override void StartShootingContinuesly() => _isShootingContinuesly = true;

    public override void StopShootingContinuesly() => _isShootingContinuesly = false;



    void Update()
    {
        UpdateCoolDown();

        // Try to shoot
        if (_isShootingContinuesly)
        {
            if (_currentCoolDown <= 0)
            {
                ShootTheGun();
                ResetCoolDown();
            }
        }
    }

    void UpdateCoolDown()
    {
        if (_currentCoolDown > 0)
        {
            _currentCoolDown -= Time.deltaTime;
        }
    }

    void ResetCoolDown()
    {
        _currentCoolDown = _coolDown;
    }

    public override void ShootTheGun()
    {
        var bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        bullet.GetComponent<Bullet>().LaunchBullet(_emission);
    }
}