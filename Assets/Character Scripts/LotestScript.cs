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
    bool lotestWillSayName;
    bool lotestWillSayAhem;

    public bool gaveLessGold;
    public bool gaveEqualOrMoreGold;

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

    private void Update()
    {
        LotestObnoxiousAhem();
        LotestObnoxiousName();
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
        //int rep = 0;
        if (cs.currentCharacter.choseQuestA)
        {
            if (gs.goldAmount == 15)
            {
                //do this
                cs.lotestD2G1B.StartNewDialogue(cs.dialogueTriggerScript);
                gaveEqualOrMoreGold = true;
                //rep = 1;
            }
            else if (gs.goldAmount > 15)
            {
                //do this
                cs.lotestD2G1C.StartNewDialogue(cs.dialogueTriggerScript);
                gaveEqualOrMoreGold = true;
                //rep = -1;
            }

            else if (gs.goldAmount < 15)
            {
                //do this
                cs.lotestD2G1A.StartNewDialogue(cs.dialogueTriggerScript);
                gaveLessGold = true;
                //rep = -1;
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
        //gameObject.GetComponent<CharacterReputation>().ModifyReputation(rep);
    }

    //activates after first part of Lotest dialogue
    public void LotestWillSayAhem()
    {
        lotestWillSayAhem = true;
    }

    void LotestObnoxiousAhem()
    {
        //when player looks left after the first part of lotest dialogue
        if(lotestWillSayAhem && movecam.instance.left && movecam.instance.canMoveCam)
        {
            cs.IsIdle();

            LotestAhem(); //say ahem dialogue

            //Move Camera Back to Center
            //Invoke(nameof(MoveCamBackToCenter), 1f);
            lotestWillSayAhem = false; //ensures this runs just once

            lotestWillSayName = true; //check now for "LotestObnoxiousName"
        }
    }
    void LotestObnoxiousName()
    {
        //if lotest has said ahem and player looks center
        if (lotestWillSayName && movecam.instance.center && movecam.instance.canMoveCam)
        {
            LotestNameDrop(); 
        }
    }
    
    // is called if ahem dialogue is completed/finished before player turns camera center
    public void MoveCamBackToCenter()
    {
        if (lotestWillSayName)
        {
            if (movecam.instance.left)
            {
                movecam.instance.RightButton();
            }
            Invoke(nameof(LotestNameDrop), 0.5f); //timed to be when camera pans back to center
        }

    }

    void LotestAhem()
    {
        cs.lotestAhem.StartNewDialogue(cs.dialogueTriggerScript);
    }
    void LotestNameDrop()
    {
        lotestWillSayName = false;
        cs.lotestD1P2.StartNewDialogue(cs.dialogueTriggerScript);
    }
}
