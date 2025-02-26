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
    public DialogueManager josieD1P1;
    public DialogueManager josieD1P2;
    public DialogueManager josieD1P3;
    public DialogueManager josieD1P4;
    public DialogueManager josieD1Q1AP1;
    public DialogueManager josieD1Q1AP2;
    public DialogueManager josieD1Q1BP1;
    public DialogueManager josieD1Q1BP2;
    public DialogueManager josieD1G1AP1;
    public DialogueManager josieD1G1BP1;
    public DialogueManager josieD1G1CP1;

    public bool isQuest;
    public bool isReward;
    public bool isEquipment;

    public bool pickedQ1A;
    public bool pickedQ1B;

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
        currentCharacter = characters[characterCount];
        currentCharacterObject = characterObjects[characterCount];

        currentCharacterObject.GetComponent<MoveCharacter>().MoveToDesk();
    }
}
