using System;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public ParallaxLayer[] layers; // Массив слоев
    public float playerSpeed = 5f; // "Мнимая" скорость игрока
    private float direction = 1f; // Направление движения (1 — вправо, -1 — влево)

    void Update()
    {
        foreach (var layer in layers)
        {
            // Двигаем слой
            Vector3 layerPosition = layer.layerTransform.position;
            layerPosition.x -= playerSpeed * layer.parallaxFactor * direction * Time.deltaTime;
            layer.layerTransform.position = layerPosition;

            // Зацикливаем слой, если он выходит за пределы экрана
            if (direction > 0 && layer.layerTransform.position.x <= -layer.layerWidth)
            {
                layer.layerTransform.position += Vector3.right * layer.layerWidth * 2;
            }
            else if (direction < 0 && layer.layerTransform.position.x >= layer.layerWidth)
            {
                layer.layerTransform.position -= Vector3.right * layer.layerWidth * 2;
            }
        }

        // Смена направления
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction *= -1;
        }
    }
}