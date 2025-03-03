using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorneScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] DialogueCharacter lorneCharacter;

    [SerializeField] GameObject refuseButton;
    bool on;
    public void StartDialogue()
    {
        if (cs.D1 && !cs.D2)
        {
            cs.lorneD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (!cs.D1 && cs.D2)
        {
            cs.lorneD2P1.StartNewDialogue(cs.dialogueTriggerScript);
        }
    }

    public void Day2Lorne()
    {
        lorneCharacter.ItemAName = "Yarn";
    }

    public void RefuseYarn()
    {
        cs.lorneD2ItemRefuse.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void RefuseButton()
    {
        if (!on)
        {
            refuseButton.SetActive(true);
            on = true;
        }
        else
        {
            refuseButton.SetActive(false);
            on = false;
        }
       
    }
}
