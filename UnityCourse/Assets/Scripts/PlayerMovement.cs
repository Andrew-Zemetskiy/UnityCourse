using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float lookSensitivity = 10f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private Transform cameraTransform;

    private InputSystem_Actions _inputSystem;
    private Vector2 _moveInput;
    private Vector2 _lookInput;

    public float gravity = -9.81f;
    private Vector3 _velocity;
    private bool _isGrounded;

    private float _verticalRotation = 0f;

    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    
    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();

        _inputSystem.Player.Move.performed += context => _moveInput = context.ReadValue<Vector2>();
        _inputSystem.Player.Move.canceled += context => _moveInput = Vector2.zero;

        _inputSystem.Player.Look.performed += context => _lookInput = context.ReadValue<Vector2>();
        _inputSystem.Player.Look.canceled += context => _lookInput = Vector2.zero;

        _inputSystem.Player.Jump.performed += Jump;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
    }

    private void OnDisable()
    {
        _inputSystem.Player.Disable();
    }

    private void Update()
    {
        Movement();
        Rotation();
        ApplyGravity();
        // HandleGravity();
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
    }

    private void HandleGravity()
    {
        _isGrounded = controller.isGrounded;
        Debug.Log($"Grounded: {_isGrounded}");

        if (_isGrounded && _velocity.y < 0)
        {
            Debug.Log("OnGround");
            _velocity.y = 0f;
        }

        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
    
    
    private void ApplyGravity()
    {
        // Проверяем, на земле ли персонаж
        _isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // Сбрасываем скорость падения при контакте с землей
        }

        // Применяем гравитацию
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    }
    
    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        if (_isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}