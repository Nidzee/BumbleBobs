using UnityEngine;

public enum RotateType
{
    RotateArmoundY,
    RotateArmoundZ
}

public class Rotator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 90f;
    [SerializeField] RotateType _rotateType = RotateType.RotateArmoundY;
    Vector3 _rotateVector;

    void Start()
    {
        if (_rotateType == RotateType.RotateArmoundY)
        {
            _rotateVector = Vector3.up;
        } 
        else
        {
            _rotateVector = Vector3.forward;
        }
    }

    void Update()
    {
        // Rotates N degrees per second around
        transform.Rotate(_rotateVector * _rotationSpeed * Time.deltaTime);
    }
}
