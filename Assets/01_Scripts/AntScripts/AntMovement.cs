using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    // Atributos de la hormiga
    public int maxLevel = 10; // Nivel m�ximo para cada atributo
    public float baseSpeed = 2f; // Velocidad base
    public float baseHealth = 10f; // Vida base
    public float baseStrength = 5f; // Fuerza base

    public int speedLevel = 1; // Nivel actual de velocidad
    public int healthLevel = 1; // Nivel actual de vida
    public int strengthLevel = 1; // Nivel actual de fuerza

    private float currentSpeed; // Velocidad calculada seg�n el nivel
    private float currentHealth; // Vida calculada seg�n el nivel
    private float currentStrength; // Fuerza calculada seg�n el nivel

    private float health;

    private Vector2 movementDirection; // Direcci�n actual de movimiento
    private Rigidbody2D rb; // Referencia al Rigidbody2D

    // NUEVO: Referencias para transportar hojas
    private bool carryingLeaf = false; // Indica si la hormiga est� transportando una hoja
    private GameObject targetLeaf; // La hoja que la hormiga transporta
    private Anthill anthill; // Referencia al hormiguero

    void Start()
    {
        health = currentHealth;
        rb = GetComponent<Rigidbody2D>();
        UpdateAttributes(); // Inicializa los valores seg�n los niveles actuales

        // Buscar el hormiguero en la escena
        anthill = FindObjectOfType<Anthill>();
        if (anthill == null)
        {
            Debug.LogError("No se encontr� ning�n hormiguero en la escena.");
        }

        // Cambia la direcci�n inicial al azar
        ChangeDirection();

        // Repite el cambio de direcci�n cada cierto tiempo
        InvokeRepeating("ChangeDirection", 2f, 2f); // Ajusta el tiempo como desees
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        // Mueve la hormiga con la velocidad actual
        rb.velocity = movementDirection * currentSpeed;

        // Rotar la hormiga hacia la direcci�n de movimiento
        RotateTowardsMovementDirection();
    }

    // Cambia la direcci�n de movimiento aleatoriamente
    void ChangeDirection()
    {
        // Si est� cargando una hoja, no cambia de direcci�n
        if (carryingLeaf) return;

        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        movementDirection = new Vector2(randomX, randomY).normalized;
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

    // Actualiza los atributos basados en los niveles actuales
    public void UpdateAttributes()
    {
        currentSpeed = baseSpeed + (speedLevel - 1) * 0.5f; // Incrementa la velocidad en 0.5 por nivel
        currentHealth = baseHealth + (healthLevel - 1) * 2f; // Incrementa la vida en 2 por nivel
        currentStrength = baseStrength + (strengthLevel - 1) * 1f; // Incrementa la fuerza en 1 por nivel
    }

    public void LevelUpSpeed()
    {
        if (speedLevel < maxLevel)
        {
            speedLevel++;
            UpdateAttributes();
            Debug.Log($"Velocidad subi� a nivel {speedLevel}. Nueva velocidad: {currentSpeed}");
        }
        else
        {
            Debug.Log("Velocidad ya est� en el nivel m�ximo.");
        }
    }

    public void LevelUpHealth()
    {
        if (healthLevel < maxLevel)
        {
            healthLevel++;
            UpdateAttributes();
            Debug.Log($"Vida subi� a nivel {healthLevel}. Nueva vida: {currentHealth}");
            health += currentHealth;
        }
        else
        {
            Debug.Log("Vida ya est� en el nivel m�ximo.");
        }
    }

    public void LevelUpStrength()
    {
        if (strengthLevel < maxLevel)
        {
            strengthLevel++;
            UpdateAttributes();
            Debug.Log($"Fuerza subi� a nivel {strengthLevel}. Nueva fuerza: {currentStrength}");
        }
        else
        {
            Debug.Log("Fuerza ya est� en el nivel m�ximo.");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Ant Health: " + health);
    }
    // M�todo para ajustar la velocidad si est� transportando una hoja
    void UpdateSpeedForCarrying()
    {
        if (carryingLeaf)
        {
            currentSpeed = baseSpeed * 0.5f; // Reduce la velocidad cuando transporta una hoja
        }
        else
        {
            currentSpeed = baseSpeed + (speedLevel - 1) * 0.5f; // Velocidad normal si no lleva hoja
        }
    }
    public float GetCurrentSpeed() => currentSpeed;
    public float GetCurrentHealth() => currentHealth;
    public float GetCurrentStrength() => currentStrength;

    // NUEVO: Detectar colisiones con hojas y el hormiguero
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!carryingLeaf && collision.CompareTag("Leaf"))
        {
            // Recoger la hoja
            carryingLeaf = true;
            targetLeaf = collision.gameObject;

            // Cambiar direcci�n hacia el hormiguero
            movementDirection = (anthill.transform.position - transform.position).normalized;

            // Notificar la hoja que est� siendo transportada
            Sheet sheet = targetLeaf.GetComponent<Sheet>();
            if (sheet != null)
            {
                sheet.UpdatePosition(anthill.transform.position); // Actualiza la posici�n de la hoja
            }

            UpdateSpeedForCarrying(); // Ajusta la velocidad
            Debug.Log("Hoja recogida por la hormiga.");
        }
        else if (carryingLeaf && collision.CompareTag("Anthill"))
        {
            // Entregar la hoja al hormiguero
            if (targetLeaf != null)
            {
                Sheet sheet = targetLeaf.GetComponent<Sheet>();
                if (sheet != null)
                {
                    anthill.AddSheets((int)sheet.foodAmount);
                    sheet.ClearAndDestroy(); // Elimina la hoja

                }
            }

            carryingLeaf = false; // La hormiga ya no est� cargando la hoja
            targetLeaf = null; // Limpia la referencia
            Debug.Log("Hoja entregada al hormiguero.");
        }
    }
}
