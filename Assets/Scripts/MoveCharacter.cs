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
    [SerializeField] ZetoScript zetoS;

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
            endPos = startPos + new Vector3(-5, -6, 1);
        }


        transform.position = endPos;

        //endPos.transform.position = startPos.transform.position + new Vector3(10.5f, 0, 0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            if (cs.currentCharacter.characterName == "Nomira")
            {
                //NomiraHit();
                //ZetoJumpAcross();
                NomiraSlowEnter();
            }
        }
    }


    public void MoveToEnd() //move character out of sight
    {
        if (cs.currentCharacter.characterName == "Andy" || cs.currentCharacter.characterName == "Zeto Storma")
        {
            transform.DOJump(endPos, jumpSpeed * 2, jumpCount, jumpDuration).OnComplete(() =>
            {
                if (cs.currentCharacter.characterName == "Zeto Storma")
                {
                    zetoS.ZetoDefault(); //return back to default
                }

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

                if(cs.currentCharacter.characterName == "Nomira")
                {
                    if (!nomiraS.partOneComplete)
                    {
                        nomiraS.partOneComplete = true;
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

        if (cs.currentCharacter.characterName == "Andy" || cs.currentCharacter.characterName == "Zeto Storma")
        {


            transform.DOJump(startPos, jumpSpeed, jumpCount, jumpDuration).OnComplete(() =>
            {
                if (cs.currentCharacter.characterName == "Andy")
                {
                    andyS.StartDialogue();
                }
                else if (cs.currentCharacter.characterName == "Zeto Storma")
                {
                    zetoS.StartDialogue();
                }

            });

        }
        else if(cs.currentCharacter.characterName == "Nomira")
        {
            if(!nomiraS.partOneComplete)
            {
                NomiraSlowEnter();
            }
            else 
            {
                if(cs.currentCharacter.choseQuestA)
                {
                    if(cs.currentCharacter.choseItemB || cs.currentCharacter.choseItemAA)
                    {
                        //divine and arcane focus
                        //peak
                        NomiraSlowEnterOtherArcaneFocus();
                    }
                    else if(cs.currentCharacter.choseItemA) //weapon
                    {
                        transform.DOMove(startPos, moveSpeed).OnComplete(() =>
                        {
                            cs.nomiraP2QAWeapon.StartNewDialogue(cs.dialogueTriggerScript);
                        });
                    }
                }
                else if (cs.currentCharacter.choseQuestB)
                {
                    if (cs.currentCharacter.choseItemB) //divine focus
                    {
                        //peak
                        NomiraSlowEnterDivineFocus();
                    }
                    else if(cs.currentCharacter.choseItemAA) // other arcane focus
                    {
                        transform.DOMove(startPos, moveSpeed).OnComplete(() =>
                        {
                            cs.nomiraP2QBOtherFocus.StartNewDialogue(cs.dialogueTriggerScript);
                        });
                    }
                }
            }

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
                    //nomiraS.StartDialogue();
                    Debug.Log("start nomira dialogue");
                }

            });
        }
    }

    //ZETO AND NOMIRA STUFF
    public void ZetoJumpUp()
    {
        transform.DOJump(startPos, jumpSpeed, jumpCount, jumpDuration).OnComplete(() =>
        {
            cs.zetonomiraD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        });
    }

    public void ZetoJumpDown()
    {
        transform.DOJump(endPos, jumpSpeed * 2, jumpCount, jumpDuration).OnComplete(() =>
        {
            //resume nomari dialogue
            cs.nomiraD1Q1AB.StartNewDialogue(cs.dialogueTriggerScript);

            if (cs.zetoCharacter.choseQuestA)
            {
                zetoS.ZetoCursedDefault();
            }
            else if (cs.zetoCharacter.choseQuestB)
            {

                zetoS.ZetoBurnedDefault();
            }
        });
    }

    public void ZetoJumpAcross()
    {
        transform.DOJump(transform.position + new Vector3(5, 0, 0), jumpSpeed, 3, 0.75f).SetEase(Ease.Linear).OnComplete(() =>
        {
            nomiraS.NomiraHit();
        });
    }

    public void NomiraMoveToEndFake()
    {
        transform.DOMove(endPos, moveSpeed).OnComplete(() =>
        {
            cs.nomiraD1Q1AB3.StartNewDialogue(cs.dialogueTriggerScript);
        });
    }
    public void NomiraMoveBackToDesk()
    {
        transform.DOMove(startPos, moveSpeed).OnComplete(() =>
        {
            if (cs.currentCharacter.choseQuestA)
            {
                cs.nomiraD1Q1AP2.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (cs.currentCharacter.choseQuestB)
            {
                cs.nomiraD1Q1BP2.StartNewDialogue(cs.dialogueTriggerScript);
            }
        });
    }

    public void NomiraSlowEnter()
    {
        transform.DOMove(startPos - new Vector3(8, 0, 0), moveSpeed).OnComplete(() =>
        {
            nomiraS.StartDialogue();
        });
    }

    public void NomiraSlowEnterOtherArcaneFocus()
    {
        transform.DOMove(startPos - new Vector3(8, 0, 0), moveSpeed).OnComplete(() =>
        {
            cs.nomiraP2QAArcaneFocus1.StartNewDialogue(cs.dialogueTriggerScript);
        });
    }
    public void NomiraSlowEnterDivineFocus()
    {
        transform.DOMove(startPos - new Vector3(8, 0, 0), moveSpeed).OnComplete(() =>
        {
            cs.nomiraP2QBDivineFocus1.StartNewDialogue(cs.dialogueTriggerScript);
        });
    }
    public void NomiraCompleteEnter()
    {
        transform.DOMove(startPos, moveSpeed).OnComplete(() =>
        {
            cs.nomiraD1P2.StartNewDialogue(cs.dialogueTriggerScript);
        });
    }

    public void NomiraCompleteEnterDivineFocus()
    {
        transform.DOMove(startPos, moveSpeed).OnComplete(() =>
        {
            cs.nomiraP2QBDivineFocus2.StartNewDialogue(cs.dialogueTriggerScript);
        });
    }

    public void NomiraCompleteEnterOtherArcaneFocus()
    {
        transform.DOMove(startPos, moveSpeed).OnComplete(() =>
        {
            cs.nomiraP2QAArcaneFocus2.StartNewDialogue(cs.dialogueTriggerScript);
        });
    }
}
