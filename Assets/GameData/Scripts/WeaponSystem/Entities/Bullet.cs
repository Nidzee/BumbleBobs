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
        
        if (collision.gameObject.tag == TagConstraintsConfig.OBSTACLE_TAG)
        {
            DestroyBullet();
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
