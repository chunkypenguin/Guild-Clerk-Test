using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TahmasScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    [SerializeField] GoldSystem gs;

    [SerializeField] MeshRenderer mr;

    private void Start()
    {
        gs = GoldSystem.instance;
    }
    public void StartDialogue()
    {
        if (!cs.D2 && cs.D3)
        {
            if (cs.currentCharacter.choseQuestA)
            {
                cs.tahmasD3JoleneQA3.StartNewDialogue(cs.dialogueTriggerScript);
                Debug.Log("tahmas A");
            }
            else
            {
                cs.tahmasD3JoleneB.StartNewDialogue(cs.dialogueTriggerScript);
                Debug.Log("tahmas B");
            }
        }
    }

    public void CheckForReward()
    {
        if (cs.currentCharacter.choseQuestA)
        {
            if (gs.goldAmount == 25)
            {
                //do this
                cs.tahmasD3G1B.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 25)
            {
                //do this
                cs.tahmasD3G1C.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 25)
            {
                //do this
                cs.tahmasD3G1A.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
        else if (cs.currentCharacter.choseQuestB)
        {
            if (gs.goldAmount == 25)
            {
                //do this
                cs.tahmasD3G2B.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 25)
            {
                //do this
                cs.tahmasD3G2C.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 25)
            {
                //do this
                cs.tahmasD3G2A.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }
}
