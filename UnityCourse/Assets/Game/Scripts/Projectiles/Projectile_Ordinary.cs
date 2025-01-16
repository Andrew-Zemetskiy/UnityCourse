using Unity.Mathematics;
using UnityEngine;

public class Projectile_Ordinary : Projectile
{
    [SerializeField] private ParticleSystem _particleSystem;
    
    protected override void OnHit(Collision collision)
    {
        StartCoroutine(DelayBeforeDestroy(lifetimeAfterHitObject));
        PlayVFX();
        PlaySound();
    }

    protected override void PlayVFX()
    {
        Instantiate(_particleSystem, transform.position, quaternion.identity);
    }

    protected override void PlaySound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.hitSound);
    }
}
