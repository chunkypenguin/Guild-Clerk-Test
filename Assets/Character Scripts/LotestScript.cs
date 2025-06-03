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

    public bool partOneComplete;
    public bool skipJosie;

    public static LotestScript instance;
    private void Awake()
    {
        instance = this;   
    }
    private void Start()
    {
        gs = GoldSystem.instance;

        //start off expecting to skip josie, changed in cs script later if chooses bean bombs
        skipJosie = true;
    }

    public void StartDialogue()
    {
        if (!partOneComplete)
        {
            cs.lotestD1P1.StartNewDialogue(cs.dialogueTriggerScript);
            partOneComplete = true;
        }
        else
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
        int rep = 0;
        if (cs.currentCharacter.choseQuestA)
        {
            if (gs.goldAmount == 15)
            {
                //do this
                cs.lotestD2G1B.StartNewDialogue(cs.dialogueTriggerScript);
                rep = 1;
            }
            else if (gs.goldAmount > 15)
            {
                //do this
                cs.lotestD2G1C.StartNewDialogue(cs.dialogueTriggerScript);
                rep = -1;
            }

            else if (gs.goldAmount < 15)
            {
                //do this
                cs.lotestD2G1A.StartNewDialogue(cs.dialogueTriggerScript);
                rep = -1;
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
        gameObject.GetComponent<CharacterReputation>().ModifyReputation(rep);
    }
}
