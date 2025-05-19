using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggieScript : MonoBehaviour
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
        if (!cs.D1 && cs.D2)
        {
            cs.maggieD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (!cs.D2 && cs.D3)
        {
            if (cs.currentCharacter.choseQuestA) //food
            {
                cs.maggieD2Q1A.StartNewDialogue(cs.dialogueTriggerScript);
                gameObject.GetComponent<CharacterReputation>().ModifyReputation(-1);
            }
            else//goblin
            {
                cs.maggieD2Q1B.StartNewDialogue(cs.dialogueTriggerScript);
                gameObject.GetComponent<CharacterReputation>().ModifyReputation(1);
            }
        }
    }

    public void CheckForReward()
    {

        int rep = 0;
        if (cs.currentCharacter.choseQuestA)//food
        {
            if (gs.goldAmount == 8)
            {
                //do this
                cs.maggieD2G1B.StartNewDialogue(cs.dialogueTriggerScript);
                rep = 1;
            }
            else if (gs.goldAmount > 8)
            {
                //do this
                cs.maggieD2G1C.StartNewDialogue(cs.dialogueTriggerScript);
                rep = -1;
            }

            else if (gs.goldAmount < 8)
            {
                //do this
                cs.maggieD2G1A.StartNewDialogue(cs.dialogueTriggerScript);
                rep = 0;
            }
        }
        else if (cs.currentCharacter.choseQuestB)//goblin
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
                rep = 1;
            }

            else if (gs.goldAmount < 16)
            {
                //do this
                cs.maggieD2G2A.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }

        gameObject.GetComponent<CharacterReputation>().ModifyReputation(rep);
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }
}
