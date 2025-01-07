using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _jumpForce = 4f;
    
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;

    private Rigidbody2D _rb;
    
    private InputSystem _inputSystem;
    private float _moveInput;
    
    private bool _isGrounded = true;
    private bool _isJumping = false;
    
    private void Awake()
    {
        _inputSystem = new InputSystem();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _inputSystem.Player.Move.performed += OnMove;
        _inputSystem.Player.Move.canceled += ctx => _moveInput = 0f;
        _inputSystem.Player.Jump.performed += OnJump;
        _inputSystem.Enable();
    }

    private void Update()
    {
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    private void Movement()
    {
        _rb.linearVelocity = new Vector2(_moveInput * _moveSpeed, _rb.linearVelocity.y);
        
        if (_isJumping && _isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            _isJumping = false;
        }
    }
    
    private void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<float>();
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _isJumping = true;
        }
    }
    
    private void OnDestroy()
    {
        _inputSystem.Disable();
        _inputSystem.Player.Move.performed -= OnMove;
        _inputSystem.Player.Jump.performed -= OnJump;
    }
}