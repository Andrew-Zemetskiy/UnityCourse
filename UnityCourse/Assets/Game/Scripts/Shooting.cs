using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private InputSystem_Actions _playerControls;
    private InputAction _shoot;

    [SerializeField] private Transform firePoint;
    [SerializeField] private ParticleSystem _shootVFXprefab;
    
    private ParticleSystem _currentVFX;

    [SerializeField] private Projectile _baseProjectile;
    public string _projectileTag;

    private void Awake()
    {
        _playerControls = new InputSystem_Actions();
    }

    private void Start()
    {
        SetCurrentProjectile(_baseProjectile);
    }

    public void SetCurrentProjectile(Projectile go)
    {
        _projectileTag = go.projectileTag;
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

    private void Shoot(InputAction.CallbackContext context)
    {
        var go = BulletManager.Instance.GetPooledObject(_projectileTag);
        if (go == null)
        {
            Debug.LogWarning("You don't have this projectile type!");
            return;
        }
        go.transform.position = firePoint.position;
        go.transform.rotation = firePoint.rotation;
        Projectile projectile = go.GetComponent<Projectile>();

        Debug.DrawRay(firePoint.position, firePoint.forward * 5, Color.red, 2f);
        go.SetActive(true);
        projectile?.Launch(firePoint.forward);
        
        PlayVFX();
        AudioManager.Instance.PlaySound(AudioManager.Instance.shootSound);
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