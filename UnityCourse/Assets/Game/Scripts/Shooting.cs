using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private InputSystem_Actions _playerControls;
    private InputAction _shoot;
    private ProjectilesType _projectileType = ProjectilesType.Ordinary;

    private void Awake()
    {
        _playerControls = new InputSystem_Actions();
    }

    private void Start()
    {
        AmmoTypeChanger.OnTriggerEntered += OnAmmoTypeChanger;
    }

    private void OnAmmoTypeChanger(ProjectilesType obj)
    {
        Debug.Log($"In shooting: {obj}");
    }

    private void OnEnable()
    {
        _shoot = _playerControls.Player.Shoot;
        _shoot.Enable();
        _shoot.performed += OnShoot;
    }

    private void OnDisable()
    {
        _shoot.performed -= OnShoot;
        _shoot.Disable();
    }

    private void OnDestroy()
    {
        AmmoTypeChanger.OnTriggerEntered -= OnAmmoTypeChanger;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Debug.Log($"Shooting {context}");
    }
}