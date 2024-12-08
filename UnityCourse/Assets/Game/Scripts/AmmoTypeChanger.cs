using System;
using UnityEngine;

public class AmmoTypeChanger : MonoBehaviour
{
    public ProjectilesType projectileType;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Change for {other.name} projectile to {projectileType}");
    }
}
