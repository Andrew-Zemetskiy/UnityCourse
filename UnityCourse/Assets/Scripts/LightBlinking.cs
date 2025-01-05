using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LightBlinking : MonoBehaviour
{
    [SerializeField] private Light _bulbLight;
    [SerializeField] private int _amountOfBlink = 3;
    [SerializeField] private float _intervalBetweenOnOffState = 0.05f;
    [SerializeField] private float _intervalBetweenOffOnState = 0.1f;
    [SerializeField] private float _intervalBetweenBlinkingCycle = 2f;
    
    private float _lightBaseIntensity;
    private float _lightZeroIntensity = 0f;

    private void Start()
    {
        _lightBaseIntensity = _bulbLight.intensity;
        StartBlinking();
    }
    
    private void StartBlinking()
    {
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            for (int i = 0; i < _amountOfBlink; i++)
            {
                _bulbLight.intensity = _lightBaseIntensity;
                yield return new WaitForSeconds(_intervalBetweenOnOffState);
                _bulbLight.intensity = _lightZeroIntensity;
                yield return new WaitForSeconds(_intervalBetweenOffOnState);
            }
            yield return new WaitForSeconds(_intervalBetweenBlinkingCycle);
        }
    }
}