using UnityEngine;

public class Grill : MonoBehaviour
{
    public float health = 100f;  // Initial health of the grill
    public float damage = 10f;   // Damage of the grill
    public float size = 1f;      // Size of the grill
    private GameObject mainTarget;  // The anthill (main target)
    private GameObject secondaryTarget; // The target to attack when the grill takes damage

    private float moveSpeed = 2f; // Movement speed of the grill

    // Start is called before the first frame update
    private void Start()
    {
        // Assign the anthill as the main target
        mainTarget = GameObject.FindGameObjectWithTag("Anthill");
    }

    // This method is called to adjust grill's parameters based on the round
    public void SetupGrill(int round)
    {
        // Adjust the grill's properties based on the round
        health += health * 0.02f * round;
        damage += damage * 0.02f * round;
        size += size * 0.02f * round;

        // Adjust the visual size of the grill
        transform.localScale = new Vector3(size, size, size);
    }

    private void Update()
    {
        // If there's a secondary target (the attacker), move towards it
        if (secondaryTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, secondaryTarget.transform.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            // If no secondary target, move towards the anthill
            transform.position = Vector3.MoveTowards(transform.position, mainTarget.transform.position, Time.deltaTime * moveSpeed);
        }
    }

    // Method called when the grill takes damage
    public void TakeDamage(float amount, GameObject attacker)
    {
        health -= amount;

        // Change the target to the one that dealt damage
        if (health > 0)
        {
            secondaryTarget = attacker;
        }
        else
        {
            // If the grill dies, destroy it
            Destroy(gameObject);
        }
    }

    // Method to make the grill attack the anthill

}

