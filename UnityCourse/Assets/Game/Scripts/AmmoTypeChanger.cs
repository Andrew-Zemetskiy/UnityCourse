using System;
using UnityEngine;

public class AmmoTypeChanger : MonoBehaviour
{
    public static event Action<ProjectilesType> OnTriggerEntered;
    [SerializeField] private ProjectilesType projectileType;
    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEntered?.Invoke(projectileType);
    }
}
