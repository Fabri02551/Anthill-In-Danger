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
    private void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        UpdateAllCoinPrices();
    }
    public void UpgradeAllAntsSpeed()
    {
        // Encuentra todas las hormigas en escena que tengan el script AntMovement
        AntMovement[] ants = FindObjectsOfType<AntMovement>();
        if (resourceManager.coins >= priceSpeedCoin)
        {
            foreach (AntMovement ant in ants)
            {
                ant.LevelUpSpeed(); 
            }
            resourceManager.RemoveCoins(priceSpeedCoin);
            priceSpeedCoin = priceSpeedCoin + 5;
            UpdateAllCoinPrices();
        }
    }

    public void UpgradeAllAntsHealth()
    {
        // Encuentra todas las hormigas en escena que tengan el script AntMovement
        AntMovement[] ants = FindObjectsOfType<AntMovement>();
        if (resourceManager.coins >= priceHealthCoin)
        {
            foreach (AntMovement ant in ants)
            {
            ant.LevelUpHealth(); // Sube el nivel de vida de cada hormiga
            }
            resourceManager.RemoveCoins(priceHealthCoin);
            priceHealthCoin = priceHealthCoin + 5;
            UpdateAllCoinPrices();
        }
    }

    public void UpgradeAllAntsStrength()
    {
        // Encuentra todas las hormigas en escena que tengan el script AntMovement
        AntMovement[] ants = FindObjectsOfType<AntMovement>();
        if (resourceManager.coins >= priceStrengthCoin)
        {
            foreach (AntMovement ant in ants)
            {
            ant.LevelUpStrength(); // Sube el nivel de fuerza de cada hormiga
            }
            resourceManager.RemoveCoins(priceStrengthCoin);
            priceStrengthCoin = priceStrengthCoin + 5;
            UpdateAllCoinPrices();
    }
}
    public void UpdateAllCoinPrices()
    {
        coinPriceTexts[0].text = priceHealthCoin.ToString();
        coinPriceTexts[1].text = priceStrengthCoin.ToString();
        coinPriceTexts[2].text = priceSpeedCoin.ToString();
    }
}
