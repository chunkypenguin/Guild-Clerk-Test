using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NomiraScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    [SerializeField] GoldSystem gs;

    [SerializeField] MeshRenderer mr;

    public bool brokeStaff;

    public static NomiraScript instance;

    public bool partOneComplete;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        cs = CharacterSystem.instance;
        gs = GoldSystem.instance;
    }

    public void StartDialogue()
    {
        cs.nomiraD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void CheckForReward()
    {
        //for 5/29
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }

    public void NomiraHit()
    {
        transform.DOPunchPosition(
            punch: new Vector3(1f, 0f, 0f),  // direction and strength of the punch
            duration: 0.5f,                 // total time of the punch effect
            vibrato: 10,                    // how many times it vibrates
            elasticity: 1f                  // how far it goes beyond the punch vector
        );

        cs.NomiraD1QOw.StartNewDialogue(cs.dialogueTriggerScript);
    }
}
