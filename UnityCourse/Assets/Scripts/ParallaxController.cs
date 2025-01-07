using System;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public ParallaxLayer[] layers;
    public float playerSpeed = 5f;
    private float _direction = 1f;

    private void Start()
    {
        Movement.OnPlayerChangeDir += () => _direction *= -1;
    }

    void Update()
    {
        LayersParallaxMove();
    }

    private void LayersParallaxMove()
    {
        foreach (var layer in layers)
        {
            Vector3 layerPosition = layer.layerTransform.position;
            layerPosition.x -= playerSpeed * layer.parallaxFactor * _direction * Time.deltaTime;
            layer.layerTransform.position = layerPosition;

            if (_direction > 0 && layer.layerTransform.position.x <= -layer.layerWidth)
            {
                layer.layerTransform.position += Vector3.right * (layer.layerWidth * 2);
            }
            else if (_direction < 0 && layer.layerTransform.position.x >= layer.layerWidth)
            {
                layer.layerTransform.position -= Vector3.right * (layer.layerWidth * 2);
            }
        }
    }
}