using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZekeScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] DialogueCharacter zekeCharacter;

    [SerializeField] MeshRenderer mr;

    [SerializeField] GameObject refuseTag;

    [SerializeField] GameObject raspberries;
    [SerializeField] RaspberriesScript raspS;

    public GameObject raspberriesGlow;

    bool tagSystem;
    bool tagOn;

    public bool zekeRejected;

    public bool refusedOnce;

    public static ZekeScript instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (tagSystem)
        {
            if (!raspS.raspberriesOnDesk && !tagOn)
            {
                refuseTag.SetActive(true);
                tagOn = true;
            }
            else if (raspS.raspberriesOnDesk && tagOn)
            {
                refuseTag.SetActive(false);
                tagOn = false;
            }
        }
    }

    public void StartDialogue()
    {
        cs.zekeD3P1.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void RefuseRaspberries()
    {
        if(!refusedOnce)
        {
            cs.zekeD3Refuse1.StartNewDialogue(cs.dialogueTriggerScript);
            refusedOnce = true;
        }
        else
        {
            zekeRejected = true;
            cs.zekeD3Refuse2.StartNewDialogue(cs.dialogueTriggerScript);
            //gameObject.GetComponent<CharacterReputation>().ModifyReputation(-3); //why isn't this working!
        }
    }

    public void TagSystemOn()
    {
        tagSystem = true;
    }

    public void TagSystemOff()
    {
        tagSystem = false;
        refuseTag.SetActive(false);
        tagOn = false;
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }
}
