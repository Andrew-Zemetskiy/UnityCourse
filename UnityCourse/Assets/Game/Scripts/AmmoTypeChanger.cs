using System;
using UnityEngine;

public class AmmoTypeChanger : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Shooting>(out var obj))
        {
            obj.SetCurrentProjectile(_projectilePrefab);
        }
    }
}
