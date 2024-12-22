using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float lookSensitivity = 10f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Button jumpBtn;

    private InputSystem_Actions _inputSystem;
    private Vector2 _moveInput;
    private Vector2 _lookInput;

    private float _gravity;
    private Vector3 _velocity;
    private bool _isGrounded;

    private float _verticalRotation = 0f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private Vector2 _lastTouchPosition;
    private Vector2 _touchDelta;
    
    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();

        _inputSystem.Player.Move.performed += context => _moveInput = context.ReadValue<Vector2>();
        _inputSystem.Player.Move.canceled += context => _moveInput = Vector2.zero;

        _inputSystem.Player.Look.performed += context => _lookInput = context.ReadValue<Vector2>();
        _inputSystem.Player.Look.canceled += context => _lookInput = Vector2.zero;

        _inputSystem.Player.Jump.performed += Jump;
        jumpBtn.onClick.AddListener(() => Jump(default));
        
        _inputSystem.Player.Touch.performed += OnTouchPerformed;

        _inputSystem.Player.TouchPress.canceled += context =>
        {
            _lastTouchPosition = Vector2.zero;
            _lookInput = Vector2.zero;
        };
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _gravity = Physics.gravity.y;
    }

    private void OnEnable()
    {
        _inputSystem.Player.Enable();
    }

    private void OnDisable()
    {
        _inputSystem.Player.Disable();
    }

    private void Update()
    {
        GetJoyStickData();
        Movement();
        Rotation();
        ApplyGravity();
    }

    private void GetJoyStickData()
    {
        _moveInput = variableJoystick.Direction;
    }

    private void Movement()
    {
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y) * moveSpeed;
        controller.Move(transform.TransformDirection(moveDirection) * Time.deltaTime);
    }

    private void Rotation()
    {
        float horizontalRotation = _lookInput.x * lookSensitivity * Time.deltaTime;
        controller.transform.Rotate(Vector3.up, horizontalRotation);

        _verticalRotation -= _lookInput.y * lookSensitivity * Time.deltaTime;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -45f, 45f);
        cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);

        _lookInput = Vector2.zero;
    }

    private void ApplyGravity()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += _gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        if (_isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity); // V=√(2gh). g - 9.81, h - height
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    }
    
    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = context.ReadValue<Vector2>();

        if (touchPosition.x > Screen.width / 2)
        {
            if (_lastTouchPosition != Vector2.zero)
            {
                _touchDelta = touchPosition - _lastTouchPosition;
            }

            if (_lastTouchPosition == touchPosition)
            {
                _lookInput = Vector2.zero;
            }
            else
            {
                _lookInput = _touchDelta;
            }
            
            _lastTouchPosition = touchPosition;
        }
        else
        {
            _lastTouchPosition = Vector2.zero;
            _lookInput = Vector2.zero;
        }
    }
}