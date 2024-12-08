using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

    protected Rigidbody Rb;

    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody>();
        // Destroy(gameObject, lifetime);
    }

    public virtual void Launch(Vector3 direction)
    {
        if (Rb != null)
        {
            Rb.linearVelocity = direction * speed;
        }
    }

    protected abstract void OnHit(Collision collision);

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision);
    }
}
