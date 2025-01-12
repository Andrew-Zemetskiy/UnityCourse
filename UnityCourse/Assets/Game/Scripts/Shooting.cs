using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private InputSystem_Actions _playerControls;
    private InputAction _shoot;
    private ProjectilesType _projectileType = ProjectilesType.Ordinary;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private ParticleSystem _shootVFXprefab;

    private int _currentProjectileIndex = 0;
    private ParticleSystem _currentVFX;

    private void Awake()
    {
        _playerControls = new InputSystem_Actions();
    }

    private void Start()
    {
        AmmoTypeChanger.OnTriggerEntered += OnAmmoTypeChange;
    }

    private void OnAmmoTypeChange(ProjectilesType obj)
    {
        switch (_projectileType = obj)
        {
            case ProjectilesType.Ordinary:
                _currentProjectileIndex = 0;
                break;
            case ProjectilesType.Grenade:
                _currentProjectileIndex = 1;
                break;
            case ProjectilesType.Tennis:
                _currentProjectileIndex = 2;
                break;
        }
    }

    private void OnEnable()
    {
        _shoot = _playerControls.Player.Shoot;
        _shoot.Enable();
        _shoot.performed += Shoot;
    }

    private void OnDisable()
    {
        _shoot.performed -= Shoot;
        _shoot.Disable();
    }

    private void OnDestroy()
    {
        AmmoTypeChanger.OnTriggerEntered -= OnAmmoTypeChange;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        GameObject projectileInstance = Instantiate(projectilePrefabs[_currentProjectileIndex], firePoint.position,
            firePoint.rotation);
        Projectile projectile = projectileInstance.GetComponent<Projectile>();

        Debug.DrawRay(firePoint.position, firePoint.forward * 5, Color.red, 2f);
        projectile?.Launch(firePoint.forward);

        PlayVFX();
    }

    private void PlayVFX()
    {
        if (!_currentVFX)
        {
            _currentVFX = Instantiate(_shootVFXprefab, firePoint.position,
                Quaternion.Euler(-90f, 0f, 0f), firePoint.transform);
        }

        _currentVFX.Play();
    }
}