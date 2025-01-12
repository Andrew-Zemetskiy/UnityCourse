using UnityEngine;

public class Projectile_Ordinary : Projectile
{
    protected override void OnHit(Collision collision)
    {
        StartCoroutine(DelayBeforeDestroy(lifetimeAfterHitObject));
    }

    protected override void PlayVFX()
    {
        
    }
}
