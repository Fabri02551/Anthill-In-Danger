using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAnts : MonoBehaviour
{
    private ResourceManager resourceManager;

    public Text[] coinPriceTexts;
    public int priceHealthCoin = 10, priceSpeedCoin = 10, priceStrengthCoin = 10;

    // Niveles globales
    public int globalSpeedLevel = 1;
    public int globalHealthLevel = 1;
    public int globalStrengthLevel = 1;

    private void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        UpdateAllCoinPrices();
    }

    public void UpgradeAllAntsSpeed()
    {
        if (resourceManager.coins >= priceSpeedCoin)
        {
            globalSpeedLevel++; // Incrementa el nivel global
            UpdateAllAntsAttributes();
            resourceManager.RemoveCoins(priceSpeedCoin);
            priceSpeedCoin += 5;
            UpdateAllCoinPrices();
        }
    }

    public void UpgradeAllAntsHealth()
    {
        if (resourceManager.coins >= priceHealthCoin)
        {
            globalHealthLevel++; // Incrementa el nivel global
            UpdateAllAntsAttributes();
            resourceManager.RemoveCoins(priceHealthCoin);
            priceHealthCoin += 5;
            UpdateAllCoinPrices();
        }
    }

    public void UpgradeAllAntsStrength()
    {
        if (resourceManager.coins >= priceStrengthCoin)
        {
            globalStrengthLevel++; // Incrementa el nivel global
            UpdateAllAntsAttributes();
            resourceManager.RemoveCoins(priceStrengthCoin);
            priceStrengthCoin += 5;
            UpdateAllCoinPrices();
        }
    }

    // Actualiza todos los atributos de las hormigas existentes
    private void UpdateAllAntsAttributes()
    {
        AntMovement[] ants = FindObjectsOfType<AntMovement>();
        foreach (AntMovement ant in ants)
        {
            ant.speedLevel = globalSpeedLevel;
            ant.healthLevel = globalHealthLevel;
            ant.strengthLevel = globalStrengthLevel;
            ant.UpdateAttributes(); // Aplica los cambios
        }
    }

    public void UpdateAllCoinPrices()
    {
        coinPriceTexts[0].text = priceHealthCoin.ToString();
        coinPriceTexts[1].text = priceStrengthCoin.ToString();
        coinPriceTexts[2].text = priceSpeedCoin.ToString();
    }
}

