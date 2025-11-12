using HeneGames.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

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

    [Header("Gold")]
    public int totalRequestedGold;
    public int totalGoldGiven;
    [SerializeField] List<float> characterGoldAccuracy;

    [Header("Gold Results")]
    public int totalGoldOverUnder;
    public float averageBias, averageAccuracy;
    public float overallAverageRep;
    [SerializeField] bool goldPerfect, goldVeryClose, goldPrettyClose, goldOffLess, goldOffMore, goldWayOffLess, goldWayOffMore;

    public bool goldClipBoard;

    [Header("Character Reputation")]
    [SerializeField] List<CharacterReputation> characterRep;
    public int characterCount;
    public int likes;
    [SerializeField] int baseRep;
    [SerializeField] float repPercentageDif;
    [SerializeField] bool everyoneLovesYou;
    [SerializeField] bool everyoneHatesYou;
    [SerializeField] bool repPerfect, repGreat, repOkay, repMeh, repBad;

    public bool reputationClipBoard;

    int index;

    [SerializeField] List<GameObject> heartFX;
    public List<int> hearts;
    private int heartCount;

    private AudioSource heartAudio;

    public int deathCount;
    [SerializeField] DialogueCharacter jolene;
    [SerializeField] DialogueCharacter ishizu;
    [SerializeField] DialogueCharacter zeke;
    [SerializeField] CharacterReputation joleneRep;
    [SerializeField] CharacterReputation achillesRep;
    [SerializeField] CharacterReputation ishizuRep;
    [SerializeField] CharacterReputation zekeRep;
    [SerializeField] CharacterReputation tahmasRep;

    CharacterSystem cs;

    public bool reviewInProgress;
    public int EODHelper;

    public static ReviewManager instance;   
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        heartAudio = UIVisualizer.instance.heartAudio;
        cs = CharacterSystem.instance;
        goldClipBoard = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //TestingCharacterGoldReview();
            //StartReview();

        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            //HeartDisplay();
            //StartCoroutine(ClipBoardHearts());
        }
    }

    public void GetReview()
    {
        //CallCharacterAccuracy();
    }

    public void StartReview()
    {
        //REP STUFF
        CheckForDeaths();//important to keep above getcharacterreputations
        GetCharacterReputations();//rep calculations

        //GOLD STUFF
        CallCharacterAccuracy();//start of gold calculations

        //should be ready for dialogue functions now
        //reviewInProgress = true;
    }

    public void ReviewInProgressCheck()
    {

        if(reviewInProgress)
        {
            reviewInProgress = false;
        }
        else
        {
            reviewInProgress = true;
        }
    }

    public void ReviewInProgressCheckP1()
    {
        if (deathCount > 0) //moves to death thing
        {
            //do nothing, moves on to death dialogue, which does its own reeview check
        }
        else //will skip death dialogue
        {
            reviewInProgress = false;
        }
    }
    //check this first before calculating rep stuff (sets up characters to be removed)
    public void CheckForDeaths()
    {


        //Jolene
        if (jolene.choseQuestB)
        {
            if(AndyScript.instance.andyMomVisited)
            {
                //saved
                //characterRep.Add(joleneRep);
            }
            else
            {
                //died
                deathCount++;
                //remove josie from character rep calculation
                joleneRep.CharacterDie();
                RemoveClipBoardIcon(ReviewClipboard.instance.clipBoardIcons[14]); //jolene = 0
            }
        }

        //Ishizu
        if (ishizu.choseItemB)
        {
            //dead
            deathCount++;
            ishizuRep.CharacterDie();
            RemoveClipBoardIcon(ReviewClipboard.instance.clipBoardIcons[10]); //ishizu = 2
        }
        else
        {
            //characterRep.Add(ishizuRep);
        }

        if (!TahmasScript.instance.tahmasMet)
        {
            tahmasRep.CharacterDie();
            RemoveClipBoardIcon(ReviewClipboard.instance.clipBoardIcons[5]);
        }

        //Achilles
        if (!AchillesScript.instance.achillesCoinGiven)
        {
            //achilles dead
            deathCount++;
            //remove achilles from character rep calculation
            achillesRep.CharacterDie();
            RemoveClipBoardIcon(ReviewClipboard.instance.clipBoardIcons[3]); //achilles = 1
        }
        else
        {
            //characterRep.Add(achillesRep);
        }



        //zeke
        if (zeke.choseItemAA)
        {
            //dead
            //deathCount++;
            //zekeRep.CharacterDie();
            //RemoveClipBoardIcon(ReviewClipboard.instance.clipBoardIcons[7]); //zeke = 3
        }
        else
        {
            //characterRep.Add(zekeRep);
        }
    }

    public void RemoveCharacter(CharacterReputation character)
    {
        characterRep.Remove(character);
    }

    public void RemoveClipBoardIcon(GameObject icon)
    {
        icon.SetActive(false);
        ReviewClipboard.instance.clipBoardIcons.Remove(icon);
    }
    public void GetCharacterReputations()
    {
        likes = 0;  // Reset count
        foreach (CharacterReputation c in characterRep)
        {
            if(c.ReputationPoints >= baseRep)
            {
                likes++;
                //ReviewClipboard.instance.TurnOnHeart(index);
                //get reputation script, get a gameobject set in each one that corresponds with the clipboard icons
                //find the object and set the red heart to active
            }
            else
            {
                //find the object and set the broken heart to active
            }
        }

        repPercentageDif = likes / (float)characterRep.Count;

        characterCount = characterRep.Count;
    }

    public IEnumerator ClipBoardHearts()
    {
        foreach (CharacterReputation c in characterRep)
        {
            if (c.ReputationPoints >= baseRep)
            {
                Debug.Log("giev heart");
                ReviewClipboard.instance.TurnOnHeart(index);
            }
            else
            {
                Debug.Log("dont give heart");
                ReviewClipboard.instance.TurnOffHeart(index);
                //find the object and set the broken heart to active
            }
            index++;
            yield return new WaitForSeconds(0.75f);
        }

        reviewInProgress = true;
        ReviewDialogueReputationResults();
        
    }

    public void CharacterReputationDialogue()
    {
        //all dialogue should end with ReviewConclusionDialogue
        if(Mathf.Approximately(repPercentageDif, 1))
        {
            cs.rPerfect.StartNewDialogue(cs.dialogueTriggerScript);
            //perfect
            repPerfect = true;
        }
        else if(Mathf.Approximately(repPercentageDif, 0)) 
        {
            cs.rBad.StartNewDialogue(cs.dialogueTriggerScript);
            //all hate
            repBad = true;
        }
        else if(repPercentageDif >= 0.7f)
        {
            cs.rGreat.StartNewDialogue(cs.dialogueTriggerScript);
            //great
            repGreat = true;
        }
        else if(repPercentageDif >= 0.50f)
        {
            cs.rOkay.StartNewDialogue(cs.dialogueTriggerScript);
            //okay
            repOkay = true;
        }
        else if(repPercentageDif >= 0.45f)
        {
            cs.rMeh.StartNewDialogue(cs.dialogueTriggerScript);
            //meh
            repMeh = true;
        }
        else
        {
            cs.rBad.StartNewDialogue(cs.dialogueTriggerScript);
            //bad
            repBad = true;
        }
    }

    public void CallCharacterAccuracy()
    {
        AndyScript.instance.AndyGold();//
        NomiraScript.instance.NomiraGold();//
        ZetoScript.instance.ZetoGold();//
        AchillesScript.instance.AchillesGold();//
        TahmasScript.instance.TahmasGold();//
        LotestScript.instance.LotestGold();//
        FinchScript.instance.FinchGold();//
        GregScript.instance.GregGold();//
        KalinScript.instance.KalinGold();//
        VanelleScript.instance.VanelleGold();//
        MaggieScript.instance.MaggieGold();//
        JoleneScript.instance.JoleneGold();//

        CalculateGoldReview();

        //TestReviewDialogueNew();
    }

    public void CharacterGoldAccuracyCalculator(int goldGiven, int goldRequested)
    {
        //if (goldRequested == 0)
        //{
        //    characterGoldAccuracy.Add(0f);
        //    return;
        //}

        float goldPercentageDifference = (goldGiven -  goldRequested) / (float)goldRequested;
        characterGoldAccuracy.Add(goldPercentageDifference);

        totalRequestedGold += goldRequested;
        totalGoldGiven += goldGiven;

        Debug.Log("Requested: " + totalRequestedGold);
        Debug.Log("Given: " + totalGoldGiven);
    }

    public void CalculateGoldReview()
    {
        Debug.Log("Gold Fountain Ready");

        CoinFountain.instance.goldGiven = totalGoldGiven;
        CoinFountain.instance.goldRequestedText.text = totalRequestedGold.ToString();

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
            cs.gPerfect.StartNewDialogue(cs.dialogueTriggerScript);
            //ReviewBar.instance.AddReviewPoints(0.3f);
            // Perfect dialogue
            Debug.Log("Perfect dialogue");
            
            goldPerfect = true;
            return;
        }

        if (percentDiff > 0) // Gave more
        {
            if (percentDiff < 0.05f)
            {
                cs.gVeryClose.StartNewDialogue(cs.dialogueTriggerScript);
                // very close dialogue
                //ReviewBar.instance.AddReviewPoints(0.2f);
                goldVeryClose = true;
                Debug.Log("very close dialogue");
            }
            else if (percentDiff < 0.15f)
            {
                cs.gPrettyClose.StartNewDialogue(cs.dialogueTriggerScript);
                // pretty close dialogue
                //ReviewBar.instance.AddReviewPoints(0.1f);
                goldPrettyClose = true;
                Debug.Log("pretty close dialogue");
            }
            else if (percentDiff < 0.3f)
            {
                cs.gOffMore.StartNewDialogue(cs.dialogueTriggerScript);
                //ReviewBar.instance.AddReviewPoints(-0.1f);
                // off more dialogue
                goldOffMore = true;
                Debug.Log("off more dialogue");
            }
            else
            {
                cs.gWayOffMore.StartNewDialogue(cs.dialogueTriggerScript);
                //ReviewBar.instance.AddReviewPoints(-0.2f);
                // way too much dialogue
                goldWayOffMore = true;
                Debug.Log("way too much dialogue");
            }
        }
        else // Gave less
        {
            // Make the percentage difference positive for easier comparisons
            percentDiff = Mathf.Abs(percentDiff);

            if (percentDiff < 0.05f)
            {
                cs.gVeryClose.StartNewDialogue(cs.dialogueTriggerScript);
                //ReviewBar.instance.AddReviewPoints(0.2f);
                // very close dialogue (slightly under)
                goldVeryClose = true;
                Debug.Log("very close dialogue");
            }
            else if (percentDiff < 0.15f)
            {
                cs.gPrettyClose.StartNewDialogue(cs.dialogueTriggerScript);
                //ReviewBar.instance.AddReviewPoints(0.1f);
                // pretty close dialogue (a bit under)
                goldPrettyClose = true;
                Debug.Log("pretty close dialogue");
            }
            else if (percentDiff < 0.3f)
            {
                cs.gOffLess.StartNewDialogue(cs.dialogueTriggerScript);
                //ReviewBar.instance.AddReviewPoints(-0.1f);
                // off less dialogue (noticeably under)
                goldOffLess = true;
                Debug.Log("off less dialogue");
            }
            else
            {
                cs.gWayOffLess.StartNewDialogue(cs.dialogueTriggerScript);
                //ReviewBar.instance.AddReviewPoints(-0.2f);
                // way too little dialogue
                goldWayOffLess = true;
                Debug.Log("way too little dialogue");
            }
        }
    }

    public void GoldTransition()
    {
        if(goldPerfect || goldVeryClose || goldPrettyClose)
        {
            cs.gTransGood.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (goldOffLess || goldWayOffLess)
        {
            cs.gTransLess.StartNewDialogue(cs.dialogueTriggerScript);
            //less
        }
        else if(goldOffMore || goldWayOffMore)
        {
            cs.gTransMore.StartNewDialogue(cs.dialogueTriggerScript);
            //more
        }
    }

    public void ReviewDialogueReputationDeath()
    {
        if(deathCount > 0)
        {
            //say death dialogue which should end with reviewdialoguereputationresults function
            cs.rDeathRes.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else
        {
            //say rep result dialogue
            //ReviewDialogueReputationResults(); //OLD
            //ReviewInProgressCheck();
            //NEW
            ClipBoardFunction();
        }
    }

    public void ClipBoardFunction()
    {
        StartCoroutine(ClipBoardHearts());
    }

    public void ReviewDialogueReputationResults()
    {
        if (Mathf.Approximately(repPercentageDif, 1))
        {
            //literally everyone likes you
            everyoneLovesYou = true;
        }
        else if (Mathf.Approximately(repPercentageDif, 0))
        {
            //litterally no one likes you
            everyoneHatesYou = true;
        }

        if (everyoneLovesYou)
        {
            cs.rPerfectRes.StartNewDialogue(cs.dialogueTriggerScript);
            //all love you dialogue
        }
        else if(everyoneHatesYou)
        {
            cs.rBadRes.StartNewDialogue(cs.dialogueTriggerScript);
            //all hate you dialogue
        }
        else if(likes > 7)
        {
            cs.rGoodRes.StartNewDialogue(cs.dialogueTriggerScript);
            //regular x adventurers like you dialogue
        }
        else
        {
            if(likes > 1)
            {
                cs.rMehRes.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else
            {
                cs.rMehResSingle.StartNewDialogue(cs.dialogueTriggerScript);
            }
                
        }

        //each dialgoue end should go to characterreputationdialogue
    }

    public void ReviewConclusionDialogue()
    {
        reviewInProgress = false;

        //see chart
        if (goldPerfect)
        {
            if (repPerfect)
            {
                //green
                cs.greenConclusion.StartNewDialogue(cs.dialogueTriggerScript);
            }
            if(repGreat)
            {
                //green
                cs.greenConclusion.StartNewDialogue(cs.dialogueTriggerScript);
            }
            if(repOkay)
            {
                //green
                cs.greenConclusion.StartNewDialogue(cs.dialogueTriggerScript);
            }
            if (repMeh)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repBad)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
        }

        if (goldVeryClose)
        {
            if (repPerfect)
            {
                //green
                cs.greenConclusion.StartNewDialogue(cs.dialogueTriggerScript);
            }
            if (repGreat)
            {
                //green
                cs.greenConclusion.StartNewDialogue(cs.dialogueTriggerScript);
            }
            if (repOkay)
            {
                //green
                cs.greenConclusion.StartNewDialogue(cs.dialogueTriggerScript);
            }
            if (repMeh)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repBad)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
        }

        if (goldPrettyClose)
        {
            if (repPerfect)
            {
                //green
                cs.greenConclusion.StartNewDialogue(cs.dialogueTriggerScript);
            }
            if (repGreat)
            {
                //green
                cs.greenConclusion.StartNewDialogue(cs.dialogueTriggerScript);
            }
            if (repOkay)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repMeh)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repBad)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
        }

        if (goldOffLess)
        {
            if (repPerfect)
            {
                //green
                cs.greenConclusion.StartNewDialogue(cs.dialogueTriggerScript);
            }
            if (repGreat)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repOkay)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repMeh)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
            if (repBad)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
        }

        if (goldOffMore)
        {
            if (repPerfect)
            {
                //green
                cs.greenConclusion.StartNewDialogue(cs.dialogueTriggerScript);
            }
            if (repGreat)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repOkay)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repMeh)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
            if (repBad)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
        }

        if (goldWayOffLess)
        {
            if (repPerfect)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repGreat)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repOkay)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
            if (repMeh)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
            if (repBad)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
        }

        if (goldWayOffMore)
        {
            if (repPerfect)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repGreat)
            {
                //orange
                cs.orangeConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 1;
            }
            if (repOkay)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
            if (repMeh)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
            if (repBad)
            {
                //red
                cs.redConclusion.StartNewDialogue(cs.dialogueTriggerScript);
                EODHelper = 2;
            }
        }
    }


    public void GoldDialogueResults()
    {
        StartCoroutine(WaitAFrame(TestReviewDialogueNew));
    }

    public void GoldTransitionFrameSkip()
    {
        StartCoroutine(WaitAFrame(GoldTransition));
    }

    public void ReputationP1()
    {
        cs.rP1.StartNewDialogue(cs.dialogueTriggerScript);
    }
    public void ReputationP1FrameSkip()
    {
        StartCoroutine(WaitAFrame(ReputationP1));
    }

    public void ReviewDialogueRepDeathFrameSkip()
    {
        StartCoroutine(WaitAFrame(ReviewDialogueReputationDeath));
    }

    public void ReviewDialogueRepResultsFrameSkip()
    {
        StartCoroutine(WaitAFrame(ReviewDialogueReputationResults));
    }

    public void CharacterReputationDialogueFrameSkip()
    {
        StartCoroutine(WaitAFrame(CharacterReputationDialogue));
    }

    public void ReviewConclusionFrameSkip()
    {
        StartCoroutine(WaitAFrame(ReviewConclusionDialogue));
    }

    IEnumerator WaitAFrame(Action act)
    {
        yield return null;

        act();
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
