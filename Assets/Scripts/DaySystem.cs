using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    bool gameEnd;

    //new stuff for end of day screen
    [SerializeField] TMP_Text dayText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] GameObject nextDayText;
    [SerializeField] GameObject goldBundleText;
    [SerializeField] GameObject nextDayButton;

    [TextArea(10, 15)]
    [SerializeField] string[] dayDescription;

    // This is a reference to the UIVisualizer script. This just changes the visuals of the UI.
    [SerializeField] private UIVisualizer _reputationVisualizer;

    //refernce to coin text
    [SerializeField] GameObject coinText;

    public static DaySystem instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameEnd = false;
        dayCount = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //EndDay();
            CreditsStart();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            NextDayButton(); //TURN THIS BACK ON

        }
        if(Input.GetKeyDown(KeyCode.Escape) && gameEnd)
        {
            GameManager.instance.QuitGame();
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
                
                //canStartNextDay = false;

                CreditsStart();

                //gameEnd = true;

                //bgMusic.DOFade(0, 7);
                //otherBGMusic.DOFade(0, 7);
                //otherOtherBGMusic.DOFade(0, 7);
                //creditSceneMusic.DOFade(0.5f, 10);
            }

            // = false; //trying to fix adding a bunch of characters to screen inbetween days DID NOT FIX
        }
    }

    private void CreditsStart()
    {
        gameEnd = true;

        canStartNextDay = false;

        //credits...
        credits.SetActive(true);
        creditsImage.DOFade(1, 5).SetEase(Ease.Linear);
        Invoke(nameof(CreditsScroll), 5f);

        gameEnd = true;

        bgMusic.DOFade(0, 7);
        otherBGMusic.DOFade(0, 7);
        otherOtherBGMusic.DOFade(0, 7);
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

        dayOneTextObject.SetActive(false);
        dayTwoTextObject.SetActive(false);

        dayText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);

        //nextDayText.gameObject.SetActive(false);
        nextDayButton.SetActive(false);

        goldBundleText.SetActive(false);

        _reputationVisualizer.ShowHeartVisual(false);

        coinText.SetActive(false);

        dayCount++;

        //for josie final emote change
        if(dayCount == 5)
        {
            TutorialScript.instance.ChangeEmote(TutorialScript.instance.josieShocked);
            Debug.Log("Josie Shocked");
        }

        if (targetImage != null)
        {
            // Fade in effect
            targetImage.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                targetImage.gameObject.SetActive(false);

                //PROMPT DECORATION INSTEAD
                AlexTabScript.instance.ShowDecorUI();
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

        dayText.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);
        //nextDayText.SetActive(true);
        nextDayButton.SetActive(true);
        coinText.SetActive(true);
        dayText.text = "Day " + dayCount.ToString();
        descriptionText.text = dayDescription[dayCount].ToString(); 

        if(TutorialScript.instance.goldBundle.activeSelf)
        {
            goldBundleText.SetActive(true);
        }
        
    }
}
