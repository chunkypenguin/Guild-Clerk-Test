using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomiraScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    [SerializeField] GoldSystem gs;

    [SerializeField] MeshRenderer mr;

    private void Start()
    {
        cs = CharacterSystem.instance;
        gs = GoldSystem.instance;
    }

    public void StartDialogue()
    {
        cs.nomiraD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }
}
