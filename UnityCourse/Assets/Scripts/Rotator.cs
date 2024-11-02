using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private Vector3 rotateDirection;

    [SerializeField] private float minRotationSpeed = 2f;
    [SerializeField] private float maxnRotationSpeed = 10f;
    [SerializeField] private float rotationChangeSpeed = 1f;
    
    private float _currentRotationSpeed;

    private void Start()
    {
        _currentRotationSpeed = minRotationSpeed;
    }

    private void Update()
    {
        ChangeRotationSpeed();
        RotateObject();
    }

    private void ChangeRotationSpeed()
    {
        float t = Mathf.PingPong(Time.time * rotationChangeSpeed, 1);
        _currentRotationSpeed = Mathf.Lerp(minRotationSpeed, maxnRotationSpeed, t);
    }

    private void RotateObject()
    {
        transform.Rotate(_currentRotationSpeed*Time.deltaTime*rotateDirection, Space.World);
    }
}
