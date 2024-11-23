using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheet : MonoBehaviour
{
    public float size; // Tama�o de la hoja
    public float foodAmount; // Cantidad de comida en la hoja

    // M�todo para asignar las propiedades de la hoja
    public void SetProperties(float newSize, float newFoodAmount)
    {
        size = newSize;
        foodAmount = newFoodAmount;

        // Cambiar el tama�o visual de la hoja
        transform.localScale = Vector3.one * size;
    }

    private void OnDrawGizmos()
    {
        // Para visualizaci�n, dibuja la hoja en la escena
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, size / 2);
    }
}
