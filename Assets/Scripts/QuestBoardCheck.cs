using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoardCheck : MonoBehaviour
{
    public bool onBoard;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            Debug.Log("EnterBoard");
            onBoard = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            Debug.Log("ExitBoard");
            onBoard = false;
        }
    }
}
