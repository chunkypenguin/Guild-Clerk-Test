// This script is used to visualize the reputation of NPCs in the game as a row of hearts.
// It also supports showing the end-of-day reputation average.

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIVisualizer : MonoBehaviour {
    // Assign NPCs in the inspector
    [Tooltip("The list of all NPCs reps to visualize")]
    [SerializeField] private List<CharacterReputation> trackedReps = new List<CharacterReputation>();

    // Should be already assigned in the prefab
    [Tooltip("Holds the character heart slots in the canvas")]
    [SerializeField] private Transform[] charHeartSlots;

    // Should also be assigned in prefab
    [Tooltip("Holds the guild heart slots in the canvas")]
    [SerializeField] private Transform[] guildStarSlots;

    // Should be already assigned in the prefab
    // This is for the floating heart effect that shows to the right of the NPC when they gain or lose a heart
    [Header("Floating Heart FX")]
    [SerializeField] private GameObject positiveHeartFX;
    [SerializeField] private GameObject negativeHeartFX;

    // This is the previous reputation value. Used to determine if the heart FX should play.
    private int previousRep = 0;

    // Don't need to assign this in the inspector. This is the current target NPC.
    [Tooltip("The current NPC you are talking to")]
    private CharacterReputation _currentTarget;

    [Header("End of Day Settings")]
    // If user presses space, this will show the end of day reputation.
    [Tooltip("Display the end of day visual instead of the current NPC's rep when true.")]
    [SerializeField] private bool _showEodVisual = false;

    // Should be assigned in prefab already
    [Tooltip("This GameObject holds the 'EOD Mode' text in the UI Canvas.")]
    [SerializeField] private GameObject eodModeText;

    [Tooltip("Check this on if you don't want to show the heart visual for each character")]
    [SerializeField] private bool _disableHeartVisual = false;

    [Tooltip("Drag the 'CoinText' GameObject here")]
    [SerializeField] private GameObject _coinText;

    [SerializeField] private GameObject _newCoinText;
    [SerializeField] private GameObject _totalCoinText;
    [SerializeField] private TMP_Text dailyTotalText;

    [SerializeField] AudioSource heartAudio;

    // Unity automatically calls Awake when the game starts. This is called before Start().
    // I use this to set up the initial state of the script.
    private void Awake() {
        // Make sure you assign the NPCs in the 'ReputationVisual' in the inspector
        if (trackedReps == null || trackedReps.Count == 0) {
            Debug.LogError("UIVisualizer: No tracked reputations assigned.");
            return;
        }

        // Make sure all NPCs assigned in the trackedReps list are not null and have the CharacterReputation script
        foreach (var rep in trackedReps) {
            if (rep == null) {
                Debug.LogError("UIVisualizer: One of the tracked reputations is null.");
                continue;
            }

            // Automatically calls the UpdateCharRepVisual function when the reputation changes
            rep.onReputationChanged.AddListener((int repValue, int gainedRep) => UpdateCharRepVisual(repValue, gainedRep, true));
        }

        // If you checked to disable the heart visual for NPCs, this will set all of the heart slots to inactive at the start of the game
        if (_disableHeartVisual) {
            ShowHeartVisual(false);
        }
    }

    // This function just shows or hides the heart visual in the UI.
    public void ShowHeartVisual(bool showVisual) {
        if (showVisual) {
            // Enable all of the heart slots in the canvas
            foreach (var slot in charHeartSlots) {
                slot.gameObject.SetActive(true);
            }
        }
        else {
            // Disable all of the heart slots in the canvas
            foreach (var slot in charHeartSlots) {
                slot.gameObject.SetActive(false);
            }
        }
    }

    // This updates the UI heart visuals for each NPC's rep.
    // You can turn this off if you want by checking Disable Heart Visual in the inspector.
    public void UpdateCharRepVisual(int currentRep, int gainedRep, bool playFX = true) {
        // Play the heart FX when NPC's rep changes
        if (playFX) {

            if(gainedRep != 0)
            {
                //PlayHeartFX(currentRep);
                PlayHeartFX(gainedRep);
            }

        }

        // If the heart visual is disabled, return.
        if (_disableHeartVisual) {
            return;
        }

        // If the end of day visual is showing, don't update per-NPC visuals
        if (_showEodVisual) return;

        // absRep is the absolute value of the current reputation points.
        int absRep = Mathf.Abs(currentRep);
        bool isPositive = currentRep > 0;
        bool isNegative = currentRep < 0;

        // This for loop goes through each heart slot and sets the visuals based on the current reputation points.
        for (int i = 0; i < charHeartSlots.Length; i++) {
            // Get the current heart slot
            Transform slot = charHeartSlots[i];

            // This is the logic for showing the heart visuals
            bool showPositive = isPositive && i < absRep;
            bool showNegative = isNegative && i < absRep;
            bool showEmpty = !showPositive && !showNegative;

            // If showEmpty is true, show the empty hearts for slot i
            slot.GetChild(0).gameObject.SetActive(showEmpty);   // Empty 1st half
            slot.GetChild(1).gameObject.SetActive(showEmpty);   // Empty 2nd half

            // If showPositive is true, show the positive hearts for slot i
            slot.GetChild(2).gameObject.SetActive(showPositive); // Positive 1st half
            slot.GetChild(3).gameObject.SetActive(showPositive); // Positive 2nd half

            // If showNegative is true, show the negative hearts for slot i
            slot.GetChild(4).gameObject.SetActive(showNegative); // Negative 1st half
            slot.GetChild(5).gameObject.SetActive(showNegative); // Negative 2nd half
        }

        // Store the current value for theFX
        previousRep = currentRep;
    }

    // This function plays the heart FX when the reputation changes.
    private void PlayHeartFX(int currentRep) {
        if (currentRep > 0) { // used to be currentRep > previousRep
            if (positiveHeartFX == null) {
                Debug.LogError("UIVisualizer: No positive heart FX assigned.");
            }

            positiveHeartFX.SetActive(true);

            if (heartAudio != null)
            {
                heartAudio.pitch = 1.2f;
                heartAudio.Play();
            }

        }
        else if (currentRep < 0) {
            if (negativeHeartFX == null) {
                Debug.LogError("UIVisualizer: No positive heart FX assigned.");
            }

            negativeHeartFX.SetActive(true);

            if (heartAudio != null)
            {
                heartAudio.pitch = 0.5f;
                heartAudio.Play();
            }
        }
    }

    // This updates visuals for the EOD reputation.
    // This is similar to UpdateCharRepVisual, but it uses a float value instead of an int.
    private void UpdateEodHeartVisuals(float value) {
        // Set visual mode to EOD
        _showEodVisual = true;
        // Show the EOD mode text
        eodModeText.SetActive(true);
        // Show the hearts visual
        ShowHeartVisual(true);

        bool isPositive = value >= 0;
        float absValue = Mathf.Abs(value);

        // This for loop goes through each heart slot and sets the visuals based on the current reputation points.
        for (int i = 0; i < charHeartSlots.Length; i++) {
            Transform slot = charHeartSlots[i];

            bool showFull = absValue >= i + 1f;
            bool showHalf = !showFull && absValue >= i + 0.5f;
            //bool showEmpty = !showFull && !showHalf;

            // Similar to UpdateCharRepVisual
            //slot.GetChild(0).gameObject.SetActive(showEmpty); // Empty1
            //slot.GetChild(1).gameObject.SetActive(showEmpty); // Empty2

            slot.GetChild(2).gameObject.SetActive(isPositive && showFull); // Pos1
            slot.GetChild(3).gameObject.SetActive(isPositive && showFull); // Pos2

            slot.GetChild(4).gameObject.SetActive(!isPositive && showFull); // Neg1
            slot.GetChild(5).gameObject.SetActive(!isPositive && showFull); // Neg2

            // This is for the half hearts. If there is a half heart, show the empty heart on the right side.
            if (showHalf) {
                if (isPositive) {
                    slot.GetChild(2).gameObject.SetActive(true); // Pos1
                    //slot.GetChild(1).gameObject.SetActive(true); // Empty2
                }
                else {
                    slot.GetChild(4).gameObject.SetActive(true); // Neg1
                    //slot.GetChild(1).gameObject.SetActive(true); // Empty2
                }
            }
        }
    }

    // This function will update the visuals for the Guild Rep.
    public void UpdateGuildRepVisual(float rep) {
        if (guildStarSlots == null || guildStarSlots.Length == 0) {
            Debug.LogWarning("Guild star slots not assigned");
            return;
        }

        // Clamp the rep between -5 to 5
        float value = Mathf.Clamp(rep, -5f, 5f);
        bool isPositive = value >= 0;
        float absValue = Mathf.Abs(value);

        for (int i = 0; i < guildStarSlots.Length; i++) {
            Transform slot = guildStarSlots[i];

            bool showFull = absValue >= i + 1f;
            bool showHalf = !showFull && absValue >= i + 0.5f;
            bool showEmpty = !showFull && !showHalf;

            // Empty Star (both halves)
            slot.GetChild(0).gameObject.SetActive(showEmpty);
            slot.GetChild(1).gameObject.SetActive(showEmpty);

            // Full Positive or Negative
            slot.GetChild(2).gameObject.SetActive(isPositive && showFull);
            slot.GetChild(3).gameObject.SetActive(isPositive && showFull);
            slot.GetChild(4).gameObject.SetActive(!isPositive && showFull);
            slot.GetChild(5).gameObject.SetActive(!isPositive && showFull);

            // Half Star display (left filled, right empty)
            if (showHalf) {
                if (isPositive) {
                    slot.GetChild(2).gameObject.SetActive(true);  // Positive 1st half
                    slot.GetChild(1).gameObject.SetActive(true);  // Empty 2nd half
                }
                else {
                    slot.GetChild(4).gameObject.SetActive(true);  // Negative 1st half
                    slot.GetChild(1).gameObject.SetActive(true);  // Empty 2nd half
                }
            }
        }
    }

    // This function sets the current target for the reputation visualizer.
    public void SetCurrentTarget(CharacterReputation newTarget) {
        // If the heart visual is disabled, don't show it for new NPCs
        if (_disableHeartVisual) {
            ShowHeartVisual(false);
        }
        else {
            // Show the heart visual for the new NPC
            ShowHeartVisual(true);
        }

        // Sets the current target to the new target
        _currentTarget = newTarget;
        // Turns off the EOD mode
        _showEodVisual = false;
        eodModeText.SetActive(false);

        if (_currentTarget == null) {
            Debug.LogError("UIVisualizer: Current target is null.");
            return;
        }

        // This is for the Floating Heart FX
        previousRep = _currentTarget.ReputationPoints;
    }

    // This is the calculation for the end of day reputation.
    public void ShowEndOfDay() {
        // Make sure there was at least one NPC interacted with today
        if (DayReputationTracker.Instance.NpcCountToday == 0) {
            Debug.Log("No npcs interacted with yet");
            return;
        }

        DayReputationTracker.Instance.GiveCoinsForReputation();

        float averageRep = DayReputationTracker.Instance.EODAverageRep();

        // Show the hearts visual
        UpdateEodHeartVisuals(averageRep);
    }

    public void OldUpdateCoinUI() {

        if (_coinText == null) {
            Debug.LogWarning("CoinText is not assigned in the inspector.");
            return;
        }

        Transform coinNum = _coinText.transform.Find("CoinNum");
        if (coinNum == null) {
            Debug.LogWarning("CoinNum is not found in the CoinText GameObject.");
            return;
        }

        Text coinTextComponent = coinNum.GetComponent<Text>();
        if (coinTextComponent == null) {
            Debug.LogWarning("CoinNum does not have a Text component.");
            return;
        }

        // Update the text to show the new amount of coins
        int coinAmount = DayReputationTracker.Instance.GetGold();
        coinTextComponent.text = coinAmount.ToString();
    }

    public void UpdateCoinUI(int coins)
    {

        if (_newCoinText == null)
        {
            Debug.LogWarning("CoinText is not assigned in the inspector.");
            return;
        }

        Transform coinNum = _newCoinText.transform.Find("RepCoinNum");
        if (coinNum == null)
        {
            Debug.LogWarning("CoinNum is not found in the CoinText GameObject.");
            return;
        }

        TMP_Text coinTextComponent = coinNum.GetComponent<TMP_Text>();
        if (coinTextComponent == null)
        {
            Debug.LogWarning("CoinNum does not have a Text component.");
            return;
        }

        // Update the text to show the new amount of coins
        int coinAmount = coins;

        if (coinAmount <= 0)
        {
            coinAmount = 0;
        }
        coinTextComponent.text = "+ " + coinAmount.ToString();

        TutorialScript.instance.GoldCheck();
        if (TutorialScript.instance.hasGoldBundle)
        {
            coinAmount += 25;
            Debug.Log("add25gold");
        }
        dailyTotalText.text = (coinAmount + 10).ToString();
    }

    public void TotalUpdateCoinUI()
    {

        if (_totalCoinText == null)
        {
            Debug.LogWarning("CoinText is not assigned in the inspector.");
            return;
        }

        Transform coinNum = _totalCoinText.transform.Find("TotalCoinNum");
        if (coinNum == null)
        {
            Debug.LogWarning("CoinNum is not found in the CoinText GameObject.");
            return;
        }

        TMP_Text coinTextComponent = coinNum.GetComponent<TMP_Text>();
        if (coinTextComponent == null)
        {
            Debug.LogWarning("CoinNum does not have a Text component.");
            return;
        }

        // Update the text to show the total amount of coins
        int coinAmount = DayReputationTracker.Instance.GetGold();
        if(coinAmount <= 0)
        {
            coinAmount = 0;
        }
        coinTextComponent.text = coinAmount.ToString();
    }
}