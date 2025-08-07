using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxMouse : MonoBehaviour
{
    public bool hoveringDiaBox;

    public static DialogueBoxMouse instance;

    private void Awake()
    {
        instance = this;
    }
    public void HoveringDialogueBox()
    {
        hoveringDiaBox = true;
    }

    public void LeavingDialogueBox()
    {
        hoveringDiaBox = false;
    }
}
