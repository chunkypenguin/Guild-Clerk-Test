using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorneScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    public void StartDialogue()
    {
        cs.lorneD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }
}
