using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TutorialScript : MonoBehaviour
{
    public bool questDialogue1, questDialogue2; //was serializefield before
    public bool tutP1, tutP2;
    public bool questsOnBoard;
    public bool holdingQuest;
    bool josieStarted;
    bool josieHasMoved;
    bool dayOneEnd;
    bool poofed;

    [SerializeField] MeshRenderer mr;

    [Header("Emotions")]
    //[SerializeField] Material josieIdle;
    //[SerializeField] Material josieFun;
    //[SerializeField] Material josieSassy;
    public Material josieDisguise;
    public Material josieRegular;
    public Material josieSad;
    public Material josieShocked;

    [SerializeField] QuestBoardCheck questA;
    [SerializeField] QuestBoardCheck questB;

    [SerializeField] CharacterSystem cs;
    [SerializeField] movecam mc;
    [SerializeField] GoldSystem gs;

    bool flashBell;

    [SerializeField] KarinJosieGoldBundle kJGoldBundle;
    [SerializeField] GameObject refuseTag;
    public GameObject goldBundleGlow;
    public GameObject goldBundle;
    bool tagSystem;
    bool tagOn;
    public bool hasGoldBundle;

    //Lotest check
    public bool lotestJosieStarted;
    public bool josieDayOneComplete;

    //achilles check
    public bool achillesDialogueFinished;

    //review stuff


    //Fade tutorial diabox text
    [SerializeField] TMP_Text tutSpaceClickText;

    public static TutorialScript instance;

    public void Start()
    {
        gs = GoldSystem.instance;
        instance = this;
    }
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

    public void StartDialogue()
    {
        if (LotestScript.instance.partOneComplete)
        {
            Debug.Log("josie talks about magenta bag");
            cs.josieD3Lotest.StartNewDialogue(cs.dialogueTriggerScript);
        }
    }

    public void AchillesDialogue()
    {
        if (AchillesScript.instance.achillesCharacter.choseQuestA)
        {
            cs.josieAchillesQ1A.StartNewDialogue(cs.dialogueTriggerScript);

        }
        else if (AchillesScript.instance.achillesCharacter.choseQuestB)
        {
            cs.josieAchillesQ1B.StartNewDialogue(cs.dialogueTriggerScript);

        }
        achillesDialogueFinished = true;
    }

    private void Update()
    {
        //tag system for kalin
        if (tagSystem)
        {
            if (!kJGoldBundle.goldBundleOnDesk && !tagOn)
            {
                refuseTag.SetActive(true);
                tagOn = true;
            }
            else if (kJGoldBundle.goldBundleOnDesk && tagOn)
            {
                refuseTag.SetActive(false);
                tagOn = false;
            }
        }

        if (questA.onBoard && questB.onBoard)
        {
            questsOnBoard = true;
        }
        else if(poofed)
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
                flashBell = true;
                //Debug.Log("flash off");
                mc.TurnFlashOff();
                //mc.flashOn = false;
            }
        }
        else if(!mc.right && holdingQuest && tutP1)
        {
            if (mc.flashOn)
            {
                //Debug.Log("flash off");
                mc.TurnFlashOff();
                //mc.flashOn = false;
            }
        }
        else if(mc.center && !holdingQuest && tutP1 && !questsOnBoard && flashBell)
        {
            if (!mc.flashOn)
            {
                mc.dontFlash = false;
                mc.ButtonFlashUp(mc.bellButton);
                //flashBell = false;
                //Debug.Log("bell flash");
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
                //mc.ButtonFlashUp(mc.topButton);
                mc.ButtonFlashUp(mc.bellButton);
            }

        }
        else if(gs.addedGold && !mc.bottom && tutP2)
        {
            if (mc.flashOn)
            {
                Debug.Log("turn off flash");
                //mc.TurnFlashOff();
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
            mc.TurnFlashOff();
        }
    }

    public void TutorialMoveDesk()
    {
        if (!josieStarted)
        {
            cs.josieD1P1.StartNewDialogue(cs.dialogueTriggerScript);
            josieStarted = true;
        }
        else if (!josieHasMoved && josieStarted)
        {
            cs.josieD1P3.StartNewDialogue(cs.dialogueTriggerScript);
            josieHasMoved = true;
        }
        else
        {
            if (!dayOneEnd)
            {
                if (cs.pickedQ1A)
                {
                    cs.josieD1Q1AP2.StartNewDialogue(cs.dialogueTriggerScript);
                }
                else if (cs.pickedQ1B)
                {
                    cs.josieD1Q1BP2.StartNewDialogue(cs.dialogueTriggerScript);
                }
                dayOneEnd = true;
            }
            else
            {
                cs.josieD1P5.StartNewDialogue(cs.dialogueTriggerScript);
                josieDayOneComplete = true;
            }
        }
    }

    public void CheckForReward()
    {
        if (cs.pickedQ1A)
        {
            if (gs.goldAmount == 7)
            {
                //Do this
                cs.josieD1G1BP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 7)
            {
                //do this
                cs.josieD1G1AP1.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 7)
            {
                //do this
                cs.josieD1G1CP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
        else if (cs.pickedQ1B)
        {
            if (gs.goldAmount == 4)
            {
                //Do this
                cs.josieD1G1BP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 4)
            {
                //do this
                cs.josieD1G1AP1.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 4)
            {
                //do this
                cs.josieD1G1CP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
    }

    public void TagSystemOn()
    {
        tagSystem = true;
    }

    public void TagSystemOff()
    {
        tagSystem = false;
        refuseTag.SetActive(false);
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }

    public void GoldCheck()
    {
        if(goldBundle.activeSelf == true)
        {
            hasGoldBundle = true;
            Debug.Log("goldhas");
        }
    }

    //1
    public void TutorialQuestPoof()
    {
        if (!poofed)
        {
            Invoke(nameof(QuestPoof), 1f);
            poofed = true;
            questsOnBoard = true;

        }

    }
    //2
    private void QuestPoof()
    {
        QuestSystem.instance.UpdateQuests();
        Invoke(nameof(QuestPoofDia), 0.5f);
    }
    //3
    private void QuestPoofDia()
    {
        cs.josieD1P4.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void ClickSpaceTextRemoval()
    {
        //Color c = tutSpaceClickText.color;
        //c.a = 1f;
        //tutSpaceClickText.color = c;
        //tutSpaceClickText.DOFade(0, 1.5f).SetEase(Ease.InOutQuad);

        tutSpaceClickText.gameObject.SetActive(false);
    }
}
