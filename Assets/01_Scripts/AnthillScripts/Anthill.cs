using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anthill : MonoBehaviour
{
    // Vida del hormiguero
    public int health = 1000;

    // Configuración del spawn
    public GameObject antPrefab; // Prefab de la hormiga
    public Transform[] spawnPoints; // Puntos donde aparecerán las hormigas
    public float spawnInterval = 3f; // Tiempo entre spawns
    public int maxAnts = 10; // Número máximo de hormigas activas
    private int currentAntCount = 0; // Número actual de hormigas activas

    // Referencia al ResourceManager
    private ResourceManager resourceManager;

    void Start()
    {
        // Buscar el ResourceManager en la escena
        resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager no encontrado en la escena.");
        }

        // Iniciar el spawn de hormigas
        InvokeRepeating("SpawnAnt", spawnInterval, spawnInterval);
    }

    void Update()
    {
        // Verifica si el hormiguero ha sido destruido
        if (health <= 0)
        {
            DestroyAnthill();
        }
    }

    // Método para reducir la vida del hormiguero
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Anthill Health: " + health);
    }

    // Método para destruir el hormiguero
    void DestroyAnthill()
    {
        Debug.Log("Anthill Destroyed!");
        CancelInvoke("SpawnAnt"); // Detiene el spawn de hormigas
        Destroy(gameObject); // Destruye el objeto del hormiguero
    }

    // Método para hacer spawn de hormigas
    void SpawnAnt()
    {
        // Verifica si se ha alcanzado el límite de hormigas activas
        if (currentAntCount >= maxAnts)
            return;

        // Selecciona un punto de spawn aleatorio
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Crea una hormiga en la posición del punto de spawn
        GameObject newAnt = Instantiate(antPrefab, spawnPoint.position, Quaternion.identity);

        // Incrementa el contador de hormigas activas
        currentAntCount++;
    }

    // Método para reducir el contador cuando una hormiga es destruida
    public void AntDestroyed()
    {
        currentAntCount--;
        Debug.Log("Ant destroyed! Current count: " + currentAntCount);
    }

    // NUEVA LÓGICA: Agregar recursos de hojas (sheets) al ResourceManager
    public void AddSheets(int amount)
    {
        if (resourceManager != null)
        {
            resourceManager.AddSheets(amount);
            Debug.Log($"Hojas añadidas al hormiguero: {amount} hojas.");
        }
        else
        {
            Debug.LogWarning("ResourceManager no está asignado. No se pueden agregar hojas.");
        }
    }

    // NUEVO MÉTODO: Detectar colisiones con las hojas
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisionó es una hoja
        Sheet sheet = other.GetComponent<Sheet>();
        if (sheet != null)
        {
            // Agregar la cantidad de comida de la hoja al ResourceManager
            AddSheets((int)sheet.foodAmount);

            // Destruir la hoja después de agregar los recursos
            Destroy(other.gameObject);

            Debug.Log($"Hoja recolectada: {sheet.foodAmount} comida añadida.");
        }
    }
}
