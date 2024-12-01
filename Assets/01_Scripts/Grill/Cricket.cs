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

    private Anthill anthill;
    private GameObject targetAnt;
    private float attackTimer = 0f;
    private CircleCollider2D detectionRange;

    public delegate void DeathHandler(GameObject enemy);
    public event DeathHandler OnDeath;

    void Start()
    {
        anthill = FindObjectOfType<Anthill>();
        detectionRange = gameObject.AddComponent<CircleCollider2D>();
        detectionRange.isTrigger = true;
        detectionRange.radius = GetComponent<Collider2D>().bounds.size.x * detectionRangeMultiplier;

    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (targetAnt != null)
        {
            MoveTowards(targetAnt.transform.position);

            if (Vector2.Distance(transform.position, targetAnt.transform.position) < 0.5f)
            {
                AttackAnt();
            }
        }
        else if (anthill != null)
        {
            MoveTowards(anthill.transform.position);

            if (Vector2.Distance(transform.position, anthill.transform.position) < 0.5f)
            {
                AttackAnthill();
                Debug.Log($"ataque ");

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
            targetAnt.GetComponent<AntMovement>().TakeDamage(damageToAnt);
            attackTimer = 0f;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ant"))
        {
            targetAnt = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ant") && collision.gameObject == targetAnt)
        {
            targetAnt = null;
        }
    }

    public void IncreaseAttributes(int healthIncrement, int damageIncrement, float speedIncrement)
    {
        health += healthIncrement;
        damageToAnthill += damageIncrement;
        damageToAnt += damageIncrement;
        speed += speedIncrement;
    }
}
