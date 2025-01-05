using System;
using UnityEngine;

public class Sfx_play : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        if (_audioSource != null && _audioSource.clip != null)
        {
            _audioSource.Play();
        }
    }
}