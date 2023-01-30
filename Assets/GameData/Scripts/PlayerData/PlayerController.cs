using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] JoyStickController _joyStickController;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _moveSpeed;

    [SerializeField] Transform _playerModelContainer;
    [SerializeField] Weapon _weapon;




    public void Start()
    {
        _weapon.SetGunStats();
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
            BasicDropableItem itemData = col.gameObject.GetComponent<BasicDropableItem>();

            switch (itemData.GetDropItemType())
            {
                case DropItemType.Gel:
                break;
                
                case DropItemType.Crystal:
                break;
                
                case DropItemType.HealthPack:
                break;
                
                case DropItemType.ArmourPack:
                break;
            }

            Destroy(col.gameObject);
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