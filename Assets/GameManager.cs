using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public DialogueCharacter[] characters;

    [SerializeField] DialogueCharacter finchCharacter;
    [SerializeField] DialogueCharacter zetoCharacter;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;

        ResetCharacters();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Application.Quit();
        }
    }

    private void ResetCharacters()
    {
        foreach (DialogueCharacter data in characters)
        {
            data.ResetBools();
        }

        finchCharacter.choseQuestA = true; //finch

        characters[7].choseQuestA = true; //lotest 

        zetoCharacter.choseQuestA = true; // zeto
    }

    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
