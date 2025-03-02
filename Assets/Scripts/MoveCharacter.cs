using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HeneGames.DialogueSystem;

public class MoveCharacter : MonoBehaviour
{
    //Move 
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
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

    private void Start()
    {
        moveSpeed = 0.25f;
    }

    public void MoveToEnd()
    {
        if (cs.currentCharacter.characterName == "Andy")
        {
            transform.DOJump(startPos.position, jumpSpeed, jumpCount, jumpDuration).OnComplete(() =>
            {
                cs.StartNewCharacter();
            });
        }

        else
        {
            transform.DOMove(startPos.position, moveSpeed).OnComplete(() =>
            {
                cs.StartNewCharacter();
            });
        }

    }

    public void MoveEndDay()
    {
        transform.position = startPos.position;
    }

    public void MoveToStart()
    {
        
        transform.DOMove(startPos.position, moveSpeed).OnComplete(() =>
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
            transform.DOMove(endPos.position, moveSpeed).OnComplete(() =>
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

            });
        }


    }
}
