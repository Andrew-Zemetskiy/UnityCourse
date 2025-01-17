using System.Collections;
using UnityEngine;

public class Projectile_Grenade : Projectile
{
    [SerializeField] private ParticleSystem _VFXprefab;
    
    public float explosionRadius = 5f;
    public float explosionForce = 500f;

    protected override void OnHit(Collision collision)
    {
        StartCoroutine(DelayBeforeExplosion(lifetimeAfterHitObject));
    }

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        PlayVFX();
        PlaySound();
    }

    protected override void PlayVFX()
    {
        Instantiate(_VFXprefab, transform.position, Quaternion.identity);
    }

    protected override void PlaySound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.explosionSound);
    }

    private IEnumerator DelayBeforeExplosion(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explosion();
        gameObject.SetActive(false);
    }
}