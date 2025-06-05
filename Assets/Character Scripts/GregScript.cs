using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GregScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] GameObject idleImage;
    [SerializeField] GameObject arrowImage;

    [SerializeField] GoldSystem gs;

    [SerializeField] MeshRenderer mr;

    [Header("Emotions")]
    [SerializeField] Material gregIdle;
    [SerializeField] Material gregHappy;
    [SerializeField] Material gregArrow;

    public bool partOneComplete;

    //bool D1 = true;
    //bool D2 = false;


    private void Start()
    {
        mr.material = gregIdle;
        gs = GoldSystem.instance;
    }

    public void StartDialogue()
    {
        if(!partOneComplete)
        {
            cs.gregD1P1.StartNewDialogue(cs.dialogueTriggerScript);
            partOneComplete = true;
        }
        else
        {
            int rep = 0;
            if (cs.currentCharacter.choseQuestA)
            {
                cs.gregD2Q1A.StartNewDialogue(cs.dialogueTriggerScript);
                rep = -1;
            }
            else
            {
                cs.gregD2Q1B.StartNewDialogue(cs.dialogueTriggerScript);
            }

            gameObject.GetComponent<CharacterReputation>().ModifyReputation(rep);
        }
    }

    public void CheckForReward()
    {
        int rep = 0;
        if (cs.currentCharacter.choseQuestA) //Slay Quest
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
                rep = -1;
            }

            else if (gs.goldAmount < 8)
            {
                //do this
                cs.gregD2G1AP1.StartNewDialogue(cs.dialogueTriggerScript);
                rep = -1;
            }
            
        }
        else if (cs.currentCharacter.choseQuestB) //Mushroom Quest
        {
            if (gs.goldAmount == 2)
            {
                //do this
                cs.gregD2G2BP1.StartNewDialogue(cs.dialogueTriggerScript);
                rep = 1;
            }
            else if (gs.goldAmount > 2)
            {
                //do this
                cs.gregD2G2CP1.StartNewDialogue(cs.dialogueTriggerScript);
                rep = 2;
            }

            else if (gs.goldAmount < 2)
            {
                //do this
                cs.gregD2G2AP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }

        gameObject.GetComponent<CharacterReputation>().ModifyReputation(rep);
    }

    public void GregArrow()
    {
        mr.material = gregArrow;
    }

    public void GregIdle()
    {
        mr.material = gregIdle;
    }

    public void GregHappy()
    {
        mr.material = gregHappy;
    }

}
