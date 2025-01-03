using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    private CharacterController _controller;
    private Camera _characterCamera;
    private float _rotationAngle = 0.0f;

    public CharacterController Controller
    {
        get { return _controller = _controller ?? GetComponent<CharacterController>(); }
    }

    public Camera CharacterCamera
    {
        get { return _characterCamera = _characterCamera ?? FindObjectOfType<Camera>(); }
    }

    private PlayerInputSystem _playerInputSystem;
    private Vector2 _moveInput;

    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();

        _playerInputSystem.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInputSystem.Player.Move.canceled += ctx => _moveInput = Vector2.zero;

        _playerInputSystem.Player.Jump.performed += Jump;
        _playerInputSystem.Player.Run.performed += Run;
        _playerInputSystem.Player.Blow.performed += Blow;
        _playerInputSystem.Player.Death.performed += Death;
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Debug.Log($"_moveInput {_moveInput}");
        Vector3 moveDir = new Vector3(_moveInput.x, 0.0f, _moveInput.y);
        Vector3 rotatedMove = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) *
                              moveDir.normalized;

        Controller.Move(rotatedMove * (_movementSpeed * Time.deltaTime));

        if (rotatedMove.sqrMagnitude > 0.0f)
        {
            _rotationAngle = Mathf.Atan2(rotatedMove.x, rotatedMove.z) * Mathf.Rad2Deg;
            
            Quaternion targetRotation = Quaternion.Euler(0.0f, _rotationAngle, 0.0f);
            Controller.transform.rotation = Quaternion.RotateTowards(
                Controller.transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }

        // Quaternion currentRotation = Controller.transform.rotation;
        // Quaternion targetRotation = Quaternion.Euler(0.0f, _rotationAngle, 0.0f);
        // Controller.transform.rotation =
        //     Quaternion.Lerp(currentRotation, targetRotation, _rotationSpeed * Time.deltaTime);
        // Debug.Log($"currentRotation {currentRotation}");
    }

    private void Jump(InputAction.CallbackContext context)
    {
    }

    private void Run(InputAction.CallbackContext context)
    {
    }

    private void Blow(InputAction.CallbackContext context)
    {
    }

    private void Death(InputAction.CallbackContext context)
    {
    }

    private void OnEnable()
    {
        _playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        _playerInputSystem.Disable();
    }
}