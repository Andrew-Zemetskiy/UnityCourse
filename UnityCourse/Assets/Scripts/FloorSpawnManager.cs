using System.Collections.Generic;
using UnityEngine;

public class FloorSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _floorPrefabs;
    [SerializeField] private Transform _baseSpawnPoint;
    [SerializeField] private float _spawnDistance = 8.04f;

    [SerializeField] private List<GameObject> _floorsList = new();
    private GameObject _lastFloor;
    private int _lastIndex;

    public static FloorSpawnManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnFloor(_baseSpawnPoint.position, _floorsList.Count);
        }
    }

    public void OnFloorTriggerEnter(GameObject floor, Transform spawnPoint)
    {
        int currentFloorIndex = _floorsList.FindIndex(go => go == floor);
        if (_lastFloor == floor && _lastIndex == currentFloorIndex)
        {
            return;
        }

        _lastIndex = currentFloorIndex;
        _lastFloor = floor;

        if (currentFloorIndex == 1) //to upper floor
        {
            DestroyLowestFloor();
            SpawnFloor(spawnPoint.position, 2);
        }
        else if (currentFloorIndex == 0) //to lower floor
        {
            DestroyTopFloor();
            SpawnFloor(spawnPoint.position, -1, false);
        }
    }

    private void DestroyTopFloor()
    {
        int lastIndex = _floorsList.Count - 1;
        GameObject oldTopFloor = _floorsList[lastIndex];
        for (int i = lastIndex; i > 0; i--)
        {
            _floorsList[i] = _floorsList[i - 1];
        }

        Destroy(oldTopFloor);
    }

    private void SpawnFloor(Vector3 spawnPoint, int multiplier, bool isToUpperFloor = true)
    {
        Vector3 verticalSpawnOffset = Vector3.up * (multiplier * _spawnDistance);
        GameObject randomFloor = _floorPrefabs[Random.Range(0, _floorPrefabs.Count)];
        GameObject floorObject =
            Instantiate(randomFloor, spawnPoint + verticalSpawnOffset, Quaternion.identity);

        if (isToUpperFloor)
        {
            _floorsList.Add(floorObject);
        }
        else
        {
            _floorsList[0] = floorObject;
        }
    }

    private void DestroyLowestFloor()
    {
        GameObject lowestFloor = _floorsList[0];
        _floorsList.RemoveAt(0);
        Destroy(lowestFloor);
    }
}