using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{


    public float moveSpeed = 10f; // Velocidad de movimiento de la c�mara
    public float edgeThickness = 10f; // Distancia del borde para activar el movimiento

    // L�mites opcionales para la c�mara
    public float minX = -50f; // L�mite izquierdo
    public float maxX = 50f;  // L�mite derecho
    public float minY = -50f; // L�mite inferior
    public float maxY = 50f;  // L�mite superior
    void Start()
    {
        
    }
    void Update()
    {
        // Obtener la posici�n actual de la c�mara
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

        // Limitar la posici�n dentro de los l�mites establecidos
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Actualizar la posici�n de la c�mara
        transform.position = pos;
    }

}
