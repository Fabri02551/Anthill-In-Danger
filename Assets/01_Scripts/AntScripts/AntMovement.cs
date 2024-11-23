using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidad de movimiento
    public float changeDirectionTime = 2f; // Tiempo entre cambios de direcci�n

    private Vector2 movementDirection; // Direcci�n actual de movimiento
    private Rigidbody2D rb; // Referencia al Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Cambia la direcci�n inicial al azar
        ChangeDirection();

        // Repite el cambio de direcci�n cada cierto tiempo
        InvokeRepeating("ChangeDirection", changeDirectionTime, changeDirectionTime);
    }

    void Update()
    {
        // Mueve la hormiga en la direcci�n establecida
        rb.velocity = movementDirection * moveSpeed;

        // Rotar la hormiga hacia la direcci�n de movimiento
        RotateTowardsMovementDirection();
    }

    // Cambia la direcci�n de movimiento aleatoriamente
    void ChangeDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        movementDirection = new Vector2(randomX, randomY).normalized; // Normaliza para mantener la misma velocidad
    }

    // Cambia la direcci�n si colisiona con algo
    void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
    }

    // Gira la hormiga hacia la direcci�n de movimiento
    void RotateTowardsMovementDirection()
    {
        if (movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Compensa porque el sprite apunta hacia arriba
        }
    }

}

