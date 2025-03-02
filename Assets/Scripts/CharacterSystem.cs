using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : MonoBehaviour
{
    public DialogueCharacter[] characters; //for example, Josie character
    public DialogueCharacter currentCharacter;

    public GameObject[] characterObjects;
    public GameObject currentCharacterObject;

    public DialogueTrigger dialogueTriggerScript;

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

    [Header("Greg")]
    public DialogueManager gregD1P1;
    public DialogueManager gregD1Q1AP1;
    public DialogueManager gregD1Q1BP1;

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

    [Header("Lorne")]
    public DialogueManager lorneD1P1;
    public DialogueManager lorneD1I1P1;

    [Header("Maggie")]
    public DialogueManager maggieD1P1;
    public DialogueManager maggieD1Q1AP1;
    public DialogueManager maggieD1Q1BP1;

    [Header("Jolene")]
    public DialogueManager joleneD1P1;
    public DialogueManager joleneD1Q1AP1;
    public DialogueManager joleneD1Q1BP1;

    public bool isQuest;
    public bool isReward;
    public bool isEquipment;

    public bool pickedQ1A;
    public bool pickedQ1B;

    public bool pickedItemA;
    public bool pickedItemB;

    //[SerializeField] GameObject josie;
    //[SerializeField] GameObject greg;

    private void Awake()
    {
        currentCharacter = characters[characterCount];
        currentCharacterObject = characterObjects[characterCount];
    }
    private void Start()
    {
        currentCharacterObject.GetComponent<MoveCharacter>().MoveToDesk();
    }

    public void IsQuest()
    {
        isQuest = true;
        isReward = false;
        isEquipment = false;
    }

    public void IsReward()
    {
        isReward = true;
        isQuest = false;
        
        isEquipment = false;
    }

    public void IsEquipment()
    {
        isEquipment = true;
        isReward = false;
        isQuest = false;
    }

    public void StartNewCharacter()
    {
        characterCount++;
        if (characterCount == characters.Length)
        {
            Debug.Log("no more");
        }
        else
        {
            currentCharacter = characters[characterCount];
            currentCharacterObject = characterObjects[characterCount];

            currentCharacterObject.GetComponent<MoveCharacter>().MoveToDesk();
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
        }

        if(currentCharacter.characterName == "Andy")
        {
            andyD1Q1AP1.StartNewDialogue(dialogueTriggerScript);
        }

        if (currentCharacter.characterName == "Maggie")
        {
            joleneD1Q1AP1.StartNewDialogue(dialogueTriggerScript);
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
        }
        if(currentCharacter.characterName == "Andy")
        {
            andyD1Q1BP1.StartNewDialogue(dialogueTriggerScript);
        }

        if (currentCharacter.characterName == "Maggie")
        {
            maggieD1Q1BP1.StartNewDialogue(dialogueTriggerScript);
        }

        if (currentCharacter.characterName == "Jolene")
        {
            joleneD1Q1BP1.StartNewDialogue(dialogueTriggerScript);
        }
    }

    public void ItemADialogue()
    {
        if(currentCharacter.characterName == "Lorne")
        {
            lorneD1I1P1.StartNewDialogue(dialogueTriggerScript);
        }
    }
    public void ItemBDialogue()
    {
        if (currentCharacter.characterName == "Lorne")
        {
            lorneD1I1P1.StartNewDialogue(dialogueTriggerScript);
        }
    }
}
