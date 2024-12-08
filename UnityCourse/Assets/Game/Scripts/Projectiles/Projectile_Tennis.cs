using UnityEngine;

public class Projectile_Tennis : Projectile
{
    protected override void OnHit(Collision collision)
    {
        StartCoroutine(DelayBeforeDestroy(lifetimeAfterHitObject));
    }
}
