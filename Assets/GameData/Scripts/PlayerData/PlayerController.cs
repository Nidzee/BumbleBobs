using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] JoyStickController _joyStickController;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _moveSpeed;

    [SerializeField] Transform _playerModelContainer;


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

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == TagConstraintsConfig.COLLECTIBLE_ITEM_TAG)
        {
            BasicDropableItem itemData = col.gameObject.GetComponent<BasicDropableItem>();
            Debug.Log("COLLECTED: " + itemData.GetDropItemType());
            Destroy(col.gameObject);
        }
    }
}