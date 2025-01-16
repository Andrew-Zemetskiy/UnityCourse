using System;
using UnityEngine;

public class Projectile_Tennis : Projectile
{
    [SerializeField] private TrailRenderer _trailRenderer;

    private void Start()
    {
        PlayVFX();
    }
    
    protected override void OnHit(Collision collision)
    {
        StartCoroutine(DelayBeforeDestroy(lifetimeAfterHitObject));
        PlaySound();
    }
    
    protected override void PlayVFX()
    {
        Instantiate(_trailRenderer, transform.position, Quaternion.identity, transform);
    }

    protected override void PlaySound()
    {
       AudioManager.Instance.PlaySound(AudioManager.Instance.bounceSound);
    }
}
