using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class BuyAnts : MonoBehaviour
{
    public GameObject antPrefab;
    public GameObject antWarriorPrefrab;
    public int antQueen;
    public Transform spawnPoint;
    public int antCost = 10;
    public int antQueenCost =210,antQueenCostLeaf =2200;
    public float timeAnt =20f, timeResoruces=15.0f;
    private ResourceManager resourceManager;
    private BaseUpgrade baseUpgrade;

    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        baseUpgrade = FindObjectOfType<BaseUpgrade>();
        antQueen = 0;
        if (resourceManager == null || baseUpgrade == null)
        {
            Debug.LogError("ResourceManager o BaseUpgrade no encontrado en la escena.");
        }
    }

    public void BuyAnt()
    {
        if (resourceManager != null && baseUpgrade != null)
        {
            if (resourceManager.coins >= antCost && baseUpgrade.CanAddAnt())
            {
                resourceManager.RemoveCoins(antCost);
                baseUpgrade.AddAnt();

                GameObject newAnt = Instantiate(antPrefab, spawnPoint.position, Quaternion.identity);

                // Ajustar los niveles de la nueva hormiga
                AntMovement antMovement = newAnt.GetComponent<AntMovement>();
                if (antMovement != null)
                {
                    // Sincroniza con los niveles globales
                    UpgradeAnts upgradeAnts = FindObjectOfType<UpgradeAnts>();
                    antMovement.speedLevel = upgradeAnts.globalSpeedLevel;
                    antMovement.healthLevel = upgradeAnts.globalHealthLevel;
                    antMovement.strengthLevel = upgradeAnts.globalStrengthLevel;
                    antMovement.UpdateAttributes(); // Aplica los cambios
                }

                Debug.Log("Hormiga comprada.");
            }
            else if (!baseUpgrade.CanAddAnt())
            {
                Debug.Log("No hay capacidad para más hormigas.");
            }
            else
            {
                Debug.Log("No tienes suficientes monedas.");
            }
        }

    }
    public void BuyWarriorAnt()
    {
        if (resourceManager != null && baseUpgrade != null)
        {
            if (resourceManager.coins >= antCost && baseUpgrade.CanAddAnt())
            {
                resourceManager.RemoveCoins(antCost);
                baseUpgrade.AddAnt();

                GameObject newAnt = Instantiate(antPrefab, spawnPoint.position, Quaternion.identity);

                // Ajustar los niveles de la nueva hormiga
                AntMovement antMovement = newAnt.GetComponent<AntMovement>();
                if (antMovement != null)
                {
                    // Sincroniza con los niveles globales
                    UpgradeAnts upgradeAnts = FindObjectOfType<UpgradeAnts>();
                    antMovement.speedLevel = upgradeAnts.globalSpeedLevel;
                    antMovement.healthLevel = upgradeAnts.globalHealthLevel;
                    antMovement.strengthLevel = upgradeAnts.globalStrengthLevel;
                    antMovement.UpdateAttributes(); // Aplica los cambios
                }

                Debug.Log("Hormiga comprada.");
            }
            else if (!baseUpgrade.CanAddAnt())
            {
                Debug.Log("No hay capacidad para más hormigas.");
            }
            else
            {
                Debug.Log("No tienes suficientes monedas.");
            }
        }
    }
    public void BuyQueenAnt()
    {
        if (resourceManager != null && baseUpgrade != null)
        {
            if (resourceManager.coins >= antQueenCost&& resourceManager.sheets >= antQueenCostLeaf && antQueen !=1)
            {
                resourceManager.RemoveCoins(antQueenCost);
                resourceManager.RemoveSheets(antQueenCostLeaf);

                antQueen = 1;
                StartCoroutine(GenerateAntsOverTime());
                StartCoroutine(GenerateResourcesOverTime());
                Debug.Log("Hormiga Queen comprada.");
            }
            else
            {
                Debug.Log("No tienes suficientes monedas.");
            }
        }
    }
    public void UpgradeTimeGenerateAnts() 
    {
        timeAnt--;
        resourceManager.RemoveCoins(20);
        resourceManager.RemoveSheets(200);
    }
    public void UpgradeTimeGenerateResources() 
    {
        timeResoruces--;
        resourceManager.RemoveCoins(10);
        resourceManager.RemoveSheets(100);
    }
    private IEnumerator GenerateAntsOverTime()
    {
        while (true) // Bucle infinito
        {
            yield return new WaitForSeconds(timeAnt);
            baseUpgrade.AddAnt();

            GameObject newAnt = Instantiate(antPrefab, spawnPoint.position, Quaternion.identity);

            // Ajustar los niveles de la nueva hormiga
            AntMovement antMovement = newAnt.GetComponent<AntMovement>();
            if (antMovement != null)
            {
                // Sincroniza con los niveles globales
                UpgradeAnts upgradeAnts = FindObjectOfType<UpgradeAnts>();
                antMovement.speedLevel = upgradeAnts.globalSpeedLevel;
                antMovement.healthLevel = upgradeAnts.globalHealthLevel;
                antMovement.strengthLevel = upgradeAnts.globalStrengthLevel;
                antMovement.UpdateAttributes(); // Aplica los cambios
            }
            Debug.Log("Generada una nueva hormiga.");
        }
    }

    private IEnumerator GenerateResourcesOverTime()
    {
        while (true) // Bucle infinito
        {
            yield return new WaitForSeconds(timeResoruces);
            resourceManager.AddCoins(1);
            Debug.Log("Generados nuevos recursos.");
        }
    }
}
