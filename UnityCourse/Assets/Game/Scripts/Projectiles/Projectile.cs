using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public string projectileTag;
    public float forceAmount = 10f;
    public float lifetime = 10f;
    public float lifetimeAfterHitObject = 5f;
    
    private Rigidbody _rb;

    public virtual void Launch(Vector3 direction)
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            return;
        }

        direction = direction.normalized;

        _rb.AddForce(direction * forceAmount, ForceMode.Impulse);

        StartCoroutine(DelayBeforeDestroy(lifetime));
    }
    
    protected IEnumerator DelayBeforeDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    
    protected abstract void OnHit(Collision collision);

    protected abstract void PlayVFX();
    protected abstract void PlaySound();

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision);
    }
}
