using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Anthill : MonoBehaviour
{
    // Vida del hormiguero
    public int health = 500;

    // Configuración del spawn
    public GameObject antPrefab; // Prefab de la hormiga
    public Transform[] spawnPoints; // Puntos donde aparecerán las hormigas
    public float spawnInterval = 3f; // Tiempo entre spawns
    public int maxAnts = 10; // Número máximo de hormigas activas

    private int currentAntCount = 0; // Número actual de hormigas activas

    void Start()
    {
        
    
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
        Instantiate(antPrefab, spawnPoint.position, Quaternion.identity);

        // Incrementa el contador de hormigas activas
        currentAntCount++;
    }

    // Método para reducir el contador cuando una hormiga es destruida
    public void AntDestroyed()
    {
        currentAntCount--;
        Debug.Log("Ant destroyed! Current count: " + currentAntCount);
    }
}

