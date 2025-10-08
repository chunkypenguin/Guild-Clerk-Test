using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorneScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] DialogueCharacter lorneCharacter;

    [SerializeField] GameObject refuseButton;
    bool on;

    [SerializeField] GameObject refuseTag;

    [SerializeField] MeshRenderer mr;
    [SerializeField] Material mat;

    [SerializeField] YarnScript yarnS;
    [SerializeField] GameObject yarn;
    [SerializeField] GameObject yarnTrigger;

    public GameObject purplePackageGlow;
    public GameObject blackPackageGlow;
    public GameObject yarnGlow;

    public GameObject returnPurpPackage;
    public GameObject returnBlackPackage;

    public GameObject purpPackage;
    public GameObject blackPackage;

    [SerializeField] ItemSystem itemS;

    bool tagSystem;
    bool tagOn;

    public bool gaveYarn;

    public bool partOneComplete;
    public bool partTwoComplete;

    public static LorneScript instance;
    private void Awake()
    {
        instance = this;
        mr = gameObject.GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (tagSystem)
        {
            if (!yarnS.yarnOnDesk && !tagOn)
            {
                refuseTag.SetActive(true);
                tagOn = true;
            }
            else if (yarnS.yarnOnDesk && tagOn)
            {
                refuseTag.SetActive(false);
                tagOn = false;
            }
        }
    }

    public void StartDialogue()
    {
        if (!partOneComplete)
        {
            cs.lorneD1P1.StartNewDialogue(cs.dialogueTriggerScript);
            //partOneComplete = true;
        }
        else if (!partTwoComplete)
        {
            cs.lorneD2P1.StartNewDialogue(cs.dialogueTriggerScript);
            //partTwoComplete = true;
        }
        else if (partTwoComplete)
        {
            if (gaveYarn)
            {
                cs.lorneD3Yarn.StartNewDialogue(cs.dialogueTriggerScript);
                //gameObject.GetComponent<CharacterReputation>().ModifyReputation(1);
            }
        }
    }

    public void Day2Lorne()
    {
        //lorneCharacter.ItemAName = "Yarn";
    }

    public void RefuseYarn()
    {
        cs.lorneD2ItemRefuse.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void RefuseButton()
    {
        if (!on)
        {
            refuseButton.SetActive(true);
            on = true;
        }
        else
        {
            refuseButton.SetActive(false);
            on = false;
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
    }

    public void StealYarn()
    {
        yarn.SetActive(false);
    }

    public void ReturnPackage()
    {
        if (purpPackage.activeSelf)
        {
            itemS.ReturnItem(returnBlackPackage);
        }
        else
        {
            itemS.ReturnItem(returnPurpPackage);
        }

    }

    public void ChangeEmote()
    {
        mr.material = mat;
    }
}
