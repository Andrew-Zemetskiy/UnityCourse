using Unity.Mathematics;
using UnityEngine;

public class Projectile_Ordinary : Projectile
{
    [SerializeField] private ParticleSystem _particleSystem;
    
    protected override void OnHit(Collision collision)
    {
        StartCoroutine(DelayBeforeDestroy(lifetimeAfterHitObject));
        PlayVFX();
    }

    protected override void PlayVFX()
    {
        Instantiate(_particleSystem, transform.position, quaternion.identity);
    }
}
