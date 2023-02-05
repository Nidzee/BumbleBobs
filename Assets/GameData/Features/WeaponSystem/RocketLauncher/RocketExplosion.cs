using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    [Header("Explosion config")]
    [SerializeField] float _sphereRadius = 2f;
    [SerializeField] float _lifeTime = 1f;
    [SerializeField] int _eplosionDamage = 10;

    [Header("Visuals config")]
    [SerializeField] bool _showExplosionSphere = true;
    [SerializeField] GameObject _spherePrefab;
    GameObject _sphere;


    private void Start()
    {
        Explode();
        DelayDestruction();
    }

    private void Explode()
    {
        // Show sphere logic
        if (_showExplosionSphere)
        {
            _sphere = GameObject.Instantiate(_spherePrefab, transform.position, Quaternion.identity);
            _sphere.transform.localScale = new Vector3(_sphereRadius,_sphereRadius,_sphereRadius);
        }


        // Explosion logic
        Collider[] colliders = Physics.OverlapSphere(transform.position, _sphereRadius);
        foreach (var col in colliders)
        {
            var damageComponent = col.GetComponent<IDamageble>();
            if (damageComponent != null)
            {
                damageComponent.TakeDamage(_eplosionDamage);
            }
        }
    }
    

    async void DelayDestruction()
    {
        int delayBeforeDestruction = (int)_lifeTime * 1000;
        await Task.Delay(delayBeforeDestruction);

        if (_showExplosionSphere)
        {
            Destroy(_sphere);            
        }

        Destroy(gameObject);
    }
}