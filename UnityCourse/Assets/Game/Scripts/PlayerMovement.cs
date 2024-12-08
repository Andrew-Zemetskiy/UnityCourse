using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 8f;
    public float rotationSpeed = 4f;
    public float tiltSpeed = 2f;

    private Rigidbody _rb;
    private Camera _camera;
    [SerializeField] private Transform gunPoint;

    private Vector2 _movementInput;

    private float _tilt = 0f; // Camera
    private float _turn = 0f; // Object

    private InputSystem_Actions _playerControls;
    private InputAction _move;

    private void Awake()
    {
        _playerControls = new InputSystem_Actions();
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
        
        _turn = transform.rotation.eulerAngles.y; // Начальный угол по оси Y
        _tilt = _camera!.transform.localRotation.x; // Начальный наклон
        
        Cursor.lockState = CursorLockMode.Locked; //cursor lock and invisible
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdatePosAndRot();
    }

    private void UpdatePosAndRot()
    {
        _movementInput = new Vector2(_move.ReadValue<Vector2>().x, _move.ReadValue<Vector2>().y);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _turn += mouseX * rotationSpeed; //rotation by mouse

        _tilt -= mouseY * tiltSpeed;
        _tilt = Mathf.Clamp(_tilt, -60f, 60f); //camera tilt restrictions

        transform.rotation = Quaternion.Euler(0, _turn, 0); // player rotation left/right
        _camera.transform.localRotation = Quaternion.Euler(_tilt, 0f, 0); //camera up/down
        gunPoint.localRotation = Quaternion.Euler(_tilt, 0, 0); // gun tilt with camera
        
        Vector3 moveDirection = new Vector3(_movementInput.x, 0, _movementInput.y).normalized;
        Vector3 movement = transform.TransformDirection(moveDirection) * (movementSpeed * Time.deltaTime);

        _rb.MovePosition(transform.position + movement);
    }
    
    
    private void OnEnable()
    {
        _move = _playerControls.Player.Move;
        _move.Enable();
    }

    private void OnDisable()
    {
        _move.Disable();
    }
}