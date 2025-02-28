using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasButtons : MonoBehaviour
{
    [SerializeField] GameObject buttons;
    bool on;
    public void FinchButtons()
    {
        if (!on)
        {
            buttons.SetActive(true);
            on = true;
        }
        else
        {
            buttons.SetActive(false);
            on = false;
        }
        
    }
}
