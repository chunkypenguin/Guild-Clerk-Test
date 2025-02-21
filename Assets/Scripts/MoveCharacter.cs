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

    bool josieHasMoved;

    //Character Dialogue Nodes
    [SerializeField] CharacterSystem cs;

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
                cs.josieD1P1.StartNewDialogue(cs.dialogueTriggerScript);
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
            if (!josieHasMoved)
            {
                cs.josieD1P3.GetComponent<DialogueManager>().StartNewDialogue(cs.dialogueTriggerScript);
                josieHasMoved = true;
            }
            else
            {
                if (cs.pickedQ1A)
                {
                    cs.josieD1Q1AP2.GetComponent<DialogueManager>().StartNewDialogue(cs.dialogueTriggerScript);
                }
                else if (cs.pickedQ1B)
                {
                    cs.josieD1Q1BP2.GetComponent<DialogueManager>().StartNewDialogue(cs.dialogueTriggerScript);
                }

            }

        });
    }
}
