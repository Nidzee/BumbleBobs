using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 90f;
    [SerializeField] Transform _rotateCustomObject;
    Transform _rotateTarget;

    void Start()
    {
        _rotateTarget = this.transform;
        if (_rotateCustomObject != null)
        {
            _rotateTarget = _rotateCustomObject;
        }
    }

    void Update()
    {
        // Rotates N degrees per second around
        _rotateTarget.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }
}
