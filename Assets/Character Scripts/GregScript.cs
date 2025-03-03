using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GregScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] GameObject idleImage;
    [SerializeField] GameObject arrowImage;

    [SerializeField] GoldSystem gs;

    //bool D1 = true;
    //bool D2 = false;


    public void StartDialogue()
    {
        if(cs.D1 && !cs.D2)
        {
            cs.gregD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if(!cs.D1 && cs.D2)
        {
            if (cs.currentCharacter.choseQuestA)
            {
                cs.gregD2Q1A.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else
            {
                cs.gregD2Q1B.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
    }

    public void CheckForReward()
    {
        if (cs.currentCharacter.choseQuestA)
        {
            if (gs.goldAmount == 8)
            {
                //do this
                cs.gregD2G1BP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 8)
            {
                //do this
                cs.gregD2G1CP1.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 8)
            {
                //do this
                cs.gregD2G1AP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
        else if (cs.currentCharacter.choseQuestB)
        {
            if (gs.goldAmount == 2)
            {
                //do this
                cs.gregD2G2BP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 2)
            {
                //do this
                cs.gregD2G2CP1.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 2)
            {
                //do this
                cs.gregD2G2AP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
    }

    public void ArrowHead()
    {
        if (cs.currentCharacter.choseQuestA)
        {
            idleImage.SetActive(false);
            arrowImage.SetActive(true);
        }
        else
        {

        }

    }
}
