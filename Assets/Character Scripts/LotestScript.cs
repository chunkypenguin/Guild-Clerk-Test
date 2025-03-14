using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotestScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] MeshRenderer mr;

    public GameObject greenGlow;
    public GameObject velvetGlow;

    public void StartDialogue()
    {
        cs.lotestD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }
}
