using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLowerButtonWhileDialogue : MonoBehaviour
{
    [SerializeField] GameObject lowerButton;     
    [SerializeField] GameObject dialogueWindow;
    [SerializeField] GameObject inputUI;

    void Update()
    {
        if (!lowerButton) return;

        bool overlayShowing = false;

        if (dialogueWindow && dialogueWindow.activeSelf)
            overlayShowing = true;

        if (inputUI && inputUI.activeSelf)
            overlayShowing = true;

        bool shouldBeActive = !overlayShowing;

        if (lowerButton.activeSelf != shouldBeActive)
            lowerButton.SetActive(shouldBeActive);
    }
}
