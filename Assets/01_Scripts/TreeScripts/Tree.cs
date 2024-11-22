using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject sheetPrefab; // Prefab de la hoja a instanciar
    public float spawnRadius = 5f; // Radio en el que se generan las hojas
    public float spawnInterval = 3f; // Intervalo de tiempo entre generación de hojas
    public int maxSheetsPerRound = 5; // Número máximo de hojas por ronda

    private int sheetsSpawned = 0; // Contador de hojas generadas

    private void Start()
    {
        StartCoroutine(SpawnSheetsCoroutine());
    }

    // Corutina para generar hojas cada cierto tiempo
    private IEnumerator SpawnSheetsCoroutine()
    {
        while (sheetsSpawned < maxSheetsPerRound)
        {
            SpawnSheet();
            sheetsSpawned++;
            yield return new WaitForSeconds(spawnInterval); // Pausa por 3 segundos (o el valor de spawnInterval)
        }
    }

    // Método para generar una sola hoja
    private void SpawnSheet()
    {
        // Generar posición aleatoria dentro del radio del árbol
        Vector3 randomPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
        GameObject sheet = Instantiate(sheetPrefab, randomPosition, Quaternion.identity);

        // Asignar propiedades aleatorias a la hoja
        Sheet sheetScript = sheet.GetComponent<Sheet>();
        if (sheetScript != null)
        {
            float size = Random.Range(0.5f, 2f); // Tamaño aleatorio
            sheetScript.SetProperties(size, size * 10); // Cantidad de comida basada en el tamaño
        }
    }
}
