using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoleneScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    public void StartDialogue()
    {
        cs.joleneD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }
}
