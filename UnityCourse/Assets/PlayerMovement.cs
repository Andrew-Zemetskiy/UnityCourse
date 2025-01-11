using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    private InputSystem _inputSystem;
    private Rigidbody2D _rb;
    private Vector2 _inputDirection;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inputSystem = new InputSystem();
    }

    private void OnEnable()
    {
        _inputSystem.Player.Move.performed += MoveTest;
        _inputSystem.Player.Move.canceled += ctx => _inputDirection = Vector2.zero;
        _inputSystem.Enable();
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
        _inputSystem.Player.Move.performed -= MoveTest;
    }
    
    private void MoveTest(InputAction.CallbackContext obj)
    {
        Debug.Log($"Value: {obj.ReadValue<Vector2>()}");
        Vector2 input = obj.ReadValue<Vector2>();

        if (input.y != 0) //vertical (W,S)
        {
            _inputDirection = new Vector2(1 * input.y, 0.5f * input.y);
        }
        else if (input.x != 0) //horizontal (A,D)
        {
            _inputDirection = new Vector2(1 * input.x, -0.5f * input.x);
        }
    }
    
    private void FixedUpdate()
    {
        Vector2 movement = _inputDirection * moveSpeed;
        Vector2 newPos = _rb.position + movement * Time.fixedDeltaTime;

        _rb.MovePosition(newPos);
    }
}