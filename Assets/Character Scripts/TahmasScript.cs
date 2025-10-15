using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TahmasScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    [SerializeField] GoldSystem gs;

    [SerializeField] MeshRenderer mr;

    public bool gaveMoreGold;
    public bool gaveEqualOrLessGold;

    public int tahmasGoldGiven;
    public int tahmasRequestedGold;
    bool askedForGold;

    public static TahmasScript instance;

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

    public void CheckForReward()
    {
        askedForGold = true;
        tahmasGoldGiven = gs.goldAmount;
        //int rep = 0;
        if (cs.currentCharacter.choseQuestA)
        {
            tahmasRequestedGold = 25;
            if (gs.goldAmount == 25)
            {
                gaveEqualOrLessGold = true;
                //do this
                cs.tahmasD3G1B.StartNewDialogue(cs.dialogueTriggerScript);
                //rep = -1;
            }
            else if (gs.goldAmount > 25)
            {
                gaveMoreGold = true;
                //do this
                cs.tahmasD3G1C.StartNewDialogue(cs.dialogueTriggerScript);
                //rep = 1;
            }

            else if (gs.goldAmount < 25)
            {
                gaveEqualOrLessGold = true;
                //do this
                cs.tahmasD3G1A.StartNewDialogue(cs.dialogueTriggerScript);
                //rep = -2;
            }
        }
        else if (cs.currentCharacter.choseQuestB)
        {
            tahmasRequestedGold = 25;
            if (gs.goldAmount == 25)
            {
                gaveEqualOrLessGold = true;
                //do this
                cs.tahmasD3G2B.StartNewDialogue(cs.dialogueTriggerScript);

            }
            else if (gs.goldAmount > 25)
            {
                gaveMoreGold = true;
                //do this
                cs.tahmasD3G2C.StartNewDialogue(cs.dialogueTriggerScript);
                //rep = 1;
            }

            else if (gs.goldAmount < 25)
            {
                gaveEqualOrLessGold = true;
                //do this
                cs.tahmasD3G2A.StartNewDialogue(cs.dialogueTriggerScript);
                //rep = -2;
            }
        }
        //gameObject.GetComponent<CharacterReputation>().ModifyReputation(rep);
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }
    public void TahmasGold()
    {
        if(askedForGold)
        {
            ReviewManager.instance.CharacterGoldAccuracyCalculator(tahmasGoldGiven, tahmasRequestedGold);

        }
    }

}
