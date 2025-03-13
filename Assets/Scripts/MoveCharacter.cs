using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HeneGames.DialogueSystem;

public class MoveCharacter : MonoBehaviour
{
    //Move 
    [SerializeField] GameObject startPos;
    [SerializeField] GameObject endPos;
    [SerializeField] float moveSpeed;

    //Jump
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

    private void Awake()
    {
        moveSpeed = 1.5f;
        startPos.transform.position = transform.position;
        endPos.transform.position = startPos.transform.position + new Vector3(10.5f, 0, 0);
    }


    public void MoveToEnd()
    {
        if (cs.currentCharacter.characterName == "Andy")
        {
            transform.DOJump(startPos.transform.position, jumpSpeed * 2, jumpCount, jumpDuration).OnComplete(() =>
            {

                if (cs.currentCharacter.characterName == "Andy")
                {
                    andyS.ChangeToMom();
                }

                cs.StartNewCharacter();
            });
        }

        else
        {
            transform.DOMove(startPos.transform.position, moveSpeed).OnComplete(() =>
            {
                if (cs.currentCharacter.characterName == "Greg")
                {
                    //if chose quest A...
                    if (cs.currentCharacter.choseItemA)
                    {
                        gregS.GregArrow();
                    }
                    
                }

                cs.StartNewCharacter();
            });
        }

    }

    public void MoveEndDay()
    {
        transform.position = startPos.transform.position;
        cs.D1 = false;
        cs.D2 = true;
    }

    public void MoveToStart()
    {
        
        transform.DOMove(startPos.transform.position, moveSpeed).OnComplete(() =>
        {
            MoveToDesk();
        });
        
    }

    public void MoveToDesk()
    {

        if (cs.currentCharacter.characterName == "Andy")
        {
            transform.DOJump(jumpPoint.position, jumpSpeed, jumpCount, jumpDuration).OnComplete(() =>
            {
                andyS.StartDialogue();
            });


        }

        else
        {
            //Debug.Log("move to desk");
            transform.DOMove(endPos.transform.position, moveSpeed).OnComplete(() =>
            {
                if (cs.currentCharacter.characterName == "Josie")
                {
                    josieS.TutorialMoveDesk();
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

            });
        }


    }
}
