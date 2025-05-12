using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoleneScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    [SerializeField] GoldSystem gs;

    [SerializeField] MeshRenderer mr;

    public bool joleneDead;

    public bool gaveJoleneMoreGold;

    private void Start()
    {
        gs = GoldSystem.instance;
    }
    public void StartDialogue()
    {
        if (!cs.D1 && cs.D2)
        {
            cs.joleneD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (!cs.D2 && cs.D3)
        {
            if (cs.currentCharacter.choseQuestA)
            {
                cs.joleneD2Q1A.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else
            {
                //activate thamas quest
                Debug.Log("this shouldnt happen");
            }
        }
    }

    public void CheckForReward()
    {
        if (cs.currentCharacter.choseQuestA)
        {
            if (gs.goldAmount == 2)
            {
                //do this
                cs.joleneD2G1B.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 2)
            {
                //do this
                cs.joleneD2G1C.StartNewDialogue(cs.dialogueTriggerScript);

                //TAHMAS ENTERS
                gaveJoleneMoreGold = true;
            }

            else if (gs.goldAmount < 2)
            {
                //do this
                cs.joleneD2G1A.StartNewDialogue(cs.dialogueTriggerScript);


                
            }
        }
        else if (cs.currentCharacter.choseQuestB)
        {
            Debug.Log("this shouldn't happen either");
            if (gs.goldAmount == 16)
            {
                //do this
                //cs.maggieD2G2B.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 16)
            {
                //do this
                //cs.maggieD2G2C.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 16)
            {
                //do this
                //cs.maggieD2G2A.StartNewDialogue(cs.dialogueTriggerScript);
            }


        }
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }
}
