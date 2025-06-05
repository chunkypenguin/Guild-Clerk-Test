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

    public AudioSource bgMusic;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //EndDay();
        }

        if(Input.GetKeyDown(KeyCode.Space) && canStartNextDay)
        {
            if(!endGame)
            {
                canStartNextDay = false;
                NewDay();
            }
            else
            {
                credits.SetActive(true);

                gameEnd = true;

                bgMusic.DOFade(0, 7);
                creditSceneMusic.DOFade(0.5f, 10);
            }

        }
        if(Input.GetKeyDown(KeyCode.Escape) && gameEnd)
        {
            GameManager.instance.QuitGame();
        }

    }

    public void EndDay()
    {
        targetImage.gameObject.SetActive(true);
        if (targetImage != null)
        {
            // Fade in effect
            targetImage.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                
                if(dayCount == 1)
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
                else
                {
                    //dayThreeTextObject.SetActive(true);
                    endGame = true;
                }

                canStartNextDay = true;
                cs.currentCharacterObject.GetComponent<MoveCharacter>().MoveEndDay();


                //new stuff
                _reputationVisualizer.ShowEndOfDay();
                Debug.Log("should do end of day stuff");
                EndOfDayScreen();


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

    public void NewDay()
    {
        canStartNextDay = false;//for the space bar option i guess

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

        if (targetImage != null)
        {
            // Fade in effect
            targetImage.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                targetImage.gameObject.SetActive(false);
                cs.StartNewCharacter();
            });
        }
        else
        {
            Debug.LogError("No Image assigned to FadeImage script!");
        }
    }

    public void EndOfDayScreen()
    {
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
