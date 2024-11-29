using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesShop : MonoBehaviour
{
    private ResourceManager resourceManager;

    // Configuración de costos
    public int sheetsToCoinsRate = 100; // 100 hojas → 5 monedas
    public int coinsFromSheets = 5;

    public int coinsToSheetsRate = 5; // 5 monedas → 75 hojas
    public int sheetsFromCoins = 75;

    void Start()
    {
        // Encontrar el ResourceManager en la escena
        resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager no encontrado en la escena.");
        }
    }

    // Método para intercambiar hojas por monedas
    public void TradeSheetsForCoins()
    {
        if (resourceManager.sheets >= sheetsToCoinsRate)
        {
            resourceManager.RemoveSheets(sheetsToCoinsRate);
            resourceManager.AddCoins(coinsFromSheets);
            Debug.Log($"Intercambio completado: {sheetsToCoinsRate} hojas por {coinsFromSheets} monedas.");
        }
        else
        {
            Debug.Log("No tienes suficientes hojas para este intercambio.");
        }
    }

    // Método para intercambiar monedas por hojas
    public void TradeCoinsForSheets()
    {
        if (resourceManager.coins >= coinsToSheetsRate)
        {
            resourceManager.RemoveCoins(coinsToSheetsRate);
            resourceManager.AddSheets(sheetsFromCoins);
            Debug.Log($"Intercambio completado: {coinsToSheetsRate} monedas por {sheetsFromCoins} hojas.");
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para este intercambio.");
        }
    }
}
