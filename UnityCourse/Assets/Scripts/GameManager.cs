using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Objects prefabs")]
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _obstaclePrefab;
    
    [Header("SpawnPoints")]
    [SerializeField] private Transform _levelSpawnPos;
    [SerializeField] private Transform _playerSpawnPos;
    [SerializeField] private Transform _enemySpawnPos;
    [SerializeField] private Transform _obstacleSpawnPos;
    
    public static GameManager Instance;
    private GameObject level;
    private GameObject player;
    private GameObject enemy;
    private GameObject obstacle;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PlayerController.OnPlayerDeath += PlayerDeath;
        LoadLevel();
    }

    public void LoadLevel()
    {
        level = Instantiate(_levelPrefab, _levelSpawnPos.position, Quaternion.identity);
        player = Instantiate(_playerPrefab, _playerSpawnPos.position, Quaternion.identity);
        enemy = Instantiate(_enemyPrefab, _enemySpawnPos.position, Quaternion.identity);
        obstacle = Instantiate(_obstaclePrefab, _obstacleSpawnPos.position, Quaternion.identity);
    }

    private void PlayerDeath()
    {
        StartCoroutine(RestartLevelWithDelay(2f));
    }

    private IEnumerator RestartLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartLevel();
    }

    private void RestartLevel()
    {
        level.transform.position = _levelSpawnPos.position;
        player.transform.position = _playerSpawnPos.position;
        player.gameObject.SetActive(true);
        enemy.transform.position = _enemySpawnPos.position;
        
        if (obstacle != null)
        {
            Destroy(obstacle);
        }
        obstacle = Instantiate(_obstaclePrefab, _obstacleSpawnPos.position, Quaternion.identity);
    }
}
