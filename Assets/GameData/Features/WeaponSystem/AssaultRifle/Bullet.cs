using System.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _lifeTime = 3f;
    [SerializeField] float _bulletSpeed;
    [SerializeField] Rigidbody _rb;
    

    void Start()
    {
        DelayDestruction();
    }

    public void LaunchBullet(float emission)
    {
        Vector3 bulletDirection = transform.forward + new Vector3(
            Random.Range(-emission, emission),
            0,
            Random.Range(-emission, emission)
        );

        _rb.velocity = bulletDirection * _bulletSpeed;
    }

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

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    async void DelayDestruction()
    {
        int delayBeforeDestruction = (int)_lifeTime * 1000;
        await Task.Delay(delayBeforeDestruction);

        if (this != null)
        {
            DestroyBullet();
        }
    }
}