using UnityEngine;
using UnityEngine.InputSystem;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private GameObject _targetObject;

    private PlayerInput _playerInput;
    private Vector2 _rotationInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Rotate.performed += OnRotate;
    }

    private void Start()
    {
        Spawner.OnCurrentObjectChanged += go => _targetObject = go; //set target object
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
       if(_targetObject == null) return;
       
       _rotationInput = context.ReadValue<Vector2>();
       _targetObject.transform.Rotate(0, -_rotationInput.x * rotationSpeed* Time.deltaTime, 0, Space.World);
    }
    
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnDestroy()
    {
        _playerInput.Player.Rotate.performed -= OnRotate;
    }
}