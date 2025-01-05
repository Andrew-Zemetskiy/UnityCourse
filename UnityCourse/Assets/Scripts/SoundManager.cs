using System;
using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Player")] [SerializeField] private Transform _rightStepPoint;
    [SerializeField] private Transform _leftStepPoint;
    [SerializeField] private float _intervalBetweenLeftAndRightSteps = 0.4f;
    [SerializeField] private float _intervalBetweenStepItself = 0.5f;

    [SerializeField] private GameObject _stepSoundPrefab;

    [Header("Another Audio")] [SerializeField]
    private GameObject _backgroundMusic;

    private bool _isFootstepsPlay = false;
    private bool _isFootstepsInPlaying = false;

    private Coroutine _stepCoroutine;

    private void Update()
    {
        PlayerStepsSpawning();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spawn");
        }
    }

    private void Start()
    {
        PlayerMovement.Instance.OnMove += OnPlayerMove;
        PlayBackgroundMusic();
    }

    private void OnDestroy()
    {
        PlayerMovement.Instance.OnMove -= OnPlayerMove;
    }

    private void OnPlayerMove(bool isMoving)
    {
        _isFootstepsPlay = isMoving;
        if (isMoving == false)
        {
            StopCoroutine(_stepCoroutine);
            _stepCoroutine = null;
            _isFootstepsInPlaying = false;
        }
    }

    private void PlayerStepsSpawning()
    {
        if (!_isFootstepsPlay || _isFootstepsInPlaying)
        {
            return;
        }

        _isFootstepsInPlaying = true;
        _stepCoroutine = StartCoroutine(StepsDelaySpawn());
    }

    private IEnumerator StepsDelaySpawn()
    {
        while (true)
        {
            // Debug.Log("SpawnRight");
            SpawnSound(_stepSoundPrefab, _rightStepPoint.position);
            yield return new WaitForSeconds(_intervalBetweenStepItself);
            // Debug.Log("SpawnLeft");
            SpawnSound(_stepSoundPrefab, _leftStepPoint.position);
            yield return new WaitForSeconds(_intervalBetweenLeftAndRightSteps);
        }
    }

    private void SpawnSound(GameObject soundPrefab, Vector3 position)
    {
        GameObject audioObject = Instantiate(soundPrefab, position, Quaternion.identity);
        if (audioObject.TryGetComponent<AudioSource>(out AudioSource audioSource) && audioSource.clip != null)
        {
            AudioClip clip = audioSource.clip;
            audioSource.Play();
            Destroy(audioObject, clip.length);
        }
        else
        {
            Debug.LogWarning("Audio clip could not be found");
            Destroy(audioObject, 1f);
        }
    }

    private void PlayBackgroundMusic()
    {
        GameObject bgMusic = Instantiate(_backgroundMusic);
        if (bgMusic.TryGetComponent<AudioSource>(out AudioSource audioSource))
        {
            audioSource.Play();
        }
    }
}