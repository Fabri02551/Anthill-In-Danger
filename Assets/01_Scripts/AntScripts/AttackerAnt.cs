using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerAnt : MonoBehaviour
{
    public int maxLevel = 10; // Nivel máximo para cada atributo
    public float baseSpeed = 3f; // Velocidad base
    public float baseHealth = 15f; // Vida base
    public float baseDamage = 1000f; // Daño base
    public float detectionRange = 5f; // Rango de detección del enemigo
    private GameObject mainTarget; // Grill (objetivo principal)

    public int speedLevel = 1; // Nivel actual de velocidad
    public int healthLevel = 1; // Nivel actual de vida
    public int damageLevel = 1; // Nivel actual de daño

    private float currentSpeed; // Velocidad calculada según el nivel
    private float currentHealth; // Vida calculada según el nivel
    private float currentDamage; // Daño calculado según el nivel

    private float health; // Vida actual
    private Rigidbody2D rb; // Referencia al Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateAttributes(); // Inicializa los valores según los niveles actuales
        health = currentHealth;

        mainTarget = GameObject.FindGameObjectWithTag("Grill"); // Asignamos al Grill como objetivo
        if (mainTarget == null)
        {
            Debug.LogError("No se ha encontrado un Grill en la escena.");
        }
    }

    void Update()
    {
        // Destruye la hormiga si la vida llega a 0
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        // Si el objetivo principal (Grill) está asignado, se mueve hacia él
        if (mainTarget != null)
        {
            MoveTowardsTarget();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta si colisiona con un enemigo llamado Grill
        if (collision.gameObject.CompareTag("Grill"))
        {
            // Aplica daño al enemigo
            Grill grill = collision.gameObject.GetComponent<Grill>();
            if (grill != null)
            {
                // Aplica el daño y pasa el atacante (hormiga)
                grill.TakeDamage(currentDamage); // Daño al Grill
                Debug.Log("Hormiga atacó al Grill. Daño aplicado.");
            }
        }
        else
        {
            Debug.Log("Colisión con: " + collision.gameObject.name);
        }
    }



    // Actualiza los atributos basados en los niveles actuales
    public void UpdateAttributes()
    {
        currentSpeed = baseSpeed + (speedLevel - 1) * 0.5f; // Incrementa la velocidad en 0.5 por nivel
        currentHealth = baseHealth + (healthLevel - 1) * 2f; // Incrementa la vida en 2 por nivel
        currentDamage = baseDamage + (damageLevel - 1) * 1.5f; // Incrementa el daño en 1.5 por nivel
    }

    // Moverse hacia el objetivo
    private void MoveTowardsTarget()
    {
        if (mainTarget != null)
        {
            // Mueve directamente hacia el objetivo Grill
            Vector2 direction = (mainTarget.transform.position - transform.position).normalized;
            rb.velocity = direction * currentSpeed; // Aplica el movimiento hacia el Grill
            RotateTowardsMovementDirection(direction); // Rota la hormiga hacia la dirección de movimiento
        }
    }

    // Gira la hormiga hacia la dirección de movimiento
    private void RotateTowardsMovementDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Ajusta el ángulo según el sprite
        }
    }

    // Métodos para subir de nivel cada atributo
    public void LevelUpSpeed()
    {
        if (speedLevel < maxLevel)
        {
            speedLevel++;
            UpdateAttributes();
        }
    }

    public void LevelUpHealth()
    {
        if (healthLevel < maxLevel)
        {
            healthLevel++;
            UpdateAttributes();
            health = currentHealth; // Restaura la salud al nuevo máximo
        }
    }

    public void LevelUpDamage()
    {
        if (damageLevel < maxLevel)
        {
            damageLevel++;
            UpdateAttributes();
        }
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Hormiga recibió daño. Vida restante: {health}");
    }

    // Método para visualizar el rango de detección en el Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
