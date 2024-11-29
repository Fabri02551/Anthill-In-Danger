using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class BaseUpgrade : MonoBehaviour
{
    public int baseHealth = 100; // Vida inicial de la base
    public int maxAntCapacity = 10; // Capacidad máxima inicial de hormigas
    public int maxSheetCapacity = 50; // Capacidad máxima inicial de hojas
    public float healthRegenSpeed = 1f; // Velocidad de regeneración de vida (puntos por segundo)

    private int currentAntCount = 0; // Contador de hormigas activas
    private ResourceManager resourceManager;

    void Start()
    {
        // Buscar el ResourceManager en la escena
        resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager no encontrado en la escena.");
        }

        // Iniciar regeneración de vida
        InvokeRepeating("RegenerateHealth", 1f, 1f / healthRegenSpeed);
    }

    // Incrementar la capacidad máxima de hormigas
    public void UpgradeAntCapacity(int increase)
    {
        maxAntCapacity += increase;
        Debug.Log($"Capacidad de hormigas mejorada a {maxAntCapacity}");
    }

    // Incrementar la capacidad máxima de hojas
    public void UpgradeSheetCapacity(int increase)
    {
        maxSheetCapacity += increase;
        Debug.Log($"Capacidad de hojas mejorada a {maxSheetCapacity}");
    }

    // Incrementar la velocidad de regeneración de vida
    public void UpgradeHealthRegen(float increase)
    {
        healthRegenSpeed += increase;
        Debug.Log($"Velocidad de regeneración mejorada a {healthRegenSpeed}");
    }

    // Método para regenerar la salud de la base
    private void RegenerateHealth()
    {
        if (baseHealth < 100)
        {
            baseHealth++;
            Debug.Log($"Salud regenerada. Salud actual: {baseHealth}");
        }
    }

    // Verificar si hay capacidad para más hormigas
    public bool CanAddAnt()
    {
        return currentAntCount < maxAntCapacity;
    }

    // Incrementar el contador de hormigas
    public void AddAnt()
    {
        if (CanAddAnt())
        {
            currentAntCount++;
            Debug.Log($"Hormiga añadida. Total de hormigas: {currentAntCount}/{maxAntCapacity}");
        }
        else
        {
            Debug.Log("Capacidad máxima de hormigas alcanzada.");
        }
    }

    // Decrementar el contador de hormigas
    public void RemoveAnt()
    {
        if (currentAntCount > 0)
        {
            currentAntCount--;
            Debug.Log($"Hormiga eliminada. Total de hormigas: {currentAntCount}/{maxAntCapacity}");
        }
    }
}
