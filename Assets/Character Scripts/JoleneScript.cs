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

    public bool partOneComplete;

    public int joleneGoldGiven;
    public int joleneRequestedGold;
    bool askedForGold;

    public static JoleneScript instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gs = GoldSystem.instance;
    }
    public void StartDialogue()
    {
        if (!partOneComplete)
        {
            cs.joleneD1P1.StartNewDialogue(cs.dialogueTriggerScript);
            partOneComplete = true;
        }
        else
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
        //int rep = 0;
        askedForGold = true;
        joleneGoldGiven = gs.goldAmount;
        if (cs.currentCharacter.choseQuestA)
        {
            joleneRequestedGold = 2;
            if (gs.goldAmount == 2)
            {
                //do this
                cs.joleneD2G1B.StartNewDialogue(cs.dialogueTriggerScript);
                //rep = -1;
            }
            else if (gs.goldAmount > 2)
            {
                //do this
                cs.joleneD2G1C.StartNewDialogue(cs.dialogueTriggerScript);

                //TAHMAS ENTERS
                gaveJoleneMoreGold = true;

                //rep = 1;
            }

            else if (gs.goldAmount < 2)
            {
                //do this
                cs.joleneD2G1A.StartNewDialogue(cs.dialogueTriggerScript);
                //rep = -2;
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
        //gameObject.GetComponent<CharacterReputation>().ModifyReputation(rep);
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }

    public void JoleneGold()
    {
        if (askedForGold)
        {
            Debug.Log("Jolene: " + joleneGoldGiven + ", " + joleneRequestedGold);

            ReviewManager.instance.CharacterGoldAccuracyCalculator(joleneGoldGiven, joleneRequestedGold);

        }
    }
}
