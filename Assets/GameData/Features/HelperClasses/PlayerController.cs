using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;


[System.Serializable]
public class AgressiveEnvironmentIconData
{
    public AgressiveEnvironmentType Type;
    public Image Icon;
}


public class PlayerController : MonoBehaviour
{
    [SerializeField] JoyStickController _joyStickController;
    [SerializeField] PlayerEffectsHandler _effectsHandler;
    [SerializeField] Weapon _weapon;



    [SerializeField] Rigidbody _rb;
    [SerializeField] float _moveSpeed;
    [SerializeField] Transform _playerModelContainer;


    [Header("Sounds")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _collectHealthSound;
    [SerializeField] AudioClip _collectArmourSound;



    public void Start()
    {
        _weapon.SetGunStats();
        _effectsHandler.Reset();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _weapon.StartShootingContinuesly();
        } 
        
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _weapon.StopShootingContinuesly();
        }
    }
















    // Collision logic
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == TagConstraintsConfig.COLLECTIBLE_ITEM_TAG)
        {
            BasicDropItem itemData = col.gameObject.GetComponent<BasicDropItem>();

            switch (itemData.GetDropItemType())
            {
                case DropItemType.HealthPack:
                _audioSource.clip = _collectHealthSound;
                _audioSource.Play();
                break;
                
                case DropItemType.ArmourPack:
                _audioSource.clip = _collectArmourSound;
                _audioSource.Play();
                break;
            }

            Destroy(col.gameObject);
        }
    
        if (col.tag == TagConstraintsConfig.AGRESSIVE_ENVIRONMENT_AREA_TAG)
        {
            // Apply effect on player
            BasicAgresiveEnvironment environment = col.gameObject.GetComponent<BasicAgresiveEnvironment>();
            if (environment != null)
            {
                _effectsHandler.ApplyEffect(environment);
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag == TagConstraintsConfig.AGRESSIVE_ENVIRONMENT_AREA_TAG)
        {
            BasicAgresiveEnvironment environment = col.gameObject.GetComponent<BasicAgresiveEnvironment>();
            if (environment != null)
            {
                _effectsHandler.RemoveEffect(environment);
            }
        }
    }





















    // Movement -> redo with new input system and new joystick
    public void FixedUpdate()
    {
        Vector3 joysticInput = _joyStickController.joysticMovementVector;
        Vector3 newVelocityVector = new Vector3(joysticInput.x, 0, joysticInput.y) * _moveSpeed;
        
        _rb.velocity = newVelocityVector;

        if (newVelocityVector.x != 0 || newVelocityVector.z != 0)
        {
            _playerModelContainer.transform.rotation = Quaternion.LookRotation(newVelocityVector);
        }
    }
}