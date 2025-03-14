using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggieScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    [SerializeField] GoldSystem gs;

    [SerializeField] MeshRenderer mr;

    public void StartDialogue()
    {
        if (!cs.D1 && cs.D2)
        {
            cs.maggieD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (!cs.D2 && cs.D3)
        {
            if (cs.currentCharacter.choseQuestA)
            {
                cs.maggieD2Q1A.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else
            {
                cs.maggieD2Q1B.StartNewDialogue(cs.dialogueTriggerScript);
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
                cs.maggieD2G1B.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 8)
            {
                //do this
                cs.maggieD2G1C.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 8)
            {
                //do this
                cs.maggieD2G1A.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
        else if (cs.currentCharacter.choseQuestB)
        {
            if (gs.goldAmount == 16)
            {
                //do this
                cs.maggieD2G2B.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 16)
            {
                //do this
                cs.maggieD2G2C.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 16)
            {
                //do this
                cs.maggieD2G2A.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }
}
