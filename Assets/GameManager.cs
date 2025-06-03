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
    [SerializeField] DialogueCharacter vanelleCharacter;
    [SerializeField] DialogueCharacter lotestCharacter;

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

        lotestCharacter.choseQuestA = true; //lotest 

        zetoCharacter.choseQuestA = true; // zeto

        vanelleCharacter.choseQuestA = true; // Vanelle

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
