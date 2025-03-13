using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DialogueCharacter[] characters;

    [SerializeField] DialogueCharacter finchCharacter;
    private void Awake()
    {
        ResetCharacters();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void ResetCharacters()
    {
        foreach (DialogueCharacter data in characters)
        {
            data.ResetBools();
        }

        finchCharacter.choseQuestA = true;
    }
}
