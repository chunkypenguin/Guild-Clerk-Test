using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggieScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    public void StartDialogue()
    {
        cs.maggieD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }
}
