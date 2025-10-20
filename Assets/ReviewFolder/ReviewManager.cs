using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ReviewManager : MonoBehaviour
{
    //for testing
    [System.Serializable]
    public class CharacterReviewTests
    {
        [Header("Character Name")]
        public string name;

        [Header("Which quest was chosen?")]
        public bool questChosenA;
        public bool questChosenB;

        [Header("How much gold does that quest ask for?")]
        public int requestedA;
        public int requestedB;

        [Header("How much gold did the player give?")]
        public int goldGiven;

    }

    public CharacterReviewTests[] characterReviewTests;
    //The above is for testing


    public float overallAverageRep;

    [SerializeField] int totalRequestedGold;
    [SerializeField] int totalGoldGiven;

    [SerializeField] List<float> characterGoldAccuracy;

    [Header("Results")]
    public int totalGoldOverUnder;
    public float averageBias, averageAccuracy;

    [SerializeField] List<GameObject> heartFX;
    public List<int> hearts;
    private int heartCount;

    private AudioSource heartAudio;


    public static ReviewManager instance;   
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        heartAudio = UIVisualizer.instance.heartAudio;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TestingCharacterGoldReview();

        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            HeartDisplay();
        }
    }

    public void GetReview()
    {
        CallCharacterAccuracy();
        OverallRep();
    }

    public void OverallRep()
    {
        overallAverageRep = DayReputationTracker.Instance.OverallAverageRep();
    }

    public void CalculateTotalRequestedGold() //I DONT THINK WE NEED THIS
    {
        totalRequestedGold =

         AndyScript.instance.andyRequestedGoldFetch
        + FinchScript.instance.finchRequestedGold
        + GregScript.instance.gregRequestedGold
        + JoleneScript.instance.joleneRequestedGold
        + KalinScript.instance.kalinRequestedGold
        + LotestScript.instance.lotestRequestedGold
        + MaggieScript.instance.maggieRequestedGold
        + NomiraScript.instance.nomiraRequestedGold
        + TahmasScript.instance.tahmasRequestedGold
        + VanelleScript.instance.vanelleRequestedGold;
        //+ZetoScript.instance.zetoRequestedGold;
    }

    public void CallCharacterAccuracy()
    {
        AndyScript.instance.AndyGold();
        NomiraScript.instance.NomiraGold();
        ZetoScript.instance.ZetoGold();
        AchillesScript.instance.AchillesGold();
        TahmasScript.instance.TahmasGold();
        LotestScript.instance.LotestGold();
        FinchScript.instance.FinchGold();
        GregScript.instance.GregGold();
        KalinScript.instance.KalinGold();
        VanelleScript.instance.VanelleGold();
        MaggieScript.instance.MaggieGold();
        JoleneScript.instance.JoleneGold();

        CalculateGoldReview();
    }

    public void CharacterGoldAccuracyCalculator(int goldGiven, int goldRequested)
    {
        if (goldRequested == 0)
        {
            characterGoldAccuracy.Add(0f);
            return;
        }

        float goldPercentageDifference = (goldGiven -  goldRequested) / (float)goldRequested;
        characterGoldAccuracy.Add(goldPercentageDifference);

        totalRequestedGold += goldRequested;
        totalGoldGiven += goldGiven;
    }

    public void CalculateGoldReview()
    {
        totalGoldOverUnder = totalGoldGiven - totalRequestedGold; //ex: gave 400 - total 600 = -200 (gave 200 less than asked)

        averageBias = characterGoldAccuracy.Average(); //- undergive too little!, + overgive too much!
        averageAccuracy = characterGoldAccuracy.Select(x => 1f - Mathf.Abs(x)).Average(); //how close, ignoring direction
    }

    //Below is part of testing

    public void TestingCharacterGoldReview()
    {
        //reset gold count each time
        totalRequestedGold = 0;
        totalGoldGiven = 0;
        characterGoldAccuracy.Clear();

        foreach (CharacterReviewTests test in characterReviewTests)
        {
            int requested = 0;
            if (test.questChosenA)
            {
                requested = test.requestedA;
            }
            else if (test.questChosenB)
            {
                requested = test.requestedB;
            }
            int given = test.goldGiven;

            CharacterGoldAccuracyCalculator(given, requested);
        }

        CalculateGoldReview();

        TestReviewDialogueNew();
    }

    public void TestReviewDialogue()
    {
        //more
        if(totalGoldGiven > totalRequestedGold)
        {
            if(totalGoldGiven < (totalRequestedGold + totalRequestedGold * 0.05f)) //5%
            {
                //very close dialogue
            }
            else if(totalGoldGiven < (totalRequestedGold + totalRequestedGold * 0.15f) && totalGoldGiven >= (totalRequestedGold + totalRequestedGold * 0.05f))
            {
                //pretty close dialogue
            }
            else if (totalGoldGiven < (totalRequestedGold + totalRequestedGold * 0.3f) && totalGoldGiven >= (totalRequestedGold + totalRequestedGold * 0.15f))
            {
                //off more dialogue
            }
        }
        //less
        else if(totalGoldGiven < totalRequestedGold)
        {
            if (totalGoldGiven > (totalRequestedGold - totalRequestedGold * 0.05f)) //5%
            {
                //very close dialogue
            }
            else if (totalGoldGiven > (totalRequestedGold - totalRequestedGold * 0.15f) && totalGoldGiven <= (totalRequestedGold - totalRequestedGold * 0.05f))
            {
                //pretty close dialogue
            }
            else if (totalGoldGiven > (totalRequestedGold - totalRequestedGold * 0.3f) && totalGoldGiven <= (totalRequestedGold - totalRequestedGold * 0.15f))
            {
                //off less dialogue
            }
        }
        //equal
        else if(totalGoldGiven == totalRequestedGold)
        {
            //perfect dialogue
        }
    }

    public void TestReviewDialogueNew()
    {
        // Both totalGoldGiven and totalRequestedGold are ints, so difference is an int too.
        float difference = totalGoldGiven - totalRequestedGold;

        // Cast totalRequestedGold to float to ensure floating-point division, not integer division.
        float percentDiff = difference / (float)totalRequestedGold;

        // Log the math results for clarity
        Debug.Log($"Requested: {totalRequestedGold}, Given: {totalGoldGiven}, Difference: {difference}, PercentDiff: {percentDiff:P1}");

        if (Mathf.Approximately(difference, 0))
        {
            ReviewBar.instance.AddReviewPoints(0.3f);
            // Perfect dialogue
            Debug.Log("Perfect dialogue");
            return;
        }

        if (percentDiff > 0) // Gave more
        {
            if (percentDiff < 0.05f)
            {
                // very close dialogue
                ReviewBar.instance.AddReviewPoints(0.2f);
                Debug.Log("very close dialogue");
            }
            else if (percentDiff < 0.15f)
            {
                // pretty close dialogue
                ReviewBar.instance.AddReviewPoints(0.1f);
                Debug.Log("pretty close dialogue");
            }
            else if (percentDiff < 0.3f)
            {
                ReviewBar.instance.AddReviewPoints(-0.1f);
                // off more dialogue
                Debug.Log("off more dialogue");
            }
            else
            {
                ReviewBar.instance.AddReviewPoints(-0.2f);
                // way too much dialogue
                Debug.Log("way too much dialogue");
            }
        }
        else // Gave less
        {
            // Make the percentage difference positive for easier comparisons
            percentDiff = Mathf.Abs(percentDiff);

            if (percentDiff < 0.05f)
            {
                ReviewBar.instance.AddReviewPoints(0.2f);
                // very close dialogue (slightly under)
                Debug.Log("very close dialogue");
            }
            else if (percentDiff < 0.15f)
            {
                ReviewBar.instance.AddReviewPoints(0.1f);
                // pretty close dialogue (a bit under)
                Debug.Log("pretty close dialogue");
            }
            else if (percentDiff < 0.3f)
            {
                ReviewBar.instance.AddReviewPoints(-0.1f);
                // off less dialogue (noticeably under)
                Debug.Log("off less dialogue");
            }
            else
            {
                ReviewBar.instance.AddReviewPoints(-0.2f);
                // way too little dialogue
                Debug.Log("way too little dialogue");
            }
        }
    }

    public void HeartDisplay()
    {
        StartCoroutine(DisplayHeart());
    }

    IEnumerator DisplayHeart()
    {
        foreach (int heart in hearts)
        {
            if (ReviewBar.instance.reviewSlider.value >= 1f)
            {
                Debug.Log("Review Bar Filled!");
                yield break; // exits the coroutine cleanly
            }

            PlayHeartFX(heart);

            heartCount++;
            if (heartCount >= heartFX.Count)
            {
                heartCount = 0; // wrap around
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void PlayHeartFX(int currentRep)
    {
        if (currentRep > 0)
        { // used to be currentRep > previousRep
            if (heartFX[heartCount].transform.GetChild(0) == null)
            {
                Debug.LogError("UIVisualizer: No positive heart FX assigned.");
            }

            heartFX[heartCount].transform.GetChild(0).gameObject.SetActive(true);

            if (heartAudio != null)
            {
                heartAudio.pitch = 1.2f;
                heartAudio.PlayOneShot(heartAudio.clip);
            }

            ReviewBar.instance.AddReviewPoints(0.15f);
        }
        else if (currentRep < 0)
        {
            if (heartFX[heartCount].transform.GetChild(1) == null)
            {
                Debug.LogError("UIVisualizer: No positive heart FX assigned.");
            }

            heartFX[heartCount].transform.GetChild(1).gameObject.SetActive(true);

            if (heartAudio != null)
            {
                heartAudio.pitch = 0.5f;
                heartAudio.PlayOneShot(heartAudio.clip);
            }

            ReviewBar.instance.AddReviewPoints(-0.1f);
        }
    }

}
