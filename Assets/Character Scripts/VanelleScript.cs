using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.ShaderData;

public class VanelleScript : MonoBehaviour
{
    CharacterSystem cs;
    GoldSystem gs;

    [SerializeField] MeshRenderer mr;

    [Header("Emotions")]
    [SerializeField] Material vanelleIdle;

    public bool questTagSystem;
    bool questTagOn;

    public bool goldTagSystem;
    bool goldTagOn;

    [SerializeField] GameObject refuseTag;
    [SerializeField] VanelleQuestAScript qAScript;

    bool questTaken;

    public static VanelleScript instance;

    bool askingForMorePlus;
    bool askingForMoreMinus;

    private void Awake()
    {
        instance = this;
        mr = gameObject.GetComponent<MeshRenderer>();
        vanelleIdle = gameObject.GetComponent<Material>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mr.material = vanelleIdle;

        cs = CharacterSystem.instance;
        gs = GoldSystem.instance;
    }


    private void Update()
    {
        if(cs.currentCharacter.characterName == "Vanelle")
        {
            if (questTagSystem)
            {
                if (!qAScript.questAOnDesk && !questTagOn)
                {
                    refuseTag.SetActive(true);
                    questTagOn = true;
                }
                else if (qAScript.questAOnDesk && questTagOn)
                {
                    refuseTag.SetActive(false);
                    questTagOn = false;
                }
            }

            if (goldTagSystem)
            {
                if (gs.goldAmount < 1 && !goldTagOn)
                {
                    refuseTag.SetActive(true);
                    goldTagOn = true;
                }
                else if (gs.goldAmount > 0 && goldTagOn)
                {
                    refuseTag.SetActive(false);
                    goldTagOn = false;
                }
            }
        }

    }

    public void GoldTag()
    {
        if (!goldTagSystem)
        {
            goldTagSystem = true;
        }
        else
        {
            goldTagSystem = false;
            refuseTag.SetActive(false);
            goldTagOn = false;
        }
    }
    public void StartDialogue()
    {
        if (!questTaken) //for when first taking quest
        {
            cs.vanelleD1P1.StartNewDialogue(cs.dialogueTriggerScript);
            questTaken = true;
        }
        else //for when returning quest for gold reward
        {
            cs.VanelleD2Q1A.StartNewDialogue(cs.dialogueTriggerScript);
        }

    }

    public void EatQuestBDialogue()
    {
        cs.VanelleD1Q1BP2.StartNewDialogue(cs.dialogueTriggerScript); //eat quest dialogue
    }

    public void CheckForReward()
    {
        int rep = 0;
        if (cs.currentCharacter.choseQuestA && !askingForMoreMinus && !askingForMorePlus) //first time around
        {
            if (gs.goldAmount == 35)
            {
                //do this
                cs.VanelleD2Q1AGB.StartNewDialogue(cs.dialogueTriggerScript);
                rep = 1;
            }
            else if (gs.goldAmount > 35)
            {
                //do this
                cs.VanelleD2Q1AGA.StartNewDialogue(cs.dialogueTriggerScript);
                rep = 1;
                askingForMorePlus = true;
            }

            else if (gs.goldAmount < 35)
            {
                //do this
                cs.VanelleD2Q1AGC.StartNewDialogue(cs.dialogueTriggerScript);

                askingForMoreMinus = true;
            }
        }
        else if(cs.currentCharacter.choseQuestA && askingForMorePlus)//if you give more gold
        {
            if (gs.goldAmount < 1) //refuse
            {
                //do this
                cs.vanelleD2Q1AGPlusRefuse.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 0) //give more
            {
                cs.vanelleD2Q1AGPlusGive.StartNewDialogue(cs.dialogueTriggerScript);
                rep = 1;
            }
        }
        else if(cs.currentCharacter.choseQuestA && askingForMoreMinus)//if you give less gold
        {
            if (gs.goldAmount < 1) //refuse
            {
                //do this
                cs.vanelleD2Q1AGMinusRefuse.StartNewDialogue(cs.dialogueTriggerScript);
                rep = -2;
            }
            else if (gs.goldAmount > 0) //give more
            {
                cs.vanelleD2Q1AGMinusGive.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }

        gameObject.GetComponent<CharacterReputation>().ModifyReputation(rep);
    }

    public void TagSystemOn()
    {
        questTagSystem = true;
    }

    public void TagSystemOff()
    {
        questTagSystem = false;
        refuseTag.SetActive(false);
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }
}
