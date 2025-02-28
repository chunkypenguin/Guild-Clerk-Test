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

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space) && !hasMoved)
        //{
        //    hasMoved = true;
        //    transform.DOMove(endPos.position, moveSpeed).OnComplete(() =>
        //    {

        //        //Start Dialogue
        //        cs.josieD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        //        //Debug.Log("Hello");
        //    });
        //}

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    transform.DOJump(jumpPoint.position, jumpSpeed, jumpCount, jumpDuration);
        //}
    }

    public void MoveToEnd()
    {
        transform.DOMove(startPos.position, moveSpeed).OnComplete(() =>
        {
            cs.StartNewCharacter();
        });
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
        //Debug.Log("move to desk");
        transform.DOMove(endPos.position, moveSpeed).OnComplete(() =>
        {
            if (cs.currentCharacter.characterName == "Josie")
            {
                josieS.TutorialMoveDesk();
            }

            if(cs.currentCharacter.characterName == "Greg")
            {
                gregS.StartDialogue();
            }

            if (cs.currentCharacter.characterName == "Finch")
            {
                finchS.StartDialogue();
            }

        });
    }
}
