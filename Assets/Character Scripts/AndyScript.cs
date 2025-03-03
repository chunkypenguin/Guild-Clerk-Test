using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndyScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    [SerializeField] GameObject AndyImage;
    [SerializeField] GameObject AndyMomImage;

    public void StartDialogue()
    {
        

        if (cs.D1 && !cs.D2)
        {
            cs.andyD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (!cs.D1 && cs.D2)
        {
            if (cs.currentCharacter.choseQuestA)
            {
                cs.andyD2Q1A.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else
            {
                cs.andyD2Q1B.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
    }

    public void ChangeToMom()
    {

        if (cs.currentCharacter.choseQuestA)
        {
            AndyImage.SetActive(false);
            AndyMomImage.SetActive(true);
        }
        else
        {

        }

    }

    public void CheckForReward()
    {
        cs.andyD2Q1BP2.StartNewDialogue(cs.dialogueTriggerScript);
    }
}
