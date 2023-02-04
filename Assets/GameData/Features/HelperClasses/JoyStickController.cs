using UnityEngine.EventSystems;
using UnityEngine;

public class JoyStickController : MonoBehaviour
{
    [SerializeField] GameObject _joystickPin;
    [SerializeField] GameObject _joystickBackground;
    [SerializeField] RectTransform _joystickBackgroundRect;

    public Vector2 joysticDirectionVector;
    public Vector2 joysticMovementVector;
    private Vector2 _joysticTouchPos;
    private Vector2 _joysticOriginalPos;
    private float _joystickRadius;

    public delegate void OnControllerUpdated(Vector2 movementDirection);
    public event OnControllerUpdated OnControllerMoveMade;


    private void Start()
    {
        _joysticOriginalPos = _joystickBackground.transform.position;
        _joystickRadius =_joystickBackgroundRect.sizeDelta.y / 5;
    }


    // Toch input logic
    public void PointerDown()
    {
        // Save finger touch position data
        Vector2 touchPos = Input.mousePosition;

        _joysticTouchPos = touchPos;
        _joystickPin.transform.position = touchPos;
        _joystickBackground.transform.position = touchPos;
    }

    public void Drag(BaseEventData baseEventData)
    {
        // Get finger position while dragging
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;


        float pinMoveDistance = Vector2.Distance(dragPos, _joysticTouchPos);
        float directionMagnitude = Mathf.Clamp(pinMoveDistance, 0f, _joystickRadius);
        

        // Identify movement direction
        joysticDirectionVector = (dragPos - _joysticTouchPos).normalized;
        joysticMovementVector = joysticDirectionVector * directionMagnitude / _joystickRadius;


        // Clamp visuals
        _joystickPin.transform.position = _joysticTouchPos + joysticDirectionVector * directionMagnitude;
    }

    public void PointerUp()
    {
        // Reset joystic data
        joysticMovementVector = Vector2.zero;
        joysticDirectionVector = Vector2.zero;

        _joystickPin.transform.position = _joysticOriginalPos;
        _joystickBackground.transform.position = _joysticOriginalPos;
    }
}