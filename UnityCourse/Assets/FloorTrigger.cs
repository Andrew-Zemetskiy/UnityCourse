using System;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _floor;
    [SerializeField] private Transform _spawnFloorPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("EnterTrigger");
            FloorSpawnManager.Instance.OnFloorTriggerEnter(_floor, _spawnFloorPoint);
        }
    }
}