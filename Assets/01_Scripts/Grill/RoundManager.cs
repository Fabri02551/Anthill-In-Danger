using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject cricketPrefab; // Prefab del grillo
    public Transform topLeft; // Esquina superior izquierda del cuadrado
    public Transform bottomRight; // Esquina inferior derecha del cuadrado
    public int initialEnemyCount = 5; // Número inicial de enemigos por ronda
    public float timeBetweenRounds = 5f; // Tiempo entre rondas

    private int currentRound = 1; // Ronda actual
    private int currentEnemyCount; // Enemigos actuales por ronda
    private List<GameObject> activeEnemies = new List<GameObject>(); // Lista de enemigos activos

    void Start()
    {
        currentEnemyCount = initialEnemyCount;
        StartCoroutine(StartRounds());
    }

    private IEnumerator StartRounds()
    {
        while (true)
        {
            Debug.Log($"Iniciando ronda {currentRound}");
            SpawnEnemies();

            // Esperar hasta que todos los enemigos sean eliminados
            yield return new WaitUntil(() => activeEnemies.Count == 0);

            // Esperar un tiempo antes de iniciar la siguiente ronda
            Debug.Log($"Esperando {timeBetweenRounds} segundos para la próxima ronda");
            yield return new WaitForSeconds(timeBetweenRounds);

            // Incrementar dificultad cada 5 rondas
            if (currentRound % 5 == 0)
            {
                IncreaseDifficulty();
            }

            currentRound++;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < currentEnemyCount; i++)
        {
            Vector2 spawnPosition = GetRandomPointOnBorder();
            GameObject cricket = Instantiate(cricketPrefab, spawnPosition, Quaternion.identity);
            // Registrar evento de muerte para el grillo
            Cricket cricketScript = cricket.GetComponent<Cricket>();
            cricketScript.OnDeath += OnEnemyDeath;

            activeEnemies.Add(cricket);
        }

        Debug.Log($"Se han generado {currentEnemyCount} enemigos en la ronda {currentRound}");
    }

    private Vector2 GetRandomPointOnBorder()
    {
        float xMin = topLeft.position.x;
        float xMax = bottomRight.position.x;
        float yMin = bottomRight.position.y;
        float yMax = topLeft.position.y;

        // Elegir un borde aleatoriamente
        int border = Random.Range(0, 4); // 0: arriba, 1: abajo, 2: izquierda, 3: derecha
        switch (border)
        {
            case 0: // Borde superior
                return new Vector2(Random.Range(xMin, xMax), yMax);
            case 1: // Borde inferior
                return new Vector2(Random.Range(xMin, xMax), yMin);
            case 2: // Borde izquierdo
                return new Vector2(xMin, Random.Range(yMin, yMax));
            case 3: // Borde derecho
                return new Vector2(xMax, Random.Range(yMin, yMax));
            default:
                return Vector2.zero; // Esto nunca debería ocurrir
        }
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }

    private void IncreaseDifficulty()
    {
        currentEnemyCount += 2; // Incrementar el número de enemigos
        foreach (GameObject enemy in activeEnemies)
        {
            Cricket cricketScript = enemy.GetComponent<Cricket>();
            cricketScript.IncreaseAttributes(20, 5, 0.5f); // Incrementar atributos de los enemigos existentes
        }

        Debug.Log($"Dificultad incrementada en la ronda {currentRound}: +2 enemigos, +20 salud, +5 daño, +0.5 velocidad");
    }
}
