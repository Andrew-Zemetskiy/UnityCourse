using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private InputSystem_Actions _playerControls;
    private InputAction _shoot;

    private void Awake()
    {
        _playerControls = new InputSystem_Actions();
    }

    private void Start()
    {
        _shoot = _playerControls.Player.Shoot;
        _shoot.Enable();
        _shoot.performed += OnShoot;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Debug.Log($"Shooting {context}");
    }
    
}
