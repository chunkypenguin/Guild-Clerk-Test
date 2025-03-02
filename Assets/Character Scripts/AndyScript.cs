using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndyScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    public void StartDialogue()
    {
        cs.andyD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }
}
