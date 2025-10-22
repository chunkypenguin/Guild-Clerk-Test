using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public DialogueCharacter[] characters;

    [SerializeField] DialogueCharacter finchCharacter;
    [SerializeField] DialogueCharacter zetoCharacter;
    [SerializeField] DialogueCharacter vanelleCharacter;
    [SerializeField] DialogueCharacter lotestCharacter;
    [SerializeField] DialogueCharacter kalinCharacter;

    [SerializeField] Image startBGImage;
    [SerializeField] float fadeTime;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;

        ResetCharacters();
    }

    private void Start()
    {
        startBGImage.DOFade(0, fadeTime).OnComplete(() => startBGImage.gameObject.SetActive(false));
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

        kalinCharacter.choseQuestA = true; // Kalin

    }

    public void MenuScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
