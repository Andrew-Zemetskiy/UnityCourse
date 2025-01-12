using System;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private CinemachineCamera _cinemaMachine;

    public static Action OnPlayerInit;
    
    private void Start()
    {
        SpawnLevel();
        SpawnPlayer();
    }

    private void SpawnLevel()
    {
        Instantiate(_levelPrefab);
    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(_playerPrefab);
        _cinemaMachine.Follow = player.transform;
        OnPlayerInit?.Invoke();
    }
}