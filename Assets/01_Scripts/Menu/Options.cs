using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class Options : MonoBehaviour
{
    public OptionController optionController;
    // Start is called before the first frame update
    void Start()
    {
        optionController = GameObject.FindGameObjectWithTag("options").GetComponent<OptionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ViewOptions();        
        }
    }
    public void ViewOptions()
    {
        optionController.pantallaOpciones.SetActive(true);
    }
}
