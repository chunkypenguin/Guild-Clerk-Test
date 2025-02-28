using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GregScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    public void StartDialogue()
    {
        cs.gregD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }
}
