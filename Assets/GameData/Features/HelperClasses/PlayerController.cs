using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] PlayerEffectsHandler _effectsHandler;
    [SerializeField] PlayerItemCollectorHandler _itemCollectHandler;
    
    [Header("Weapon")]
    [SerializeField] Weapon _weapon;

    [Space]
    [SerializeField] JoyStickController _joyStickController;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _moveSpeed;
    [SerializeField] Transform _playerModelContainer;




    public void Start()
    {
        _weapon.SetGunStats();
        _effectsHandler.Reset();
        _itemCollectHandler.Reset();
    }
















    // Collision logic
    public void OnTriggerEnter(Collider col)
    {
        // Collectible item logic
        if (col.tag == TagConstraintsConfig.COLLECTIBLE_ITEM_TAG)
        {
            BasicDropItem itemData = col.gameObject.GetComponent<BasicDropItem>();
            if (itemData != null)
            {
                _itemCollectHandler.CollectItem(itemData);
            }
        }
    

        // Effect zone logic
        if (col.tag == TagConstraintsConfig.EFFECT_ZONE_TAG)
        {
            BasicEffectZone environment = col.gameObject.GetComponent<BasicEffectZone>();
            if (environment != null)
            {
                _effectsHandler.ApplyEffect(environment.EffectData);
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag == TagConstraintsConfig.EFFECT_ZONE_TAG)
        {
            BasicEffectZone environment = col.gameObject.GetComponent<BasicEffectZone>();
            if (environment != null)
            {
                _effectsHandler.RemoveEffect(environment.EffectData);
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
}