using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float _lifeTime = 3f;
    [SerializeField] float _bulletSpeed;
    [SerializeField] Rigidbody _rb;
    [SerializeField] RocketExplosion _explosionPrefab;

    [Header("Visuals")]
    [SerializeField] ParticleSystem _destroyParticle;
    


    void Start()
    {
        DelayDestruction();
    }

    public void LaunchRocket()
    {
        Vector3 bulletDirection = transform.forward;
        _rb.velocity = bulletDirection * _bulletSpeed;
    }




    // Projectile Collision Logic
    void OnCollisionEnter(Collision collision) {

        // Get collider tag of object we reach
        string colliderTag = collision.gameObject.tag;

        switch (colliderTag)
        {
            case TagConstraintsConfig.ENVIRONMENT_TAG:
            DestroyBullet();
            break;
            

            case TagConstraintsConfig.DESTRUCTIBLE_UNIT_TAG:
            var data = collision.gameObject.GetComponent<BasicDestructibleUnit>();
            if (data != null)
            {
                data.TakeDamage(10);
            }
            DestroyBullet();
            break;
            

            case TagConstraintsConfig.ENEMY_TAG:
            var enemyData = collision.gameObject.GetComponent<BasicEnemy>();
            if (enemyData != null)
            {
                enemyData.TakeDamage(10);
            }
            DestroyBullet();
            break;
        }
    }





    // Destruction logic
    async void DelayDestruction()
    {
        int delayBeforeDestruction = (int)_lifeTime * 1000;
        await Task.Delay(delayBeforeDestruction);

        if (this != null)
        {
            DestroyBullet();
        }
    }
    
    public void DestroyBullet()
    {
        // Place particles
        GameObject.Instantiate(_destroyParticle.gameObject, transform.position, Quaternion.identity);
        GameObject.Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}