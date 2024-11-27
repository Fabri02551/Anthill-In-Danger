using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAnts : MonoBehaviour
{
    public void UpgradeAllAntsSpeed()
    {
        // Encuentra todas las hormigas en escena que tengan el script AntMovement
        AntMovement[] ants = FindObjectsOfType<AntMovement>();

        foreach (AntMovement ant in ants)
        {
            ant.LevelUpSpeed(); // Sube el nivel de velocidad de cada hormiga
        }
    }

    public void UpgradeAllAntsHealth()
    {
        // Encuentra todas las hormigas en escena que tengan el script AntMovement
        AntMovement[] ants = FindObjectsOfType<AntMovement>();

        foreach (AntMovement ant in ants)
        {
            ant.LevelUpHealth(); // Sube el nivel de vida de cada hormiga
        }
    }

    public void UpgradeAllAntsStrength()
    {
        // Encuentra todas las hormigas en escena que tengan el script AntMovement
        AntMovement[] ants = FindObjectsOfType<AntMovement>();

        foreach (AntMovement ant in ants)
        {
            ant.LevelUpStrength(); // Sube el nivel de fuerza de cada hormiga
        }
    }
}
