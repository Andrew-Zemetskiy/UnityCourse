using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action OnPlayerKilled;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnPlayerKilled?.Invoke();
        }
    }
}
