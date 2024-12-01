using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cricket : MonoBehaviour
{
    public int health = 100;
    public int damageToAnthill = 10;
    public int damageToAnt = 20;
    public float attackInterval = 1f;
    public float speed = 2f;
    public float detectionRangeMultiplier = 2f;

    private Anthill anthill; // Referencia al hormiguero
    private GameObject targetAnt; // Hormiga objetivo
    private float attackTimer = 0f;
    private CircleCollider2D detectionRange;

    void Start()
    {
        // Encuentra el hormiguero en la escena
        anthill = FindObjectOfType<Anthill>();

        // Configura el rango de detección
        detectionRange = gameObject.AddComponent<CircleCollider2D>();
        detectionRange.isTrigger = true;
        detectionRange.radius = GetComponent<Collider2D>().bounds.size.x * detectionRangeMultiplier;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (targetAnt != null)
        {
            // Si hay una hormiga objetivo, muévete hacia ella
            MoveTowards(targetAnt.transform.position);

            // Si está cerca, ataca
            if (Vector2.Distance(transform.position, targetAnt.transform.position) < 0.5f)
            {
                AttackAnt();
            }
        }
        else if (anthill != null)
        {
            // Si no hay hormiga, muévete hacia el hormiguero
            MoveTowards(anthill.transform.position);

            // Si estás cerca del hormiguero, ataca
            if (Vector2.Distance(transform.position, anthill.transform.position) < 0.5f)
            {
                AttackAnthill();
            }
        }
    }

    void MoveTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void AttackAnthill()
    {
        if (attackTimer >= attackInterval)
        {
            anthill.TakeDamage(damageToAnthill);
            attackTimer = 0f;
        }
    }

    void AttackAnt()
    {
        if (attackTimer >= attackInterval)
        {
            // Reducir la vida de la hormiga y destruirla
            Destroy(targetAnt);
            attackTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ant"))
        {
            // Si detecta una hormiga, la convierte en objetivo
            targetAnt = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ant") && collision.gameObject == targetAnt)
        {
            // Si la hormiga sale del rango, deja de ser objetivo
            targetAnt = null;
        }
    }
}
