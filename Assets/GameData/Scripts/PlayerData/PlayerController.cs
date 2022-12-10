using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] JoyStickController _joyStickController;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _moveSpeed;

    [SerializeField] Transform _playerModelContainer;

    private int _gelAmount;



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



    // Collision logic
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == TagConstraintsConfig.COLLECTIBLE_ITEM_TAG)
        {
            BasicDropableItem itemData = col.gameObject.GetComponent<BasicDropableItem>();
            Debug.Log("COLLECTED: " + itemData.GetDropItemType());

            switch (itemData.GetDropItemType())
            {
                case DropItemType.Gel1:
                break;
                case DropItemType.Gel2:
                break;
                case DropItemType.Gel3:
                break;
                case DropItemType.Gel4:
                break;
                

                case DropItemType.Crystal1:
                break;
                case DropItemType.Crystal2:
                break;
                case DropItemType.Crystal3:
                break;
                case DropItemType.Crystal4:
                break;
                
                
                case DropItemType.ArmourPack1:
                break;
                case DropItemType.ArmourPack2:
                break;
                case DropItemType.ArmourPack3:
                break;
                

                case DropItemType.HealthPack1:
                break;
                case DropItemType.HealthPack2:
                break;
                case DropItemType.HealthPack3:
                break;
            }

            Destroy(col.gameObject);
        }
    }
}