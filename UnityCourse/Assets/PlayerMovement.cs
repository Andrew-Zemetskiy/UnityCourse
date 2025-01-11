using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;

        // Получаем направление движения (только одна кнопка может быть нажата)
        Vector2 inputDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) // Вверх (вправо-вверх)
            inputDirection = new Vector2(1, 0.5f);
        else if (Input.GetKey(KeyCode.S)) // Вниз (влево-вниз)
            inputDirection = new Vector2(-1, -0.5f);
        else if (Input.GetKey(KeyCode.A)) // Влево (влево-вверх)
            inputDirection = new Vector2(-1, 0.5f);
        else if (Input.GetKey(KeyCode.D)) // Вправо (вправо-вниз)
            inputDirection = new Vector2(1, -0.5f);

        // Вычисляем движение
        Vector2 movement = inputDirection * moveSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;

        // Перемещаем персонажа
        rb.MovePosition(newPos);
    }
}