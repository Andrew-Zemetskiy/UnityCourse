using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputSystem _inputSystem;
    private bool _isMoveRight = true;

    private void Awake()
    {
        _inputSystem = new InputSystem();
    }

    private void Start()
    {
        _inputSystem.Player.ChangeDirection.performed += ChangeDirection;
        _inputSystem.Enable();
    }

    private void ChangeDirection(InputAction.CallbackContext obj)
    {
        _isMoveRight = !_isMoveRight;
        FlipSides();
    }

    private void FlipSides()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    
    private void OnDestroy()
    {
        _inputSystem.Disable();
        _inputSystem.Player.ChangeDirection.performed -= ChangeDirection;
    }
}