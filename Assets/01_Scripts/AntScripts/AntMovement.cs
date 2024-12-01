using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    // Atributos de la hormiga
    public int maxLevel = 10; // Nivel máximo para cada atributo
    public float baseSpeed = 2f; // Velocidad base
    public float baseHealth = 10f; // Vida base
    public float baseStrength = 5f; // Fuerza base

    public int speedLevel = 1; // Nivel actual de velocidad
    public int healthLevel = 1; // Nivel actual de vida
    public int strengthLevel = 1; // Nivel actual de fuerza

    private float currentSpeed; // Velocidad calculada según el nivel
    private float currentHealth; // Vida calculada según el nivel
    private float currentStrength; // Fuerza calculada según el nivel

    private Vector2 movementDirection; // Dirección actual de movimiento
    private Rigidbody2D rb; // Referencia al Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateAttributes(); // Inicializa los valores según los niveles actuales

        // Cambia la dirección inicial al azar
        ChangeDirection();

        // Repite el cambio de dirección cada cierto tiempo
        InvokeRepeating("ChangeDirection", 2f, 2f); // Ajusta el tiempo como desees
    }

    void Update()
    {
        // Mueve la hormiga con la velocidad actual
        rb.velocity = movementDirection * currentSpeed;

        // Rotar la hormiga hacia la dirección de movimiento
        RotateTowardsMovementDirection();
    }

    // Cambia la dirección de movimiento aleatoriamente
    void ChangeDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        movementDirection = new Vector2(randomX, randomY).normalized;
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

    // Actualiza los atributos basados en los niveles actuales
    public void UpdateAttributes()
    {
        currentSpeed = baseSpeed + (speedLevel - 1) * 0.5f; // Incrementa la velocidad en 0.5 por nivel
        currentHealth = baseHealth + (healthLevel - 1) * 2f; // Incrementa la vida en 2 por nivel
        currentStrength = baseStrength + (strengthLevel - 1) * 1f; // Incrementa la fuerza en 1 por nivel
    }

    // Métodos para subir de nivel cada atributo
    public void LevelUpSpeed()
    {
        if (speedLevel < maxLevel)
        {
            speedLevel++;
            UpdateAttributes();
            Debug.Log($"Velocidad subió a nivel {speedLevel}. Nueva velocidad: {currentSpeed}");
        }
        else
        {
            Debug.Log("Velocidad ya está en el nivel máximo.");
        }
    }

    public void LevelUpHealth()
    {
        if (healthLevel < maxLevel)
        {
            healthLevel++;
            UpdateAttributes();
            Debug.Log($"Vida subió a nivel {healthLevel}. Nueva vida: {currentHealth}");
        }
        else
        {
            Debug.Log("Vida ya está en el nivel máximo.");
        }
    }

    public void LevelUpStrength()
    {
        if (strengthLevel < maxLevel)
        {
            strengthLevel++;
            UpdateAttributes();
            Debug.Log($"Fuerza subió a nivel {strengthLevel}. Nueva fuerza: {currentStrength}");
        }
        else
        {
            Debug.Log("Fuerza ya está en el nivel máximo.");
        }
    }

    // Métodos para obtener los atributos actuales
    public float GetCurrentSpeed() => currentSpeed;
    public float GetCurrentHealth() => currentHealth;
    public float GetCurrentStrength() => currentStrength;
}
