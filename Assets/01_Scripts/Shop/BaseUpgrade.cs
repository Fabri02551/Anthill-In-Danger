using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class BaseUpgrade : MonoBehaviour
{
    public int baseHealth = 100; // Vida inicial de la base
    public int maxAntCapacity = 10; // Capacidad máxima inicial de hormigas
    public int maxSheetCapacity = 50; // Capacidad máxima inicial de hojas
    public float healthRegenSpeed = 1f; // Velocidad de regeneración de vida (puntos por segundo)
    public int priceHeltCoin = 10, priceResourceCoin = 10, priceAntAmountCoin = 10, priceRegenerationCoin = 10;
    public int priceHeltSheets = 100, priceResourceSheets = 100, priceAntAmountSheets = 100, priceRegenerationSheets = 100;
    private int currentAntCount = 0; // Contador de hormigas activas
    private ResourceManager resourceManager;
    public Text[] coinPriceTexts;
    void Start()
    {
        // Buscar el ResourceManager en la escena
        resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager no encontrado en la escena.");
        }

        // Iniciar regeneración de vida
        InvokeRepeating("RegenerateHealth", 1f, 1f / healthRegenSpeed);
        UpdateAllCoinPrices();
    }

    // Incrementar la capacidad máxima de hormigas
    public void UpgradeAntCapacity(int increase)
    {
        if (resourceManager.coins >= priceAntAmountCoin && resourceManager.sheets >= priceAntAmountSheets)
        {
            resourceManager.RemoveCoins(priceAntAmountCoin);
            resourceManager.RemoveSheets(priceAntAmountSheets);
            priceAntAmountCoin = priceAntAmountCoin + 5;
            priceAntAmountSheets = priceAntAmountSheets + 50;
            maxAntCapacity += increase;
            Debug.Log($"Capacidad de hormigas mejorada a {maxAntCapacity}");
            UpdateAllCoinPrices();
        }
           
    }
    public void UpgradeHealth(int increase)
    {
        if (resourceManager.coins >= priceHeltCoin && resourceManager.sheets >= priceHeltSheets)
        {
            resourceManager.RemoveCoins(priceHeltCoin); 
            resourceManager.RemoveSheets(priceHeltSheets);
            baseHealth += increase;
            Debug.Log($"Capacidad de vida mejorada {baseHealth}");
            priceHeltCoin = priceHeltCoin + 5;
            priceHeltSheets = priceHeltSheets + 50;
            UpdateAllCoinPrices();
        }
      
    }
    // Incrementar la capacidad máxima de hojas
    public void UpgradeSheetCapacity(int increase)
    {
        if (resourceManager.coins >= priceResourceCoin && resourceManager.sheets >= priceResourceSheets)
        {
            resourceManager.RemoveCoins(priceResourceCoin);
            priceResourceCoin = priceResourceCoin + 5;
            priceRegenerationSheets = priceResourceSheets + 50;
            maxSheetCapacity += increase;
            Debug.Log($"Capacidad de hojas mejorada a {maxSheetCapacity}");
            UpdateAllCoinPrices();
        }
            
    }

    // Incrementar la velocidad de regeneración de vida
    public void UpgradeHealthRegen(float increase)
    {
        if (resourceManager.coins >= priceRegenerationCoin && resourceManager.sheets >= priceRegenerationSheets)
        {
            resourceManager.RemoveCoins(priceRegenerationCoin);
            resourceManager.RemoveSheets(priceRegenerationSheets);
            healthRegenSpeed += increase;
            Debug.Log($"Velocidad de regeneración mejorada a {healthRegenSpeed}");
            priceRegenerationCoin = priceRegenerationCoin + 5;
            priceRegenerationSheets = priceRegenerationSheets + 50;
            UpdateAllCoinPrices();
        }
            
    }

    // Método para regenerar la salud de la base
    private void RegenerateHealth()
    {
        if (baseHealth < 100)
        {
            baseHealth++;
            Debug.Log($"Salud regenerada. Salud actual: {baseHealth}");
        }
    }

    // Verificar si hay capacidad para más hormigas
    public bool CanAddAnt()
    {
        return currentAntCount < maxAntCapacity;
    }

    // Incrementar el contador de hormigas
    public void AddAnt()
    {
        if (CanAddAnt())
        {
            currentAntCount++;
            Debug.Log($"Hormiga añadida. Total de hormigas: {currentAntCount}/{maxAntCapacity}");
        }
        else
        {
            Debug.Log("Capacidad máxima de hormigas alcanzada.");
        }
    }

    // Decrementar el contador de hormigas
    public void RemoveAnt()
    {
        if (currentAntCount > 0)
        {
            currentAntCount--;
            Debug.Log($"Hormiga eliminada. Total de hormigas: {currentAntCount}/{maxAntCapacity}");
        }
    }
    public void UpdateAllCoinPrices()
    {
        
            coinPriceTexts[0].text = priceHeltCoin.ToString(); 
            coinPriceTexts[1].text = priceHeltSheets.ToString(); 
            coinPriceTexts[2].text = priceAntAmountCoin.ToString();
            coinPriceTexts[3].text = priceAntAmountSheets.ToString(); 
            coinPriceTexts[4].text = priceResourceCoin.ToString();
            coinPriceTexts[5].text = priceResourceSheets.ToString();
            coinPriceTexts[6].text = priceRegenerationCoin.ToString();
            coinPriceTexts[7].text = priceRegenerationSheets.ToString();
            coinPriceTexts[8].text = baseHealth.ToString();
            coinPriceTexts[9].text = maxAntCapacity.ToString();
            coinPriceTexts[10].text = maxSheetCapacity.ToString();
            coinPriceTexts[11].text = healthRegenSpeed.ToString();
        
    }
}
