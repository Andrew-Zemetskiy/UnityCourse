using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    private Rigidbody _rb;

    private InputSystem_Actions _playerControls;
    private InputAction _move;
    private Vector2 _movementInput;

    private void Awake()
    {
        _playerControls = new InputSystem_Actions();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _move = _playerControls.Player.Move;
        _move.Enable();
    }
    
    void FixedUpdate()
    {
        _movementInput = _move.ReadValue<Vector2>();
        
        float sideForce = _movementInput.x * rotationSpeed;
        _rb.angularVelocity = sideForce != 0.0f ? new Vector3(0.0f, sideForce, 0.0f) : Vector3.zero;

        float forwardForce = _movementInput.y * movementSpeed;
        _rb.linearVelocity = forwardForce != 0.0f ? _rb.transform.forward * forwardForce : Vector3.zero;
    }
}