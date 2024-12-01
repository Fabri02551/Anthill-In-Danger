using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BaseUpgrade : MonoBehaviour
{
    public int baseHealth = 1000; // Vida inicial de la base
    public int maxAntCapacity = 10; // Capacidad máxima inicial de hormigas
    public int maxSheetCapacity = 50; // Capacidad máxima inicial de hojas
    public float healthRegenSpeed = 1f; // Velocidad de regeneración de vida (puntos por segundo)
    public int priceHeltCoin = 10, priceResourceCoin = 10, priceAntAmountCoin = 10, priceRegenerationCoin = 10;
    public int priceHeltSheets = 100, priceResourceSheets = 100, priceAntAmountSheets = 100, priceRegenerationSheets = 100;

    private int currentAntCount = 0; // Contador de hormigas activas
    private ResourceManager resourceManager;
    public Text[] coinPriceTexts;

    private int currentHealth; // Vida actual de la base

    private Coroutine healthRegenCoroutine; // Corutina de regeneración de vida
    private Anthill anthill;
    void Start()
    {
        anthill = FindObjectOfType<Anthill>();

        if (anthill == null)
        {
            Debug.LogError("No se encontró el script Anthill.");
        }
        resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager no encontrado en la escena.");
        }
        // Iniciar regeneración de vida utilizando corutina
        StartHealthRegen();
        UpdateAllCoinPrices();
    }

    private void StartHealthRegen()
    {
        StartCoroutine(RegenerateHealthCoroutine());
    }

    private IEnumerator RegenerateHealthCoroutine()
    {
        while (true)
        {
            if (anthill.health < 1000) // Si la salud del hormiguero es menor que su valor máximo
            {
                anthill.health += Mathf.FloorToInt(healthRegenSpeed); // Regeneramos la salud del hormiguero
                anthill.health = Mathf.Min(anthill.health, 1000); // Evitamos que se pase de 1000
                Debug.Log($"Salud del hormiguero regenerada. Salud actual: {anthill.health}/1000");
            }

            yield return new WaitForSeconds(5f); // Regenerar cada 5 segundos
        }
    }

    public void UpgradeAntCapacity(int increase)
    {
        if (resourceManager.coins >= priceAntAmountCoin && resourceManager.sheets >= priceAntAmountSheets)
        {
            resourceManager.RemoveCoins(priceAntAmountCoin);
            resourceManager.RemoveSheets(priceAntAmountSheets);
            priceAntAmountCoin += 5;
            priceAntAmountSheets += 50;
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
            Debug.Log($"Capacidad de vida mejorada a {baseHealth}");
            priceHeltCoin += 5;
            priceHeltSheets += 50;

            // Asegurarse de que la vida actual se mantenga dentro del nuevo máximo
            currentHealth = Mathf.Min(currentHealth, baseHealth);

            UpdateAllCoinPrices();
        }
    }

    public void UpgradeSheetCapacity(int increase)
    {
        if (resourceManager.coins >= priceResourceCoin && resourceManager.sheets >= priceResourceSheets)
        {
            resourceManager.RemoveCoins(priceResourceCoin);
            priceResourceCoin += 5;
            priceResourceSheets += 50;
            maxSheetCapacity += increase;
            Debug.Log($"Capacidad de hojas mejorada a {maxSheetCapacity}");
            UpdateAllCoinPrices();
        }
    }

    public void UpgradeHealthRegen(float increase)
    {
        if (resourceManager.coins >= priceRegenerationCoin && resourceManager.sheets >= priceRegenerationSheets)
        {
            resourceManager.RemoveCoins(priceRegenerationCoin);
            resourceManager.RemoveSheets(priceRegenerationSheets);
            healthRegenSpeed += increase;
            Debug.Log($"Velocidad de regeneración mejorada a {healthRegenSpeed}");
            priceRegenerationCoin += 5;
            priceRegenerationSheets += 50;
            UpdateAllCoinPrices();
        }
    }

    public void TakeDamage(int damage)
    {
        anthill.TakeDamage(damage); // Llamamos a la función TakeDamage del hormiguero
        Debug.Log($"Daño recibido. Salud del hormiguero actual: {anthill.health}/1000");
    }


    public bool CanAddAnt()
    {
        return currentAntCount < maxAntCapacity;
    }

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
