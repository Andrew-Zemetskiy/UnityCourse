using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLine_Manager : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private PlayableAsset _deathTimeLineAsset;

    private void OnEnable()
    {
        PlayerMove.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        PlayerMove.OnPlayerDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Player Death react (TimeLine)");
        _director.playableAsset = _deathTimeLineAsset;
        _director.Play();
    }
}