using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using Lightbug.GrabIt;


[System.Serializable]
public class DayTexts
{
    public TextMeshProUGUI[] ParaTexts;
}

public class DaySystem : MonoBehaviour
{
    public int dayCount = 1;
    public Image targetImage; // Assign this in the Inspector
    public float fadeDuration = 1.5f; // Duration of fade effect
    [SerializeField] GameObject dayOneTextObject;
    [SerializeField] GameObject dayTwoTextObject;
    [SerializeField] GameObject dayThreeTextObject;

    bool canStartNextDay;

    [SerializeField] CharacterSystem cs;

    [SerializeField] GameObject[] returnItems;

    bool endGame;

    [SerializeField] GameObject credits;
    [SerializeField] Image creditsImage;
    public AudioSource bgMusic;
    public AudioSource otherBGMusic;
    public AudioSource otherOtherBGMusic;
    public AudioSource creditSceneMusic;

    public bool gameEnd;

    //new stuff for end of day screen
    [SerializeField] TMP_Text dayText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] GameObject nextDayText;
    [SerializeField] GameObject goldBundleText;
    [SerializeField] GameObject goldBundleCoinText;
    [SerializeField] GameObject nextDayButton;

    //even newer stuff for end of day scrolling text
    [SerializeField] TextMeshProUGUI dayTextMeshProUGUI;

    [Header("Day Text")]
    [SerializeField] DayTexts[] dayTexts;
    [SerializeField] TextMeshProUGUI[] paragraphTextMesh;
    [SerializeField] GameObject newDayTextObject;

    [TextArea(10, 15)]
    [SerializeField] string[] dayDescription;

    // This is a reference to the UIVisualizer script. This just changes the visuals of the UI.
    [SerializeField] private UIVisualizer _reputationVisualizer;

    [Header("Gold/Rep Recap")]
    [SerializeField] public List<GameObject> recapObjects = new List<GameObject>();
    [SerializeField] float recapDisplayTime;
    public bool displayingGold;
    [SerializeField] AudioSource coinSound;

    //refernce to coin text
    [SerializeField] GameObject coinText;

    [SerializeField] GameObject recapButton;

    public bool endOfDay;

    public bool endOfDayCantPause;

    public bool escTextOn;
    bool pressedEsc;

    public static DaySystem instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameEnd = false;
        dayCount = 1;

        MakeAllInvisible();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //EndDay();
            //CreditsStart();
            //EndOfDayScroll();
            //NewEndDay();
        }

        if(Input.GetKeyDown(KeyCode.Space) && endOfDay)
        {
            //NextDayButton(); //TURN THIS BACK ON
            Debug.Log("space");
            DialogueUI.instance.CompleteDayText();

        }

        if(Input.GetMouseButtonDown(0) && endOfDay)
        {
            Debug.Log("mouse");
            DialogueUI.instance.CompleteDayText();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && gameEnd)
        {
            if (!escTextOn && !pressedEsc)
            {
                pressedEsc = true;
                Credits.instance.FadeReturnMenuText();
            }
            else
            {
                Debug.Log("quit to main menu");
                //GameManager.instance.QuitGame();
                GameManager.instance.MenuScene();
            }

        }

    }

    public void EndDay()
    {
        //targetImage.gameObject.SetActive(true);
        //if (targetImage != null)
        //{
        //    // Fade in effect
        //    targetImage.DOFade(1f, fadeDuration).OnComplete(() =>
        //    {

        //        if(dayCount == 1)
        //        {
        //            //dayOneTextObject.SetActive(true);
        //            cs.D2 = true;
        //            cs.D1 = false;
        //        }
        //        else if (dayCount == 2)
        //        {
        //            //dayTwoTextObject.SetActive(true);
        //            cs.D3 = true;
        //            cs.D2 = false;
        //            cs.D1 = false;
        //        }
        //        else
        //        {
        //            //dayThreeTextObject.SetActive(true);
        //            endGame = true;
        //        }

        //        canStartNextDay = true;
        //        cs.currentCharacterObject.GetComponent<MoveCharacter>().MoveEndDay();


        //        //new stuff
        //        _reputationVisualizer.ShowEndOfDay();
        //        Debug.Log("should do end of day stuff");
        //        EndOfDayScreen();


        //        //remove return items
        //        foreach (GameObject obj in returnItems)
        //        {
        //            obj.SetActive(false);
        //        }
        //    });


        //}
        //else
        //{
        //    Debug.LogError("No Image assigned to FadeImage script!");
        //}

        Debug.Log("old End Day Call");
    }



    public void NewEndDay()
    {
        endOfDayCantPause = true;
        Debug.Log("NewEndDay");
        targetImage.gameObject.SetActive(true);
        if (targetImage != null)
        {
            Debug.Log("Start Fade");
            // Fade in effect
            targetImage.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                Debug.Log("Fade Complete");
                if (dayCount == 1)
                {
                    //dayOneTextObject.SetActive(true);
                    cs.D2 = true;
                    cs.D1 = false;
                }
                else if (dayCount == 2)
                {
                    //dayTwoTextObject.SetActive(true);
                    cs.D3 = true;
                    cs.D2 = false;
                    cs.D1 = false;
                }
                else if(dayCount == 3)
                {
                    //dayThreeTextObject.SetActive(true);
                }
                else if (dayCount == 4)
                {
                    //dayThreeTextObject.SetActive(true);
                }
                else if (dayCount == 5)
                {
                    //dayThreeTextObject.SetActive(true);
                    endGame = true;
                }

                canStartNextDay = true;

                if(cs.currentCharacter.characterName != "End Of Day")
                {
                    cs.currentCharacterObject.GetComponent<MoveCharacter>().MoveEndDay();
                }

                newDayTextObject.SetActive(true);
                endOfDay = true;

                //new stuff
                _reputationVisualizer.ShowEndOfDay();
                Debug.Log("should do end of day stuff");
                EndOfDayScreen();

                //Reset cam to center between days
                movecam.instance.DayCamReset();

                //remove return items
                foreach (GameObject obj in returnItems)
                {
                    obj.SetActive(false);
                }
            });


        }
        else
        {
            Debug.LogError("No Image assigned to FadeImage script!");
        }


    }
    public void NextDayButton()
    {
        if (canStartNextDay)
        {
            Debug.Log("Passed next day bool");
            if (!endGame)
            {
                canStartNextDay = false;
                NewDay();
            }
            else
            {
                //CreditsStart();

                NewDay();
            }
        }
    }

    public void CreditsStart()
    {
        gameEnd = true;

        canStartNextDay = false;

        //after recap
        Recap.instance.RecapPage.SetActive(false);

        //credits...
        credits.SetActive(true);
        creditsImage.DOFade(1, 5).SetEase(Ease.Linear);
        Invoke(nameof(CreditsScroll), 5f);

        //gameEnd = true;

        bgMusic.DOFade(0, 7);
        otherBGMusic.DOFade(0, 7);
        otherOtherBGMusic.DOFade(0, 7);
        creditSceneMusic.Play();
        creditSceneMusic.DOFade(0.5f, 10);
    }

    private void CreditsScroll()
    {
        Credits.instance.ScrollToTarget();
    }

    public void NewDay()
    {

        Debug.Log("New Day");
        //canStartNextDay = false;//for the space bar option i guess

        TutorialScript.instance.hasGoldBundle = false;

        MakeAllInvisible();

        displayingGold = false;

        HideGoldRecap();

        newDayTextObject.SetActive(false);

        dayOneTextObject.SetActive(false);
        dayTwoTextObject.SetActive(false);

        dayText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);

        //nextDayText.gameObject.SetActive(false);
        nextDayButton.SetActive(false);

        goldBundleText.SetActive(false);

        _reputationVisualizer.ShowHeartVisual(false);

        //coinText.SetActive(false);

        dayCount++;

        //for josie final emote change
        if(dayCount == 5)
        {
            TutorialScript.instance.ChangeEmote(TutorialScript.instance.josieShocked);
            Debug.Log("Josie Shocked");
        }

        if(dayCount > 5)
        {
            Recap.instance.RecapPage.SetActive(true);
            Recap.instance.FadeToRecap();
        }

        if (targetImage != null)
        {
            // Fade in effect
            targetImage.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                endOfDayCantPause = false;
                targetImage.gameObject.SetActive(false);

                if (dayCount <= 5)
                {
                    AlexTabScript.instance.ShowDecorUI();
                }
                //PROMPT DECORATION INSTEAD
                //AlexTabScript.instance.ShowDecorUI();
                //cs.StartNewCharacter();
            });
        }
        else
        {
            Debug.LogError("No Image assigned to FadeImage script!");
        }
    }

    public void EndOfDayScreen()
    {
        Debug.Log("end of day screen activated");

        GrabIt.instance.m_hoveringGrab = false;

        //OLD
        dayText.gameObject.SetActive(true);
        //descriptionText.gameObject.SetActive(true);
        //nextDayText.SetActive(true);
        //nextDayButton.SetActive(true);
        //coinText.SetActive(true);
        dayText.text = "Day " + dayCount.ToString();
        //descriptionText.text = dayDescription[dayCount].ToString(); 

        //THIS WILL HAVE TO CHANGE
        if(TutorialScript.instance.goldBundle.activeSelf)
        {
            //goldBundleText.SetActive(true);
            recapObjects.Insert(5, goldBundleText);
            recapObjects.Insert(6, goldBundleCoinText);
        }

        EndOfDayScroll();
    }

    public void EndOfDayScroll()
    {
        Debug.Log("start day scroll");
        //StartCoroutine(DialogueUI.instance.WriteTextToTextmeshDay(dayDescription[dayCount], dayTextMeshProUGUI));
        DialogueUI.instance.StartDayTextCoroutine(dayTexts);
    }

    public void MakeAllInvisible()
    {
        foreach (DayTexts day in dayTexts)
        {
            foreach (TextMeshProUGUI tmp in day.ParaTexts)
            {
                if (tmp != null)
                {
                    tmp.ForceMeshUpdate();          // ensure TMP knows character count
                    tmp.maxVisibleCharacters = 0;   // hide all characters
                }
            }
        }
    }

    public void GoldRecap()
    {
        endOfDay = false;
        displayingGold = true;
        StartCoroutine(DisplayGoldRecap());
    }

    private IEnumerator DisplayGoldRecap()
    {
        if(dayCount < 5)
        {
            foreach (GameObject obj in recapObjects)
            {
                obj.SetActive(true);

                if (obj.CompareTag("Coins"))
                {
                    coinSound.Play();
                }

                yield return new WaitForSeconds(recapDisplayTime);
            }
        }
        else
        {
            recapButton.SetActive(true);
        }

    }

    private void HideGoldRecap()
    {
        foreach (GameObject obj in recapObjects)
        {
            obj.SetActive(false);
        }

        recapButton.SetActive(false);
    }
}
