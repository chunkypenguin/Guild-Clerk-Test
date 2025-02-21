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

    //[SerializeField] DialogueManager dialogueManagerScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
