using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using HeneGames.DialogueSystem;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject nameInputField;
    [SerializeField] TMP_InputField inputField;
    bool inputFieldShown;

    public string playerName = "Hi";

    [SerializeField] CharacterSystem cs;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inputFieldShown)
        {
            Debug.Log("Enter");
            PrintInputText();
            //ShowInputField();
            //cs.josieD1P2.StartNewDialogue(cs.dialogueTriggerScript);
            ContinueDialogue();
        }
    }

    public void ShowInputField()
    {
        if(!inputFieldShown)
        {
            nameInputField.SetActive(true);
            inputFieldShown = true;
        }
        else
        {
            nameInputField.SetActive(false);
            inputFieldShown = false;
        }

    }

    public void PrintInputText()
    {
        playerName = inputField.text; // Get the text
        Debug.Log("User input: " + playerName);
    }

    public void ContinueDialogue()
    {
        if(string.IsNullOrWhiteSpace(playerName))
        {
            //Do nothing
        }
        else
        {
            ShowInputField();
            cs.josieD1P2.StartNewDialogue(cs.dialogueTriggerScript);
        }

    }
}
