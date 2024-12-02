using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSpecialAnt : MonoBehaviour
{
    private ResourceManager resourceManager;

    public Text[] coinPriceTexts; // 0: Vida, 1: Daño, 2: Velocidad
    public int priceHealthCoin = 10, priceSpeedCoin = 10, priceDamageCoin = 10;

    // Niveles exclusivos para la hormiga especial
    public int specialSpeedLevel = 1;
    public int specialHealthLevel = 1;
    public int specialDamageLevel = 1;

    private void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        UpdateAllCoinPrices();
    }

    public void UpgradeSpecialAntSpeed()
    {
        if (resourceManager.coins >= priceSpeedCoin)
        {
            specialSpeedLevel++; // Incrementa nivel exclusivo
            UpdateSpecialAntAttributes();
            resourceManager.RemoveCoins(priceSpeedCoin);
            priceSpeedCoin += 5; // Incremento progresivo del costo
            UpdateAllCoinPrices();
        }
    }

    public void UpgradeSpecialAntHealth()
    {
        if (resourceManager.coins >= priceHealthCoin)
        {
            specialHealthLevel++; // Incrementa nivel exclusivo
            UpdateSpecialAntAttributes();
            resourceManager.RemoveCoins(priceHealthCoin);
            priceHealthCoin += 5; // Incremento progresivo del costo
            UpdateAllCoinPrices();
        }
    }

    public void UpgradeSpecialAntDamage()
    {
        if (resourceManager.coins >= priceDamageCoin)
        {
            specialDamageLevel++; // Incrementa nivel exclusivo
            UpdateSpecialAntAttributes();
            resourceManager.RemoveCoins(priceDamageCoin);
            priceDamageCoin += 5; // Incremento progresivo del costo
            UpdateAllCoinPrices();
        }
    }

    private void UpdateSpecialAntAttributes()
    {
        AttackerAnt[] ants = FindObjectsOfType<AttackerAnt>();
        foreach (AttackerAnt ant in ants)
        {
            ant.speedLevel = specialSpeedLevel;
            ant.healthLevel = specialHealthLevel;
            ant.damageLevel = specialDamageLevel;
            ant.UpdateAttributes(); // Aplica los cambios exclusivos
        }
    }

    private void UpdateAllCoinPrices()
    {
        coinPriceTexts[0].text = priceHealthCoin.ToString();
        coinPriceTexts[1].text = priceDamageCoin.ToString();
        coinPriceTexts[2].text = priceSpeedCoin.ToString();
    }
}
