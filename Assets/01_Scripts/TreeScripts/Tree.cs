using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject sheetPrefab; // Prefab de la hoja a instanciar
    public float spawnRadius = 5f; // Radio en el que se generan las hojas
    public float spawnInterval = 3f; // Intervalo de tiempo entre generaci�n de hojas
    public int maxSheetsPerRound = 5; // N�mero m�ximo de hojas por ronda

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

    // M�todo para generar una sola hoja
    private void SpawnSheet()
    {
        // Generar posici�n aleatoria dentro del radio del �rbol
        Vector3 randomPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
        GameObject sheet = Instantiate(sheetPrefab, randomPosition, Quaternion.identity);

        // Asignar propiedades aleatorias a la hoja
        Sheet sheetScript = sheet.GetComponent<Sheet>();
        if (sheetScript != null)
        {
            float size = Random.Range(0.5f, 2f); // Tama�o aleatorio
            sheetScript.SetProperties(size, size * 10); // Cantidad de comida basada en el tama�o
        }
    }
}
