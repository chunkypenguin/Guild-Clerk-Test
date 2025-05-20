using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.ShaderData;

public class VanelleScript : MonoBehaviour
{
    CharacterSystem cs;
    GoldSystem gs;

    public bool tagSystem;
    bool tagOn;

    [SerializeField] GameObject refuseTag;
    [SerializeField] VanelleQuestAScript qAScript;

    public static VanelleScript instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cs = CharacterSystem.instance;
        gs = GoldSystem.instance;
    }


    private void Update()
    {
        if (tagSystem)
        {
            if (!qAScript.questAOnDesk && !tagOn)
            {
                refuseTag.SetActive(true);
                tagOn = true;
            }
            else if (qAScript.questAOnDesk && tagOn)
            {
                refuseTag.SetActive(false);
                tagOn = false;
            }
        }
    }

    public void StartDialogue()
    {
        cs.vanelleD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void EatQuestBDialogue()
    {
        cs.VanelleD1Q1BP2.StartNewDialogue(cs.dialogueTriggerScript); //eat quest dialogue
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
}
