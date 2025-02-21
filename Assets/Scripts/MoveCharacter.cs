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
    [SerializeField] DialogueTrigger dialogueTriggerScript;
    [SerializeField] DialogueManager dialogueManagerScript;

    bool hasMoved;

    //Character Dialogue Nodes
    [SerializeField] GameObject Josie3;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !hasMoved)
        {
            hasMoved = true;
            transform.DOMove(endPos.position, moveSpeed).OnComplete(() =>
            {

                //Start Dialogue
                dialogueManagerScript.StartNewDialogue(dialogueTriggerScript);
                //Debug.Log("Hello");
            });
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            transform.DOJump(jumpPoint.position, jumpSpeed, jumpCount, jumpDuration);
        }
    }

    public void MoveToStart()
    {
        
        transform.DOMove(startPos.position, moveSpeed * 0.25f).OnComplete(() =>
        {
            MoveToDesk();
        });
        
    }

    public void MoveToDesk()
    {
        Debug.Log("move to desk");
        transform.DOMove(endPos.position, moveSpeed * 0.25f).OnComplete(() =>
        {
            Josie3.GetComponent<DialogueManager>().StartNewDialogue(dialogueTriggerScript);
        });
    }
}
