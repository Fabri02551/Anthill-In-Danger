using UnityEngine;

public class Grill : MonoBehaviour
{
    public float health = 100f;  // Vida inicial del Grill
    public float damage = 10f;   // Da�o del Grill
    public float size = 1f;      // Tama�o del Grill
    private GameObject mainTarget;  // El hormiguero (objetivo principal)
    private GameObject secondaryTarget; // El objetivo secundario cuando el Grill recibe da�o

    private float moveSpeed = 2f; // Velocidad de movimiento del Grill

    // Start es llamado antes de la primera actualizaci�n del frame
    private void Start()
    {
        // Asignamos el hormiguero como objetivo principal
        mainTarget = GameObject.FindGameObjectWithTag("Anthill");
    }

    // Este m�todo ajusta los par�metros del Grill basados en el nivel de la hormiga atacante
    public void SetupGrill(int antLevel)
    {
        // Ajusta las propiedades del Grill en funci�n del nivel de la hormiga
        health += health * 0.05f * antLevel;  // Aumenta la vida en funci�n del nivel de la hormiga
        damage += damage * 0.05f * antLevel;  // Aumenta el da�o en funci�n del nivel de la hormiga
        size += size * 0.02f * antLevel;     // Aumenta el tama�o en funci�n del nivel de la hormiga

        // Ajusta el tama�o visual del Grill
        transform.localScale = new Vector3(size, size, size);
    }

    private void Update()
    {
        // Si hay un objetivo secundario (la hormiga atacante), el Grill se mueve hacia �l
        if (secondaryTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, secondaryTarget.transform.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            // Si no hay objetivo secundario, el Grill se mueve hacia el hormiguero
            transform.position = Vector3.MoveTowards(transform.position, mainTarget.transform.position, Time.deltaTime * moveSpeed);
        }
    }

    // M�todo llamado cuando el Grill recibe da�o
    // M�todo llamado cuando el Grill recibe da�o
    public void TakeDamage(float amount)
    {
        health -= amount; // Disminuye la salud

        // Si la vida del Grill llega a 0 o menos, destruye el objeto
        if (health <= 0)
        {
            Destroy(gameObject); // Destruye el Grill
            Debug.Log("Grill ha muerto.");
        }
        else
        {
            Debug.Log("Grill recibi� da�o. Vida restante: " + health);
        }
    }

}
