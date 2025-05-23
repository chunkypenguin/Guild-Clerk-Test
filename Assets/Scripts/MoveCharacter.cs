using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HeneGames.DialogueSystem;

public class MoveCharacter : MonoBehaviour
{
    //Move 
    //[SerializeField] GameObject startPos;
    //[SerializeField] GameObject endPos;
    [SerializeField] float moveSpeed;

    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;

    //Jump
    [SerializeField] bool jumping;
    [SerializeField] Transform jumpPoint;
    [SerializeField] float jumpSpeed;
    [SerializeField] int jumpCount;
    [SerializeField] float jumpDuration;

    bool hasMoved;

    //Character Dialogue Nodes
    [SerializeField] CharacterSystem cs;

    [SerializeField] TutorialScript josieS;
    [SerializeField] GregScript gregS;
    [SerializeField] FinchScript finchS;
    [SerializeField] AndyScript andyS;
    [SerializeField] LorneScript lorneS;
    [SerializeField] MaggieScript maggieS;
    [SerializeField] JoleneScript joleneS;
    [SerializeField] LotestScript lotestS;
    [SerializeField] TahmasScript tahmasS;
    [SerializeField] ZekeScript zekeS;
    [SerializeField] VanelleScript vanelleS;
    [SerializeField] NomiraScript nomiraS;

    [SerializeField] DaySystem ds;

    private void Awake()
    {
        moveSpeed = 1.5f;
        if (!jumping)
        {
            startPos = transform.position;
            endPos = startPos - new Vector3(12f, 0, 0);
        }
        else
        {
            startPos = transform.position;
            endPos = startPos + new Vector3(0, -5, 1);
        }


        transform.position = endPos;

        //endPos.transform.position = startPos.transform.position + new Vector3(10.5f, 0, 0);
    }


    public void MoveToEnd() //move character out of sight
    {
        if (cs.currentCharacter.characterName == "Andy")
        {
            transform.DOJump(endPos, jumpSpeed * 2, jumpCount, jumpDuration).OnComplete(() =>
            {

                if (cs.currentCharacter.characterName == "Andy" && cs.D1)
                {
                    andyS.ChangeToMom();
                }

                if (cs.currentCharacter.characterName == "Andy")
                {
                    if (cs.currentCharacter.choseQuestB && cs.D1)
                    {
                        andyS.AndyAngry();
                    }
                }
                cs.StartNewCharacter();
            });
        }

        else
        {
            transform.DOMove(endPos, moveSpeed).OnComplete(() =>
            {
                if (cs.currentCharacter.characterName == "Greg")
                {
                    //if chose quest A...
                    if (cs.currentCharacter.choseQuestA)
                    {
                        gregS.GregArrow();
                    }
                }

                if (cs.currentCharacter.characterName == "Maggie")//Maggie is always before day 3 Jolene
                {
                    if(joleneS.joleneDead && cs.D3) //if day 3 and jolene has died
                    {
                        cs.characterCount++;//skip jolene
                    }
                }
                if (cs.currentCharacter.characterName == "Jolene")
                {
                    if(!joleneS.gaveJoleneMoreGold && cs.D3) //if player did not give jolene more gold, skip tahmas
                    {
                        cs.characterCount++;
                    }
                }

                if (cs.currentCharacter.characterName == "Zeke")
                {
                    if (!lorneS.gaveYarn && cs.D3) //skip lorne if didnt give yarn
                    {
                        cs.characterCount++;
                        lorneS.StealYarn();

                        if (lotestS.lotestCharacter.choseItemB) //skip lotest as well if lotest given valvet pouch of seeds
                        {
                            cs.characterCount++; //skip to josie
                        }
                    }

                }

                if (cs.currentCharacter.characterName == "Lorne")
                {
                    if (lorneS.gaveYarn && cs.D3)
                    {
                        if (lotestS.lotestCharacter.choseItemB)//skip lotest, else stay with lotest
                        {
                            cs.characterCount++;
                        }
                    }
                }

                if (cs.currentCharacter.characterName == "Lotest")
                {
                    if (cs.D3)
                    {
                        cs.characterCount++; //skip josie
                        if (!andyS.andyCharacter.choseQuestA)
                        {
                            cs.characterCount++; //skip Andy
                            //END GAME
                            ds.EndDay();
                        }
                    }
                }

                if (cs.currentCharacter.characterName == "Josie")
                {
                    if (cs.D1)
                    {
                        josieS.ChangeEmote(josieS.josieRegular);
                    }
                }

                    cs.StartNewCharacter();
            });
        }

    }

    public void MoveEndDay()
    {
        transform.position = endPos;

        if(cs.currentCharacter.characterName == "Andy")
        {
            andyS.ChangeToAndy();


            andyS.AndyInjuiredIdle();

        }
    }

    public void MoveToStart()
    {
        
        transform.DOMove(endPos, moveSpeed).OnComplete(() =>
        {
            MoveToDesk();
            if (cs.D1)
            {
                josieS.ChangeEmote(josieS.josieDisguise);
            }
        });
        
    }

    public void MoveToDesk()
    {

        if (cs.currentCharacter.characterName == "Andy")
        {
            transform.DOJump(startPos, jumpSpeed, jumpCount, jumpDuration).OnComplete(() =>
            {
                andyS.StartDialogue();
            });

        }

        else
        {
            transform.DOMove(startPos, moveSpeed).OnComplete(() =>
            {
                if (cs.currentCharacter.characterName == "Josie")
                {
                    if (cs.D1 && !cs.D2 && !cs.D3)
                    {
                        josieS.TutorialMoveDesk();
                        
                    }
                    else if (cs.D3)
                    {
                        josieS.StartDialogue();
                    }

                }

                if (cs.currentCharacter.characterName == "Greg")
                {
                    gregS.StartDialogue();
                }

                if (cs.currentCharacter.characterName == "Finch")
                {
                    finchS.StartDialogue();
                }

                if (cs.currentCharacter.characterName == "Lorne")
                {
                    lorneS.StartDialogue();
                }

                if (cs.currentCharacter.characterName == "Maggie")
                {
                    maggieS.StartDialogue();
                }

                if (cs.currentCharacter.characterName == "Jolene")
                {
                    joleneS.StartDialogue();
                }

                if (cs.currentCharacter.characterName == "Lotest")
                {
                    lotestS.StartDialogue();
                }

                if(cs.currentCharacter.characterName == "Tahmas")
                {
                    tahmasS.StartDialogue();
                    Debug.Log("Start tahmas dialogue");
                }

                if(cs.currentCharacter.characterName == "Zeke")
                {
                    zekeS.StartDialogue();
                    Debug.Log("start zeke dialogue");
                }

                if (cs.currentCharacter.characterName == "Vanelle")
                {
                    vanelleS.StartDialogue();
                    Debug.Log("start vanelle dialogue");
                }

                if (cs.currentCharacter.characterName == "Nomira")
                {
                    nomiraS.StartDialogue();
                    Debug.Log("start nomira dialogue");
                }

            });
        }
    }

    public void ZetoJumpUp()
    {
        transform.DOJump(startPos, jumpSpeed, jumpCount, jumpDuration).OnComplete(() =>
        {
            cs.zetoD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        });
    }

    public void ZetoJumpDown()
    {
        transform.DOJump(endPos, jumpSpeed * 2, jumpCount, jumpDuration).OnComplete(() =>
        {
            //resume nomari dialogue
        });
    }
}
