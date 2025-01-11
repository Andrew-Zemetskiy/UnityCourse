using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    public float moveSpeed = 1f;

    private InputSystem _inputSystem;
    private Rigidbody2D _rb;
    private Vector2 _inputDirection;
    private Vector2 _previousDirection;
    private float _animDir = -1;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inputSystem = new InputSystem();
    }

    private void OnEnable()
    {
        _inputSystem.Player.Move.performed += Move;
        _inputSystem.Player.Move.canceled += ResetMovement;
        _inputSystem.Enable();
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
        _inputSystem.Player.Move.performed -= Move;
        _inputSystem.Player.Move.canceled -= ResetMovement;
    }
    
    private void Move(InputAction.CallbackContext obj)
    {
        Vector2 input = obj.ReadValue<Vector2>();
        float inputY = input.y;
        float inputX = input.x;

        if (inputY != 0) //vertical (W,S)
        {
            _inputDirection = new Vector2(1 * inputY, 0.5f * inputY);
            float newAnimDir= inputY > 0 ? 0 : 0.5f;
            UpdateAnimationState(newAnimDir);
        }
        else if (inputX != 0) //horizontal (A,D)
        {
            _inputDirection = new Vector2(1 * inputX, -0.5f * inputX);
            float newAnimDir = inputX > 0 ? 0.25f : 0.75f;
            UpdateAnimationState(newAnimDir);
        }
    }
    
    private void UpdateAnimationState(float newAnimDir)
    {
        if (_animDir != newAnimDir) // Меняем анимацию только при изменении направления
        {
            _animDir = newAnimDir;
            _playerAnimator.SetTrigger("Run");
            _playerAnimator.SetFloat("Direction", _animDir);
        }
    }
    
    private void ResetMovement(InputAction.CallbackContext obj)
    {
        _inputDirection = Vector2.zero;
        if (_animDir != -1)
        {
            _playerAnimator.SetTrigger("Idle");
            _animDir = -1;
        }
    }
    
    private void FixedUpdate()
    {
        Vector2 movement = _inputDirection * moveSpeed;
        Vector2 newPos = _rb.position + movement * Time.fixedDeltaTime;

        _rb.MovePosition(newPos);
    }
}