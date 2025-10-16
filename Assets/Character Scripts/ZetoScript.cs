using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ZetoScript : MonoBehaviour
{
    QuestSystem qs;
    public static ZetoScript instance;

    public Transform zetoTransform;
    public GameObject zetoQuest;
    public CharacterSystem cs;
    public GoldSystem gs;

    [SerializeField] MeshRenderer mr;

    [SerializeField] Material defaultEmote;
    [SerializeField] Material burnedEmote;
    [SerializeField] Material cursedEmote;

    public int zetoGoldGivenSkeleton;
    public int zetoRequestedGoldSkeleton;
    public int zetoGoldGivenNomira;
    public int zetoRequestedGoldNomira;

    public bool partOneComplete;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        zetoTransform = transform;
        qs = QuestSystem.instance;
        cs = CharacterSystem.instance;
        gs = GoldSystem.instance;
        mr = gameObject.GetComponent<MeshRenderer>();
    }

    public void StartDialogue()
    {
        if (!partOneComplete)
        {
            cs.zetoD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        }

        else
        {
            if (cs.currentCharacter.choseQuestA)
            {
                cs.zetoP2QA1.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else
            {
                cs.ZetoP2QB1.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
    }

    public void CheckForReward()
    {
        //int rep = 0;
        zetoGoldGivenSkeleton = gs.goldAmount;
        if (!partOneComplete)
        {
            if (cs.currentCharacter.choseQuestA) //Slay Quest
            {
                zetoGoldGivenSkeleton = gs.goldAmount;
                zetoRequestedGoldSkeleton = 15;
                if (gs.goldAmount == 15) //=
                {
                    //do this
                    cs.zetoD1QAGEquals.StartNewDialogue(cs.dialogueTriggerScript);

                }
                else if (gs.goldAmount > 15) //+
                {
                    //do this
                    cs.zetoD1QAGPlus.StartNewDialogue(cs.dialogueTriggerScript);
                    //rep = -1;
                }

                else if (gs.goldAmount < 15) //-
                {
                    //do this
                    cs.zetoD1QAGMinus.StartNewDialogue(cs.dialogueTriggerScript);
                    //rep = -1;
                }
            }

            partOneComplete = true;
            cs.currentCharacter.choseQuestA = false;
        }
        else
        {
            zetoGoldGivenNomira = gs.goldAmount;
            if (cs.currentCharacter.choseQuestA) //Slay Quest
            {
                zetoRequestedGoldNomira = 40;
                cs.zetoP2QAG.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (cs.currentCharacter.choseQuestB)
            {
                zetoRequestedGoldNomira = 35;
                if (gs.goldAmount == 35) //=
                {
                    //do this
                    cs.ZetoP2QBGEquals.StartNewDialogue(cs.dialogueTriggerScript);

                }
                else if (gs.goldAmount > 35) //+
                {
                    //do this
                    cs.ZetoP2QBGPlus.StartNewDialogue(cs.dialogueTriggerScript);
                    //rep = -1;
                }

                else if (gs.goldAmount < 35) //-
                {
                    //do this
                    cs.ZetoP2QBGMinus.StartNewDialogue(cs.dialogueTriggerScript);
                    //rep = -1;
                }
            }
        }



        //gameObject.GetComponent<CharacterReputation>().ModifyReputation(rep);
    }
    public void ZetoQuestSteal()
    {
        qs.GetQuestRB(zetoQuest);
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }

    public void ZetoDefault()
    {
        mr.material = defaultEmote;
    }

    public void ZetoBurnedDefault()
    {
        mr.material = burnedEmote;
    }

    public void ZetoCursedDefault()
    {
        mr.material = cursedEmote;
    }

    public void ZetoGold()
    {
        ZetoGoldSkeleton();
        ZetoGoldNomira();
    }
    private void ZetoGoldSkeleton()
    {
        ReviewManager.instance.CharacterGoldAccuracyCalculator(zetoGoldGivenSkeleton, zetoRequestedGoldSkeleton);
    }
    private void ZetoGoldNomira()
    {
        ReviewManager.instance.CharacterGoldAccuracyCalculator(zetoGoldGivenNomira, zetoRequestedGoldNomira);
    }

}
