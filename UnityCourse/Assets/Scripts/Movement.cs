using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputSystem _inputSystem;

    public static Action OnPlayerChangeDir;
    
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
        FlipSides();
        OnPlayerChangeDir?.Invoke();
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