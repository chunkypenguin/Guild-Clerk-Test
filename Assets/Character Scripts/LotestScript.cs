using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotestScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    public void StartDialogue()
    {
        cs.lotestD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }
}
