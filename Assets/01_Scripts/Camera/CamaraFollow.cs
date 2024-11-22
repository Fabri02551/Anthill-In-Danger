using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{


    public float moveSpeed = 10f; // Velocidad de movimiento de la cámara
    public float edgeThickness = 10f; // Distancia del borde para activar el movimiento

    // Límites opcionales para la cámara
    public float minX = -50f; // Límite izquierdo
    public float maxX = 50f;  // Límite derecho
    public float minY = -50f; // Límite inferior
    public float maxY = 50f;  // Límite superior
    void Start()
    {
        
    }
    void Update()
    {
        // Obtener la posición actual de la cámara
        Vector3 pos = transform.position;

        // Movimiento horizontal
        if (Input.mousePosition.x >= Screen.width - edgeThickness) // Mover derecha
        {
            pos.x += moveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= edgeThickness) // Mover izquierda
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }

        // Movimiento vertical
        if (Input.mousePosition.y >= Screen.height - edgeThickness) // Mover arriba
        {
            pos.y += moveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= edgeThickness) // Mover abajo
        {
            pos.y -= moveSpeed * Time.deltaTime;
        }

        // Limitar la posición dentro de los límites establecidos
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Actualizar la posición de la cámara
        transform.position = pos;
    }

}
