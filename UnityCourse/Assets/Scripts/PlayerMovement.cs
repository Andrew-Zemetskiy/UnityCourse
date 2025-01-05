using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _flashPoint;
    [SerializeField] private Camera _camera;

    public float movementSpeed = 8f;
    public float rotationSpeed = 4f;
    public float tiltSpeed = 2f;

    private Vector2 _movementInput;

    private float _tilt = 0f; // Camera
    private float _turn = 0f; // Object

    private InputSystem _playerControls;
    private InputAction _move;

    private void Awake()
    {
        _playerControls = new InputSystem();
    }

    private void Start()
    {
        _camera = Camera.main;

        _turn = transform.rotation.eulerAngles.y; // base angle on Y axis
        _tilt = _camera!.transform.localRotation.x; // base tilt

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
        _flashPoint.localRotation = Quaternion.Euler(_tilt, 0, 0); // gun tilt with camera

        Vector3 moveDirection = new Vector3(_movementInput.x, 0, _movementInput.y).normalized;
        Vector3 movement = transform.TransformDirection(moveDirection) * (movementSpeed * Time.deltaTime);

        _characterController.Move(movement);
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