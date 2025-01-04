using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _sprintSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 150f;
    [SerializeField] private float _animationBlendSpeed = 0.2f;
    [SerializeField] private float _jumpSpeed = 7f;
    [SerializeField] private float _delayForSpawning = 1.45f;
    [SerializeField] private float _attackAmount = 4f;
    
    private CharacterController _controller;
    private Camera _characterCamera;
    private Animator _animator;
    private float _rotationAngle = 0.0f;
    private float _targetAnimationSpeed = 0.0f;
    private bool _isSprint = false;

    private float _speedY = 0.5f;
    private float _gravity = -9.81f;
    private bool _isJumping = false;
    private bool _isSpawned = false;
    private bool _isDead = false;

    public CharacterController Controller
    {
        get { return _controller = _controller ?? GetComponent<CharacterController>(); }
    }

    public Camera CharacterCamera
    {
        get { return _characterCamera = _characterCamera ?? FindObjectOfType<Camera>(); }
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

        _playerInputSystem.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInputSystem.Player.Move.canceled += ctx => _moveInput = Vector2.zero;

        _playerInputSystem.Player.Jump.performed += ctx => Jump();
        _playerInputSystem.Player.Run.performed += ctx => _isSprint = true;
        _playerInputSystem.Player.Run.canceled += ctx => _isSprint = false;
        _playerInputSystem.Player.Blow.performed += Attack;
        _playerInputSystem.Player.Death.performed += Death;
    }

    void Update()
    {
        if (!_isSpawned)
        {
            StartCoroutine(SpawnWithDelay());
            SimpleGravity();
        }
        else if(!_isDead)
        {
            Movement();
        }
        else
        {
            SimpleGravity();
        }
    }
    
    private IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(_delayForSpawning);
        _isSpawned = true;
    }

    private void SimpleGravity()
    {
        Controller.Move(Vector3.up * (_gravity * Time.deltaTime));
    }
    
    private void Movement()
    {
        Jumping();

        Vector3 moveDir = new Vector3(_moveInput.x, 0.0f, _moveInput.y);
        Vector3 rotatedMove = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) *
                              moveDir.normalized;
        Vector3 verticalMovement = Vector3.up * (_speedY == 0.0f ? _gravity * Time.deltaTime : _speedY);

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

    private void Jump()
    {
        if (!_isJumping)
        {
            _isJumping = true;
            CharacterAnimator.SetTrigger("Jump");
            _speedY += _jumpSpeed;
        }
    }

    private void Jumping()
    {
        if (!Controller.isGrounded)
        {
            _speedY += _gravity * Time.deltaTime;
            // Debug.Log("NOT Grounded");
        }
        else if (_speedY < 0.0f)
        {
            // Debug.Log("Grounded");
            _speedY = 0.0f;
        }

        CharacterAnimator.SetFloat("SpeedY", _speedY / _jumpSpeed);
        if (_isJumping && _speedY < 0.0f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, LayerMask.GetMask("Default")))
            {
                _isJumping = false;
                CharacterAnimator.SetTrigger("Land");
            }
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Random.Range(1.0f, 5.0f);
        int attackType = (int)Random.Range(1, _attackAmount);
        CharacterAnimator.SetInteger("Attack_Type", attackType);
        CharacterAnimator.SetTrigger("Attack");
    }

    private void Death(InputAction.CallbackContext context)
    {
        CharacterAnimator.SetTrigger("Death");
        _isDead = true;
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