using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _sprintSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 150f;
    [SerializeField] private float _animationBlendSpeed = 0.2f;

    private CharacterController _controller;
    private Camera _characterCamera;
    private Animator _animator;
    private float _rotationAngle = 0.0f;
    private float _targetAnimationSpeed = 0.0f;
    private bool _isSprint = false;

    private float _gravity = -9.81f;
    private bool _isDead = false;
    private bool _isStandingUp = false;

    public static Action OnPlayerDeath;
    public static Action OnPlayerStandUp;
    public static PlayerMove Instance;
    
    public CharacterController Controller
    {
        get { return _controller = _controller ?? GetComponent<CharacterController>(); }
    }

    public Camera CharacterCamera
    {
        get { return _characterCamera = _characterCamera ?? FindFirstObjectByType<Camera>(); }
    }

    public Animator CharacterAnimator
    {
        get { return _animator = _animator ?? GetComponent<Animator>(); }
    }

    private PlayerInputSystem _playerInputSystem;
    private Vector2 _moveInput;

    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();
        Instance = this;
    }

    void Update()
    {
        if (!_isDead)
        {
            Movement();
        }
    }

    private void SimpleGravity()
    {
        Controller.Move(Vector3.up * (_gravity * Time.deltaTime));
    }

    private void Movement()
    {
        Vector3 moveDir = new Vector3(_moveInput.x, 0.0f, _moveInput.y);
        Vector3 rotatedMove = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) *
                              moveDir.normalized;
        Vector3 verticalMovement = Vector3.up * (_gravity * Time.deltaTime);

        float currentSpeed = _isSprint ? _sprintSpeed : _movementSpeed;
        Controller.Move((verticalMovement + rotatedMove * currentSpeed) * Time.deltaTime);

        if (rotatedMove.sqrMagnitude > 0.0f)
        {
            _rotationAngle = Mathf.Atan2(rotatedMove.x, rotatedMove.z) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0.0f, _rotationAngle, 0.0f);
            Controller.transform.rotation = Quaternion.RotateTowards(
                Controller.transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );

            _targetAnimationSpeed = _isSprint ? 1.0f : 0.5f;
        }
        else
        {
            _targetAnimationSpeed = 0.0f;
        }

        CharacterAnimator.SetFloat("Speed",
            Mathf.Lerp(CharacterAnimator.GetFloat("Speed"), _targetAnimationSpeed, _animationBlendSpeed));
    }

    public void InitPlayerDeath()
    {
        Death(new InputAction.CallbackContext());
    }
    
    private void Death(InputAction.CallbackContext context)
    {
        if (!_isDead)
        {
            _isDead = true;
            _controller.enabled = false;
            OnPlayerDeath?.Invoke();
        }
    }

    private void StandUp(InputAction.CallbackContext context)
    {
        if (_isDead && !_isStandingUp)
        {
            OnPlayerStandUp?.Invoke();
            _controller.enabled = true;
            CharacterAnimator.SetTrigger("StandUp");
            CharacterAnimator.SetFloat("Speed", 0f);
            _isStandingUp = true;
            StartCoroutine(GetUpDelay(4f));
        }
    }
    
    private IEnumerator GetUpDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _isDead = false;
        _isStandingUp = false;
    }

    private void OnEnable()
    {
        _playerInputSystem.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInputSystem.Player.Move.canceled += ctx => _moveInput = Vector2.zero;

        _playerInputSystem.Player.Run.performed += ctx => _isSprint = true;
        _playerInputSystem.Player.Run.canceled += ctx => _isSprint = false;
        _playerInputSystem.Player.Death.performed += Death;
        _playerInputSystem.Player.Respawn.performed += StandUp;
        _playerInputSystem.Enable();
    }
    
    private void OnDisable()
    {
        _playerInputSystem.Disable();
        _playerInputSystem.Player.Death.performed -= Death;
        _playerInputSystem.Player.Respawn.performed -= StandUp;
    }
}