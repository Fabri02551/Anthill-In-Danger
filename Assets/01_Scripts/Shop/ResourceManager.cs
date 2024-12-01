using UnityEngine;
using UnityEngine.UI;
public class ResourceManager : MonoBehaviour
{
    public int coins = 0;
    public int sheets = 0;
    public Text[] coinPriceTexts;
    private BaseUpgrade baseUpgrade;

    void Start()
    {
        baseUpgrade = FindObjectOfType<BaseUpgrade>();
        if (baseUpgrade == null)
        {
            Debug.LogError("BaseUpgrade no encontrado en la escena.");
        }
    }
    private void Update()
    {
        UpdateAllCoinPrices();
    }
    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log($"Monedas añadidas: {amount}. Total monedas: {coins}");
    }

    public void RemoveCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            Debug.Log($"Monedas restadas: {amount}. Total monedas: {coins}");
        }
        else
        {
            Debug.Log("No hay suficientes monedas.");
        }
    }

    public void AddSheets(int amount)
    {
        if (sheets + amount <= baseUpgrade.maxSheetCapacity)
        {
            sheets += amount;
            Debug.Log($"Hojas añadidas: {amount}. Total hojas: {sheets}/{baseUpgrade.maxSheetCapacity}");
        }
        else
        {
            Debug.Log("Capacidad máxima de hojas alcanzada.");
        }
    }

    public void RemoveSheets(int amount)
    {
        if (sheets >= amount)
        {
            sheets -= amount;
            Debug.Log($"Hojas restadas: {amount}. Total hojas: {sheets}");
        }
        else
        {
            Debug.Log("No hay suficientes hojas.");
        }
    }
    public void UpdateAllCoinPrices()
    {
        coinPriceTexts[0].text = coins.ToString();
        coinPriceTexts[1].text = sheets.ToString();
    }
}
