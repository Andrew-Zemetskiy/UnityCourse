using System;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float moveSpeed = 1f;
    private Renderer _targetRenderer;

    private void Start()
    {
        if (targetObject.TryGetComponent<Renderer>(out Renderer renderer))
        {
            _targetRenderer = renderer;
        }
    }

    private void Update()
    {
        float t = Mathf.PingPong(Time.time * moveSpeed, 1f);

        ChangePos(t);
        if (_targetRenderer != null)
        {
            ChangeColor(t);
        }
    }

    private void ChangePos(in float t)
    {
        targetObject.transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }

    private void ChangeColor(in float t)
    {
        _targetRenderer.material.color = Color.Lerp(Color.blue, Color.yellow, t);
    }
}