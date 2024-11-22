using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidad de movimiento de la hormiga
    public float changeDirectionTime = 2f; // Tiempo entre cambios de dirección

    private Vector2 movementDirection; // Dirección actual de movimiento
    public Rigidbody2D rb; // Referencia al Rigidbody2D

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
    }

    // Cambia la dirección de movimiento aleatoriamente
    void ChangeDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        movementDirection = new Vector2(randomX, randomY).normalized; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
    }
}
