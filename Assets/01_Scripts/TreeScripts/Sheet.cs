using System.Collections.Generic;
using UnityEngine;

public class Sheet : MonoBehaviour
{
    public float size; // Tamaño de la hoja
    public float foodAmount; // Cantidad de comida en la hoja
    public int maxAnts; // Máxima cantidad de hormigas permitidas
    public float weight; // Peso de la hoja

    private List<AntMovement> carryingAnts = new List<AntMovement>();

    private Vector3 targetPosition; // Posición objetivo para la hoja
    private float moveSpeed = 1f; // Velocidad de movimiento de la hoja (lento)

    /// <summary>
    /// Inicializa las propiedades de la hoja.
    /// </summary>
    /// <param name="newSize">Tamaño de la hoja.</param>
    /// <param name="newFoodAmount">Cantidad de comida en la hoja.</param>
    public void SetProperties(float newSize, float newFoodAmount)
    {
        size = newSize;
        foodAmount = newFoodAmount;
        weight = size * 10; // Peso proporcional al tamaño
        maxAnts = Mathf.CeilToInt(size); // Máximo de hormigas depende del tamaño
        transform.localScale = Vector3.one * (size * 0.3f); // Ajusta visualmente el tamaño
        targetPosition = transform.position; // Inicializa la posición objetivo
    }

    /// <summary>
    /// Actualiza la posición de la hoja cuando una hormiga la transporta.
    /// </summary>
    public void UpdatePosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }

    /// <summary>
    /// Mueve la hoja hacia la posición objetivo de forma lenta.
    /// </summary>
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Intenta asignar una hormiga para transportar esta hoja.
    /// </summary>
    /// <param name="ant">Referencia a la hormiga que intenta cargar la hoja.</param>
    /// <returns>True si la hormiga fue asignada, false si no hay más espacio.</returns>
    public bool AddAnt(AntMovement ant)
    {
        if (carryingAnts.Count < maxAnts)
        {
            carryingAnts.Add(ant);
            return true;
        }
        return false; // No se pueden añadir más hormigas
    }

    /// <summary>
    /// Remueve una hormiga de la lista de transporte.
    /// </summary>
    /// <param name="ant">Referencia a la hormiga que termina de cargar.</param>
    public void RemoveAnt(AntMovement ant)
    {
        if (carryingAnts.Contains(ant))
        {
            carryingAnts.Remove(ant);
        }
    }

    /// <summary>
    /// Verifica si la hoja está completamente cargada.
    /// </summary>
    /// <returns>True si el número máximo de hormigas está asignado, false de lo contrario.</returns>
    public bool IsFullyCarried()
    {
        return carryingAnts.Count >= maxAnts;
    }

    /// <summary>
    /// Limpia la hoja y la elimina cuando ya no está en uso.
    /// </summary>
    public void ClearAndDestroy()
    {
        carryingAnts.Clear();
        Destroy(gameObject);
    }
}
