using Unity.VisualScripting;
using UnityEngine;

public class Ordinary : Projectile
{
    protected override void OnHit(Collision collision)
    {
        Debug.Log("Ordinary projectile hit:" + collision.gameObject.name);
        Destroy(gameObject);
    }
}
