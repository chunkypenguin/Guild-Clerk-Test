using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveCharacter : MonoBehaviour
{
    //Move 
    [SerializeField] Transform endPos;
    [SerializeField] float moveSpeed;

    //Jump
    [SerializeField] Transform jumpPoint;
    [SerializeField] float jumpSpeed;
    [SerializeField] int jumpCount;
    [SerializeField] float jumpDuration;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.DOScale(endPos.position, moveSpeed);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            transform.DOJump(jumpPoint.position, jumpSpeed, jumpCount, jumpDuration);
        }
    }
}
