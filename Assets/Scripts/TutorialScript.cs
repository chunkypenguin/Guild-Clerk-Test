using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] bool questDialogue1, questDialogue2;
    public bool tutP1, tutP2;
    public bool questsOnBoard;
    public bool holdingQuest;

    [SerializeField] QuestBoardCheck questA;
    [SerializeField] QuestBoardCheck questB;

    [SerializeField] CharacterSystem cs;
    [SerializeField] movecam mc;
    [SerializeField] GoldSystem gs;
    public void JosieTutDialogue(DialogueManager dialogue)
    {
        if(!questDialogue1)
        {
            dialogue.StartNewDialogue(cs.dialogueTriggerScript);
            questDialogue1 = true;
        }
        else if(questDialogue1 && !questDialogue2)
        {

        }

    }

    private void Update()
    {
        if(questA.onBoard && questB.onBoard)
        {
            questsOnBoard = true;
        }
        else
        {
            questsOnBoard = false;
        }

        //QUEST PORTION
        if(questsOnBoard && !mc.right && tutP1)
        {
            //flash right button
            if (!mc.flashOn)
            {
                Debug.Log("quests and tutp1");
                mc.dontFlash = false;
                mc.ButtonFlashUp(mc.rightButton);
                //mc.flashOn = true;
            }
        }
        else if(mc.right && tutP1 && !holdingQuest) //if the player is looking at the quest board and not holding a quest, turn all flashing off
        {
            if(mc.flashOn)
            {
                Debug.Log("flash off");
                mc.TurnFlashOff();
                //mc.flashOn = false;
            }
        }
        else if(!mc.right && holdingQuest && tutP1)
        {
            if (mc.flashOn)
            {
                Debug.Log("flash off");
                mc.TurnFlashOff();
                //mc.flashOn = false;
            }
        }

        //REWARDS PORTION
        if (tutP2 && !mc.bottom && !gs.addedGold)
        {
            if (!mc.flashOn)
            {
                mc.dontFlash = false;
                mc.ButtonFlashUp(mc.bottomButton);
            }
        }
        else if (tutP2 && mc.bottom && !gs.addedGold)
        {
            if (mc.flashOn)
            {
                mc.TurnFlashOff();
            }
        }
        else if (gs.addedGold && mc.bottom && tutP2)
        {
            if (!mc.flashOn)
            {
                mc.dontFlash = false;
                mc.ButtonFlashUp(mc.topButton);
            }

        }
        else if(gs.addedGold && !mc.bottom && tutP2)
        {
            if (mc.flashOn)
            {
                Debug.Log("turn off flash");
                mc.TurnFlashOff();
            }
        }
    }

    public void TutorialP1()
    {
        if (!tutP1 && !tutP2)
        {
            tutP1 = true;
        }
        else if(tutP1 && !tutP2)
        {
            tutP1 = false;
            tutP2 = true;
        }
        else
        {
            tutP2 = false;
        }

    }
}
