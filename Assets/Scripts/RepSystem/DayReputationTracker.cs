// This script remembers every NPC the player interacts with in the current day and puts them in a list.
// Once the day is finished, the EODAverageRep function will calculate the average reputation points of all the characters in the list.
// You can also use the NewDayClearList function to clear the list when a new day starts.

using UnityEngine;
using System.Collections.Generic;

public class DayReputationTracker : MonoBehaviour
{
    // Make sure there's one instance so that other scripts can say DayReputationTracker.Instance
    // for whatever they need.
    public static DayReputationTracker Instance { get; private set; }

    // This is a reference to the UIVisualizer script. This just changes the visuals of the UI.
    [SerializeField] private UIVisualizer _reputationVisualizer;

    // This is the list of all the characters the player interacts with in the current day.
    [SerializeField]private List<CharacterReputation> _visitedToday = new List<CharacterReputation>();

    public int _playerGold = 0;

    private void Awake() {
        // This makes sure there's only one instance of this script.
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // This line makes sure this object doesn't get destroyed when changing scenes.
        // Don't use this if you want to reset the list when changing scenes.
        DontDestroyOnLoad(gameObject);
    }

    // This function is called the first time you interact with a character in the day.
    public void RegisterNpc(CharacterReputation npc) {
        // Check if the npc is null or if the _visitedToday list already contains the npc.
        if (npc == null || _visitedToday.Contains(npc)) {
            return;
        }
        // Add the npc to the list of visited characters.
        _visitedToday.Add(npc);
    }

    // Reference NpcCountToday to get the number of characters the player has interacted with today safely.
    public int NpcCountToday => _visitedToday.Count;

    // This is the calculation for the average reputation points of all the characters the player has interacted with today.
    // EX: 2, -1, 0 => (2 + -1 + 0) / 3 = 0.33 => rounded to next .5 = 0.5
    public float EODAverageRep() {
        if (NpcCountToday == 0) {
            Debug.Log("No npcs interacted with today");
            return 0f;
        }

        // Start with 0 total reputation points
        int totalRep = 0;

        // Add up the reputation points for each NPC
        foreach (CharacterReputation rep in _visitedToday) {
            totalRep += rep.ReputationPoints;
        }

        // Calculate the average reputation points
        float average = (float)totalRep / NpcCountToday;
        // Round to the nearest positive half
        return Mathf.Ceil(average * 2f) / 2f;
    }

    // Call this at the end of the day. Increases the player's gold based on the reputation.
    public void GiveCoinsForReputation() {
        float rep = EODAverageRep();
        int coinsToAdd = Mathf.RoundToInt(rep * 10f); // 5 coins for 0.5 rep

        //DAILY WAGE
        //coinsToAdd += 10;

        AddCoins(coinsToAdd);
        _reputationVisualizer.UpdateCoinUI(coinsToAdd);
        Debug.Log($"EOD Rep: {rep}, Coins Added: {coinsToAdd}");
    }

    // This function adds coins to the player and updates the coins UI.
    public void AddCoins(int amount) {
        _playerGold += amount + 10; //rep amount plus daily wage

        //CHECK IF PLAYER KEEPS JOSIES GOLD BUNDLE, MAKE SURE TO SET GOLD BUNDLE TO SET ACTIVE FALSE AFTER
        TutorialScript.instance.GoldCheck();
        if (TutorialScript.instance.hasGoldBundle)
        {
            _playerGold += 25;
            Debug.Log("add25gold");
        }

        _reputationVisualizer.TotalUpdateCoinUI();
    }

    // Returns how much gold the player has
    public int GetGold() {
        return _playerGold;
    }

    // Deducts gold and updates UI (for guild rep)
    public bool SpendGold(int amount) {
        if (_playerGold < amount) {
            return false;
        }

        _playerGold -= amount;

        _reputationVisualizer.TotalUpdateCoinUI();

        return true;
    }

    // Clear the list when the new day begins
    public void NewDayClearList() {
        _visitedToday.Clear();
    }
}
