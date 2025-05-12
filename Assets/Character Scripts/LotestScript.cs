using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotestScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] MeshRenderer mr;
    [SerializeField] GoldSystem gs;

    public DialogueCharacter lotestCharacter;

    public GameObject greenGlow;
    public GameObject velvetGlow;

    private void Start()
    {
        gs = GoldSystem.instance;
    }

    public void StartDialogue()
    {
        if (!cs.D1 && cs.D2)
        {
            cs.lotestD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (!cs.D1 && !cs.D2 && cs.D3)
        {
            cs.lotestD2Q1A.StartNewDialogue(cs.dialogueTriggerScript);
        }
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }

    public void CheckForReward()
    {
        if (cs.currentCharacter.choseQuestA)
        {
            if (gs.goldAmount == 15)
            {
                //do this
                cs.lotestD2G1B.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 15)
            {
                //do this
                cs.lotestD2G1C.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 15)
            {
                //do this
                cs.lotestD2G1A.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
        else if (cs.currentCharacter.choseQuestB)
        {
            if (gs.goldAmount == 2)
            {
                //do this
                //cs.gregD2G2BP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 2)
            {
                //do this
                //cs.gregD2G2CP1.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 2)
            {
                //do this
                //cs.gregD2G2AP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
    }
}
