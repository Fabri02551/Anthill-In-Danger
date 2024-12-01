using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealhBar : MonoBehaviour
{
    public Image lifeBar; // Referencia a la imagen de la barra de vida (Image con tipo Filled)

    private Anthill anthill; // Referencia al script Anthill
    private BaseUpgrade baseUpgrade; // Referencia al script BaseUpgrade

    void Start()
    {
        // Busca automáticamente los scripts en la escena
        anthill = FindObjectOfType<Anthill>();
        baseUpgrade = FindObjectOfType<BaseUpgrade>();

        // Verifica que las referencias se hayan encontrado
        if (lifeBar == null)
        {
            Debug.LogError("LifeBar no asignado en el Inspector.");
            return;
        }

        if (anthill == null)
        {
            Debug.LogError("No se encontró un script Anthill en la escena.");
            return;
        }

        if (baseUpgrade == null)
        {
            Debug.LogError("No se encontró un script BaseUpgrade en la escena.");
            return;
        }

        // Inicializa la barra de vida con los valores actuales
        UpdateHealthBar();
    }

    void Update()
    {
        // Actualiza la barra de vida cada frame
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Obtiene la vida actual y el máximo
        int currentHealth = anthill.health;
        int maxHealth = baseUpgrade.baseHealth;

        // Calcula el porcentaje de vida
        float healthPercentage = Mathf.Clamp01((float)currentHealth / maxHealth);

        // Actualiza el valor de relleno de la barra
        lifeBar.fillAmount = healthPercentage;

        // Opción para debug:
        Debug.Log($"Barra de vida actualizada: {currentHealth}/{maxHealth} ({healthPercentage * 100:F1}%)");
    }
}