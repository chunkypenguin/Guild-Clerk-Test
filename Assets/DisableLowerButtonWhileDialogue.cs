using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLowerButtonWhileDialogue : MonoBehaviour
{
    [SerializeField] GameObject lowerButton;     
    [SerializeField] GameObject dialogueWindow;  

    void Update()
    {
        if (!lowerButton) return;

        // activeSelf is TRUE while dialogue text box is showing
        bool dialogueShowing = dialogueWindow.activeSelf;

        if (lowerButton.activeSelf == dialogueShowing)
            lowerButton.SetActive(!dialogueShowing);   // toggle only when needed
    }
}
