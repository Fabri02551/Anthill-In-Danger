using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerAnt : MonoBehaviour
{
    public int maxLevel = 10; // Nivel m�ximo para cada atributo
    public float baseSpeed = 3f; // Velocidad base
    public float baseHealth = 15f; // Vida base
    public float baseDamage = 1000f; // Da�o base
    public float detectionRange = 5f; // Rango de detecci�n del enemigo
    private GameObject mainTarget; // Grill (objetivo principal)

    public int speedLevel = 1; // Nivel actual de velocidad
    public int healthLevel = 1; // Nivel actual de vida
    public int damageLevel = 1; // Nivel actual de da�o

    private float currentSpeed; // Velocidad calculada seg�n el nivel
    private float currentHealth; // Vida calculada seg�n el nivel
    private float currentDamage; // Da�o calculado seg�n el nivel

    private float health; // Vida actual
    private Rigidbody2D rb; // Referencia al Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateAttributes(); // Inicializa los valores seg�n los niveles actuales
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

        // Si el objetivo principal (Grill) est� asignado, se mueve hacia �l
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
            // Aplica da�o al enemigo
            Grill grill = collision.gameObject.GetComponent<Grill>();
            if (grill != null)
            {
                // Aplica el da�o y pasa el atacante (hormiga)
                grill.TakeDamage(currentDamage); // Da�o al Grill
                Debug.Log("Hormiga atac� al Grill. Da�o aplicado.");
            }
        }
        else
        {
            Debug.Log("Colisi�n con: " + collision.gameObject.name);
        }
    }



    // Actualiza los atributos basados en los niveles actuales
    public void UpdateAttributes()
    {
        currentSpeed = baseSpeed + (speedLevel - 1) * 0.5f; // Incrementa la velocidad en 0.5 por nivel
        currentHealth = baseHealth + (healthLevel - 1) * 2f; // Incrementa la vida en 2 por nivel
        currentDamage = baseDamage + (damageLevel - 1) * 1.5f; // Incrementa el da�o en 1.5 por nivel
    }

    // Moverse hacia el objetivo
    private void MoveTowardsTarget()
    {
        if (mainTarget != null)
        {
            // Mueve directamente hacia el objetivo Grill
            Vector2 direction = (mainTarget.transform.position - transform.position).normalized;
            rb.velocity = direction * currentSpeed; // Aplica el movimiento hacia el Grill
            RotateTowardsMovementDirection(direction); // Rota la hormiga hacia la direcci�n de movimiento
        }
    }

    // Gira la hormiga hacia la direcci�n de movimiento
    private void RotateTowardsMovementDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Ajusta el �ngulo seg�n el sprite
        }
    }

    // M�todos para subir de nivel cada atributo
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
            health = currentHealth; // Restaura la salud al nuevo m�ximo
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

    // M�todo para recibir da�o
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Hormiga recibi� da�o. Vida restante: {health}");
    }

    // M�todo para visualizar el rango de detecci�n en el Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
