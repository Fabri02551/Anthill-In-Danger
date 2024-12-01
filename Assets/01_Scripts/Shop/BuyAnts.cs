using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class BuyAnts : MonoBehaviour
{
    public GameObject antPrefab;
    public GameObject antWarriorPrefrab;
    public GameObject antQueenPrefrab;
    public Transform spawnPoint;
    public int antCost = 10;

    private ResourceManager resourceManager;
    private BaseUpgrade baseUpgrade;

    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        baseUpgrade = FindObjectOfType<BaseUpgrade>();

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

}
