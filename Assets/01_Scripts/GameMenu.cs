using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    public void BacktoHome()
    {
        SceneManager.LoadScene("Menu");
    }
}
