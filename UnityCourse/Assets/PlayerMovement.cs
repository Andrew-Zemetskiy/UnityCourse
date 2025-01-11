using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения

    private Vector2 inputDirection;

    private void Update()
    {
        // Получение ввода
        float inputX = Input.GetAxis("Horizontal"); // A/D или стрелки
        float inputY = Input.GetAxis("Vertical"); // W/S или стрелки

        // Вычисляем изометрическое направление
        Vector2 direction = new Vector2(inputX, inputY);

        // Угол поворота (45 градусов)
        float angle = -45f;


        float radians = angle * Mathf.Deg2Rad; // Угол в радианах
        float newX = direction.x * Mathf.Cos(radians) - direction.y * Mathf.Sin(radians);
        float newY = direction.x * Mathf.Sin(radians) + direction.y * Mathf.Cos(radians);
        Vector3 newDirection = new Vector3(newX, newY, 0f);

        
        transform.position += newDirection * Time.deltaTime;
    }
}