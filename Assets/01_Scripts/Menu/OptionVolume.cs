using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionVolume : MonoBehaviour
{
    public Slider slider;
    public float volume;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;   
    }
    public void ChanceSlider(float valor)
    {
        volume = valor;
        PlayerPrefs.SetFloat("volimenAudio", slider.value);
        AudioListener.volume = slider.value;
    }
}
