using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : MonoBehaviour
{
    //public DialogueCharacter[] characters; //for example, Josie character
    public List<DialogueCharacter> characters;
    public DialogueCharacter currentCharacter;

    //public GameObject[] characterObjects;
    public List<GameObject> characterObjects;
    public GameObject currentCharacterObject;

    public DialogueTrigger dialogueTriggerScript;

    public JoleneScript joleneS;
    public LorneScript lorneS;

    public int characterCount;

    //JOSIE
    [Header("Josie")]
    public DialogueManager josieD1P1;
    public DialogueManager josieD1P2;
    public DialogueManager josieD1P3;
    public DialogueManager josieD1P4;
    public DialogueManager josieD1P5;
    public DialogueManager josieD1Q1AP1;
    public DialogueManager josieD1Q1AP2;
    public DialogueManager josieD1Q1BP1;
    public DialogueManager josieD1Q1BP2;
    public DialogueManager josieD1G1AP1;
    public DialogueManager josieD1G1BP1;
    public DialogueManager josieD1G1CP1;
    public DialogueManager josieD3Lotest;

    [Header("Greg")]
    public DialogueManager gregD1P1;
    public DialogueManager gregD1Q1AP1;
    public DialogueManager gregD1Q1BP1;

    public DialogueManager gregD2Q1A;
    public DialogueManager gregD2Q1B;
    //picked A
    public DialogueManager gregD2G1AP1;
    public DialogueManager gregD2G1BP1;
    public DialogueManager gregD2G1CP1;
    //picked B
    public DialogueManager gregD2G2AP1;
    public DialogueManager gregD2G2BP1;
    public DialogueManager gregD2G2CP1;


    [Header("Finch")]
    public DialogueManager finchD1P1;
    public DialogueManager finchD1G1AP1;
    public DialogueManager finchD1G1BP1;
    public DialogueManager finchD1G1CP1;

    [Header("Finch Gold Request")]
    public DialogueManager finchD1GR1;
    public DialogueManager finchD1GR2;
    public DialogueManager finchD1GR3;
    public DialogueManager finchD1GR4;
    public DialogueManager finchD1GR5;
    public DialogueManager finchD1GR6;
    public DialogueManager finchD1GR7;
    public DialogueManager finchD1GR8;
    public DialogueManager finchD1GR9;
    public DialogueManager finchD1GR10;
    public DialogueManager finchD1GR11;
    public DialogueManager finchD1GR12;

    [Header("Andy")]
    public DialogueManager andyD1P1;
    public DialogueManager andyD1Q1AP1;
    public DialogueManager andyD1Q1BP1;
    public DialogueManager andyD2Q1A;
    public DialogueManager andyD2Q1B;
    public DialogueManager andyD2Q1BP2;
    public DialogueManager andyD2Q1BP3;
    public DialogueManager andyD2Q1AP2;
    public DialogueManager andyD3P1;
    public DialogueManager andyD3G1A;
    public DialogueManager andyD3G1B;

    [Header("Lorne")]
    public DialogueManager lorneD1P1;
    public DialogueManager lorneD1I1P1;
    public DialogueManager lorneD2P1;
    public DialogueManager lorneD2ItemB;
    public DialogueManager lorneD2ItemRefuse;
    public DialogueManager lorneD3Yarn;

    [Header("Maggie")]
    public DialogueManager maggieD1P1;
    public DialogueManager maggieD1Q1AP1;
    public DialogueManager maggieD1Q1BP1;
    public DialogueManager maggieD2Q1A;
    public DialogueManager maggieD2G1A;
    public DialogueManager maggieD2G1B;
    public DialogueManager maggieD2G1C;
    public DialogueManager maggieD2Q1B;
    public DialogueManager maggieD2G2A;
    public DialogueManager maggieD2G2B;
    public DialogueManager maggieD2G2C;


    [Header("Jolene")]
    public DialogueManager joleneD1P1;
    public DialogueManager joleneD1Q1AP1;
    public DialogueManager joleneD1Q1BP1;
    public DialogueManager joleneD2Q1A;
    public DialogueManager joleneD2G1A;
    public DialogueManager joleneD2G1B;
    public DialogueManager joleneD2G1C;

    [Header("Lotest")]
    public DialogueManager lotestD1P1;
    public DialogueManager lotestD1ItemA1;
    public DialogueManager lotestD1ItemB1;
    public DialogueManager lotestD2Q1A;
    public DialogueManager lotestD2G1A;
    public DialogueManager lotestD2G1B;
    public DialogueManager lotestD2G1C;


    [Header("Tahmas")]
    public DialogueManager tahmasD3JoleneQA3;
    public DialogueManager tahmasD3G1A;
    public DialogueManager tahmasD3G1B;
    public DialogueManager tahmasD3G1C;
    public DialogueManager tahmasD3JoleneB;
    public DialogueManager tahmasD3G2A;
    public DialogueManager tahmasD3G2B;
    public DialogueManager tahmasD3G2C;

    [Header("Zeke")]
    public DialogueManager zekeD3P1;
    public DialogueManager zekeD3Feed;
    public DialogueManager zekeD3Refuse1;
    public DialogueManager zekeD3Refuse2;

    [Header("Vanelle")]
    public DialogueManager vanelleD1P1;
    public DialogueManager VanelleD1Q1AP1;
    public DialogueManager VanelleD1Q1BP1;
    public DialogueManager VanelleD1Q1BP2;
    public DialogueManager VanelleD1Q1BRefuse;
    public DialogueManager VanelleD2Q1A;
    public DialogueManager VanelleD2Q1AGA;
    public DialogueManager VanelleD2Q1AGB;
    public DialogueManager VanelleD2Q1AGC;
    public DialogueManager vanelleD2Q1AGPlusRefuse;
    public DialogueManager vanelleD2Q1AGPlusGive;
    public DialogueManager vanelleD2Q1AGMinusRefuse;
    public DialogueManager vanelleD2Q1AGMinusGive;

    [Header("Nomira")]
    public DialogueManager nomiraD1P1;
    public DialogueManager nomiraD1P2;
    public DialogueManager nomiraD1Q1AP1;
    public DialogueManager nomiraD1Q1AP2;
    public DialogueManager nomiraD1Q1AEWeapon;
    public DialogueManager nomiraD1Q1BP1;
    public DialogueManager nomiraD1Q1BP2;
    public DialogueManager nomiraD1Q1BEDivine;
    public DialogueManager nomiraD1Q1AB;
    public DialogueManager nomiraD1Q1AB2;
    public DialogueManager nomiraD1Q1AB3;
    public DialogueManager nomiraD1Q1ABEOther;
    public DialogueManager NomiraD1QOw;
    public DialogueManager nomiraP2QAWeapon;
    public DialogueManager nomiraP2QAArcaneFocus1;
    public DialogueManager nomiraP2QAArcaneFocus2;
    public DialogueManager nomiraP2QAGWeaponMinus;
    public DialogueManager nomiraP2QAGWeaponEquals;
    public DialogueManager nomiraP2QAGWeaponPlus;
    public DialogueManager nomiraP2QAGArcaneFoucosMinus;
    public DialogueManager nomiraP2QAGArcaneFoucosEquals;
    public DialogueManager nomiraP2QAGArcaneFoucosPlus;
    public DialogueManager nomiraP2QBDivineFocus1;
    public DialogueManager nomiraP2QBDivineFocus2;
    public DialogueManager nomiraP2QBOtherFocus;
    public DialogueManager nomiraP2QBGDivineFocusMinus;
    public DialogueManager nomiraP2QBGDivineFocusEquals;
    public DialogueManager nomiraP2QBGDivineFocusPlus;

    [Header("Zeto")]
    public DialogueManager zetoD1P1;
    public DialogueManager zetoD1QAGMinus;
    public DialogueManager zetoD1QAGEquals;
    public DialogueManager zetoD1QAGPlus;
    public DialogueManager zetonomiraD1P1;
    public DialogueManager zetoD1ASteal;
    public DialogueManager zetoD1BSteal;
    public DialogueManager zetoP2QA1;
    public DialogueManager zetoP2QAG;
    public DialogueManager ZetoP2QB1;
    public DialogueManager ZetoP2QBGMinus;
    public DialogueManager ZetoP2QBGEquals;
    public DialogueManager ZetoP2QBGPlus;


    [Header("Dialogue History")]
    public DialogueManager dialogueHistory;

    [Header("CharacterStates")]
    public DialogueManager questDialogue;
    public DialogueManager equipmentDialogue;
    public DialogueManager rewardDialogue;

    [SerializeField] DialogueUI duiScript;

    [SerializeField] DialogueCharacter tahmasCharacter;
    public DialogueCharacter zetoCharacter;

    public bool isQuest;
    public bool isReward;
    public bool isEquipment;
    public bool isIdle;

    public bool pickedQ1A;
    public bool pickedQ1B;

    public bool pickedItemA;
    public bool pickedItemB;

    public bool D1 = true;
    public bool D2 = false;
    public bool D3 = false;

    [SerializeField] DialogueUI dui;

    public static CharacterSystem instance;

    private void Awake()
    {
        instance = this;

        currentCharacter = characters[characterCount];
        currentCharacterObject = characterObjects[characterCount];
    }
    private void Start()
    {
        currentCharacterObject.GetComponent<MoveCharacter>().MoveToDesk();
        ChangeTextColor();
    }

    public void IsQuest()
    {
        isQuest = true;
        isReward = false;
        isEquipment = false;
        isIdle = false;
    }

    public void IsReward()
    {
        isReward = true;
        isQuest = false;
        isEquipment = false;
        isIdle = false;
    }

    public void IsEquipment()
    {
        isEquipment = true;
        isReward = false;
        isQuest = false;
        isIdle = false;
    }

    public void IsIdle()
    {
        isIdle = true;
        isEquipment = false;
        isReward = false;
        isQuest = false;
    }

    public void StartNewCharacter()
    {
        characterCount++;
        if (characterCount == characters.Count) //characters.Length
        {
            Debug.Log("no more");
        }
        else
        {
            currentCharacter = characters[characterCount];
            currentCharacterObject = characterObjects[characterCount];

            SkipCharacter();

            //NEW SHIT for rep system
            PlayerRepTrackerCharacter.instance.ActivateNpc(characterCount);

            currentCharacterObject.GetComponent<MoveCharacter>().MoveToDesk();
            ChangeTextColor();
        }
    }

    public void QuestADialogue()
    {
        if (currentCharacter.characterName == "Josie")
        {
            josieD1Q1AP1.StartNewDialogue(dialogueTriggerScript);
            
        }

        if (currentCharacter.characterName == "Greg")
        {
            gregD1Q1AP1.StartNewDialogue(dialogueTriggerScript);
            //currentCharacterObject.GetComponent<CharacterReputation>().RemoveReputation(1);
        }

        if(currentCharacter.characterName == "Andy")
        {
            //when dragon quest it given
            if (!AndyScript.instance.partOneComplete)
            {
                andyD1Q1AP1.StartNewDialogue(dialogueTriggerScript);
                
            }
            else
            {
                andyD2Q1AP2.StartNewDialogue(dialogueTriggerScript);
            }

            currentCharacterObject.GetComponent<CharacterReputation>().AddReputation(2);

        }

        if (currentCharacter.characterName == "Maggie") //food
        {
            maggieD1Q1AP1.StartNewDialogue(dialogueTriggerScript);
            currentCharacterObject.GetComponent<CharacterReputation>().ModifyReputation(1);
        }

        if (currentCharacter.characterName == "Jolene")
        {
            joleneD1Q1AP1.StartNewDialogue(dialogueTriggerScript);

            tahmasCharacter.choseQuestA = true;

            currentCharacterObject.GetComponent<CharacterReputation>().ModifyReputation(-1);
        }

        if (currentCharacter.characterName == "Vanelle")
        {
            VanelleD1Q1AP1.StartNewDialogue(dialogueTriggerScript);
        }

        if (currentCharacter.characterName == "Nomira")
        {
            if (!NomiraScript.instance.brokeStaff)
            {
                nomiraD1Q1AP1.StartNewDialogue(dialogueTriggerScript);
            }
            else
            {
                nomiraD1Q1AB2.StartNewDialogue(dialogueTriggerScript);
            }

        }
    }

    public void QuestBDialogue()
    {
        if (currentCharacter.characterName == "Josie")
        {
            josieD1Q1BP1.StartNewDialogue(dialogueTriggerScript);
        }

        if (currentCharacter.characterName == "Greg")
        {
            gregD1Q1BP1.StartNewDialogue(dialogueTriggerScript);
            //Adds reputation
            currentCharacterObject.GetComponent<CharacterReputation>().AddReputation(1);
        }
        if(currentCharacter.characterName == "Andy")
        {
            if (!AndyScript.instance.partOneComplete)
            {
                andyD1Q1BP1.StartNewDialogue(dialogueTriggerScript);
                currentCharacterObject.GetComponent<CharacterReputation>().RemoveReputation(1);
            }
            else
            {
                andyD2Q1BP3.StartNewDialogue(dialogueTriggerScript);
                currentCharacterObject.GetComponent<CharacterReputation>().RemoveReputation(2);
            }
            
        }

        if (currentCharacter.characterName == "Maggie")
        {
            maggieD1Q1BP1.StartNewDialogue(dialogueTriggerScript);
            //currentCharacterObject.GetComponent<CharacterReputation>().ModifyReputation(1);
        }

        if (currentCharacter.characterName == "Jolene")
        {
            joleneD1Q1BP1.StartNewDialogue(dialogueTriggerScript);
            joleneS.joleneDead = true;
            tahmasCharacter.choseQuestB = true;
            currentCharacterObject.GetComponent<CharacterReputation>().ModifyReputation(-1);
        }

        if (currentCharacter.characterName == "Vanelle")
        {
            VanelleD1Q1BP1.StartNewDialogue(dialogueTriggerScript);
        }

        if (currentCharacter.characterName == "Nomira")
        {
            if (!NomiraScript.instance.brokeStaff)
            {
                nomiraD1Q1BP1.StartNewDialogue(dialogueTriggerScript);
            }
            else
            {
                nomiraD1Q1AB2.StartNewDialogue(dialogueTriggerScript);
            }
            
        }
    }

    public void ItemADialogue()
    {
        if(currentCharacter.characterName == "Lorne")
        {
            if (D1 && !D2)
            {
                lorneD1I1P1.StartNewDialogue(dialogueTriggerScript);
                //D1 = false;
                //D2 = true;
            }
        }

        if (currentCharacter.characterName == "Lotest") //drab
        {
            lotestD1ItemA1.StartNewDialogue(dialogueTriggerScript);
            currentCharacterObject.GetComponent<CharacterReputation>().ModifyReputation(-1);
        }

        if(currentCharacter.characterName == "Zeke")
        {
            zekeD3Feed.StartNewDialogue(dialogueTriggerScript);
            currentCharacterObject.GetComponent<CharacterReputation>().ModifyReputation(1);
        }
    }
    public void ItemBDialogue()
    {
        if (currentCharacter.characterName == "Lorne")
        {
            
            if (D1 && !D2)
            {
                lorneD1I1P1.StartNewDialogue(dialogueTriggerScript);
            }

            else if (!D1 && D2)
            {
                lorneD2ItemB.StartNewDialogue(dialogueTriggerScript);
                currentCharacterObject.GetComponent<CharacterReputation>().ModifyReputation(1);
                //gave yarn
                Debug.Log("gave yarn");
                //lorneS.gaveYarn = true;
            }
        }


        if (currentCharacter.characterName == "Lotest") //fancy
        {
            lotestD1ItemB1.StartNewDialogue(dialogueTriggerScript);
            currentCharacterObject.GetComponent<CharacterReputation>().ModifyReputation(1);
        }
    }

    public void ChangeTextColor()
    {
        dui.messageText.color = currentCharacter.textColor;
        dui.nameText.color = currentCharacter.textColor;
    }

    public void TextHistory()
    {
        Debug.Log("text history");
        dialogueHistory.currentSentence = 0;
        dialogueHistory.sentences[dialogueHistory.currentSentence].sentence = duiScript.lastMessage;
        dialogueHistory.sentences[dialogueHistory.currentSentence].dialogueCharacter = currentCharacter;
        dialogueHistory.StartNewDialogue(dialogueTriggerScript);
    }

    private void SkipCharacter()
    {
        //new shit for reodering characters
        if (currentCharacter.characterName == "Vanelle" && !currentCharacter.choseQuestA) //if you refused vanelle any quest
        {
            characterCount++; //SKIP VANELLE
            currentCharacter = characters[characterCount];
            currentCharacterObject = characterObjects[characterCount];
        }
        if (currentCharacter.characterName == "Lorne" && !LorneScript.instance.gaveYarn && LorneScript.instance.partTwoComplete) //if you've already talked to Lorne two times and didn't give them Yarn...
        {
            characterCount++; //SKIP LORNE
            currentCharacter = characters[characterCount];
            currentCharacterObject = characterObjects[characterCount];
        }
        if (currentCharacter.characterName == "Lotest" && currentCharacter.choseItemB && LotestScript.instance.partOneComplete) //if player gave exploding beans, after part one, skip lotest to Josie
        {
            characterCount++; 
            currentCharacter = characters[characterCount];
            currentCharacterObject = characterObjects[characterCount];
            LotestScript.instance.skipJosie = false;
        }
        if (currentCharacter.characterName == "Josie" && LotestScript.instance.skipJosie && LotestScript.instance.partOneComplete) //if josies turn, lotest was not skipped on his quest return, skip Josie
        {
            characterCount++;
            currentCharacter = characters[characterCount];
            currentCharacterObject = characterObjects[characterCount];
        }
        if (currentCharacter.characterName == "Andy" && currentCharacter.choseQuestB && AndyScript.instance.partTwoComplete) //if josies turn, lotest was not skipped on his quest return, skip Josie
        {
            characterCount++;
            currentCharacter = characters[characterCount];
            currentCharacterObject = characterObjects[characterCount];
        }
    }
}
