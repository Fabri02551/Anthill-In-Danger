using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidad de movimiento
    public float changeDirectionTime = 2f; // Tiempo entre cambios de dirección

    private Vector2 movementDirection; // Dirección actual de movimiento
    private Rigidbody2D rb; // Referencia al Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Cambia la dirección inicial al azar
        ChangeDirection();

        // Repite el cambio de dirección cada cierto tiempo
        InvokeRepeating("ChangeDirection", changeDirectionTime, changeDirectionTime);
    }

    void Update()
    {
        // Mueve la hormiga en la dirección establecida
        rb.velocity = movementDirection * moveSpeed;

        // Rotar la hormiga hacia la dirección de movimiento
        RotateTowardsMovementDirection();
    }

    // Cambia la dirección de movimiento aleatoriamente
    void ChangeDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        movementDirection = new Vector2(randomX, randomY).normalized; // Normaliza para mantener la misma velocidad
    }

    // Cambia la dirección si colisiona con algo
    void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
    }

    // Gira la hormiga hacia la dirección de movimiento
    void RotateTowardsMovementDirection()
    {
        if (movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Compensa porque el sprite apunta hacia arriba
        }
    }

}

