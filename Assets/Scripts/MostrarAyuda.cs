using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarAyuda : MonoBehaviour
{
    public Button toggleButton;
    public Canvas ayuda;
    private int buttonClickCount = 0;
    private void Start()
    {
        ayuda.enabled = false;  
        toggleButton.onClick.AddListener(ToggleCanvas);
    }


    private void ToggleCanvas()
    {
        buttonClickCount++;

        if (buttonClickCount % 2 == 1)
        {
            ayuda.enabled = true;
        }
        else
        {
            ayuda.enabled = false;
        }
    }
   
}
