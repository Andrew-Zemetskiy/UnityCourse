using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio")] 
    public AudioClip shootSound;
    public AudioClip hitSound;
    public AudioClip bounceSound;
    public AudioClip explosionSound;

    [SerializeField] private AudioSource _audioSource;   

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            _audioSource.PlayOneShot(clip); 
        }
    }
}
