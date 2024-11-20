using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public Slider sliderSound;
    public Image muteImage;
    private float sliderVolume;

    void Start()
    {
        sliderVolume = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = sliderVolume;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptionsPanel()
    {
        optionsPanel.SetActive(true);
        optionsPanel.SetActive(true);

        sliderSound.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);

        UpdateMuteIcon();
    }

    public void ChangeSliderValue(float value)
    {
        sliderVolume = value;
        PlayerPrefs.SetFloat("volumenAudio", sliderVolume);
        AudioListener.volume = sliderVolume;

        UpdateMuteIcon();
    }

    private void UpdateMuteIcon()
    {
        muteImage.enabled = sliderVolume == 0;
    }
}
