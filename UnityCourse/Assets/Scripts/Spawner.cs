using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    private InputSystem_Actions _playerControls;
    private InputAction _spawn;
    private int _spawnIndex;
    private GameObject _spawnedObject;
    
    [SerializeField] private List<GameObject> objectForSpawn;
    [SerializeField] private Transform spawnPoint;
    private void Awake()
    {
        _playerControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _spawn = _playerControls.Player.Spawn;
        _spawn.Enable();
        _spawn.performed += Spawn;
    }

    private void OnDisable()
    {
        _spawn.performed -= Spawn;
        _spawn.Disable();
    }

    private void Spawn(InputAction.CallbackContext context)
    {
        if (_spawnedObject != null)
        {
            Destroy(_spawnedObject);
        }
        
        _spawnedObject = Instantiate(objectForSpawn[_spawnIndex], spawnPoint.position, Quaternion.identity);
        _spawnIndex = (_spawnIndex + 1) % objectForSpawn.Count;
    }
}
