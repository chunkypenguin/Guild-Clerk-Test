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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inputFieldShown)
        {
            Debug.Log("Enter");
            PrintInputText();
            ShowInputField();
            cs.josieD1P2.StartNewDialogue(cs.dialogueTriggerScript);
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
}
