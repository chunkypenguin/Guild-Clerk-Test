// This script tracks player actions and changes the character's reputation points based on those actions.
// This can be expanded later for dialogue options, quests, and other game mechanics.
// Right now:
// - Pressing 'backspace' key will increase target character's reputation by 1.
// - Pressing 'delete' key will decrease target character's reputation by 1.
// - Pressing 'up arrow' key will switch to the next character in the list.
// - Pressing 'down arrow' key will switch to the previous character in the list.
// - Pressing 'space' key will show the end of day reputation visual.
/// MAKE SURE YOU ASSIGN NPCS TO THE LIST IN THE INSPECTOR!

using UnityEngine;
using System.Collections.Generic;   // This is for the List<T> class. It allows you to create a list of objects that can be changed at runtime.

public class PlayerRepTrackerCharacter : MonoBehaviour {
    // This tells you which NPC in the list you are working with. 0 = first NPC, 1 = second NPC, etc.
    [Tooltip("Which NPC are you currently talking to?")]
    [SerializeField] private int _currentNPCIndex = 0;

    // When you call _currentTarget, it checks:
    // - Is the allNpcs list not empty?
    // - Is the _currentNPCIndex a valid index in the list?
    // If both are true, then it returns the NPC at that index.
    // If not, it returns null.
    // This part just safely gives you the current NPC when you call _currentTarget.
    //private CharacterReputation _currentTarget =>
    //npcObjects.Count == 0 ? null :
    //npcObjects[CharacterSystem.instance.characterCount].GetComponent<CharacterReputation>();


    private CharacterReputation _currentTarget;
    // This part is just for testing visuals. This just holds the NPC objects in the scene to switch the characters.
    [Header("NPC Visuals")]
    [SerializeField] private List<GameObject> npcObjects = new List<GameObject>();

    [Header("UI")]
    // This is a reference to the UIVisualizer script. This just changes the visuals of the UI.
    [SerializeField] private UIVisualizer _reputationVisualizer;

    // This section is just for testing purposes. You can change how the player interacts with the characters later.
    [Header("Keybinds for testing")]
    // This part just sets keys to increase or decrease the reputation points.
    // 'backspace' for increase and 'delete' for decrease.
    [SerializeField] private KeyCode increaseRepKey = KeyCode.Backspace;
    [SerializeField] private KeyCode decreaseRepKey = KeyCode.Delete;
    // This part sets the keys to switch between different characters. You can change this logic on how the player knows who they're talking to later if you want!
    [SerializeField] private KeyCode nextNPC = KeyCode.UpArrow;
    [SerializeField] private KeyCode previousNPC = KeyCode.DownArrow;
    // This part sets the keys to show the end of day rep visual.
    [SerializeField] private KeyCode showEodRepVisual = KeyCode.Space;
    // This part clears the list of characters who have visited for the day.
    [SerializeField] private KeyCode clearList = KeyCode.C;


    public static PlayerRepTrackerCharacter instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start() {
        ActivateNpc(0);
    }

    // Update is called once per frame
    void Update() {
        _currentTarget = CharacterSystem.instance.characterObjects[CharacterSystem.instance.characterCount].GetComponent<CharacterReputation>();

        // Make sure you assign the characters in the inspector
        if (_currentTarget == null) {
            Debug.LogWarning("There isn't a character at this index in the list in the RepSystemManager in the inspector!");
            return;
        }

        // If the player presses the 'up arrow' key, go to the next character in the list.
        if (Input.GetKeyDown(nextNPC)) {
            CycleNpc(1);
        }

        // If the player presses the 'down arrow' key, go to the previous character in the list.
        if (Input.GetKeyDown(previousNPC)) {
            CycleNpc(-1);
        }

        // If the player does something good (for now just pressing 'backspace' key), increase the target character's reputation points.
        if (Input.GetKeyDown(increaseRepKey) && _currentTarget != null) {
            // Increase their rep points by 1
            _currentTarget.AddReputation(1);

            // Print the new reputation points to the console
            Debug.Log($"Increased {_currentTarget.name}'s reputation to {_currentTarget.ReputationPoints}");
        }

        // If the player does something bad (for now just pressing 'delete' key), decrease the target character's reputation points.
        if (Input.GetKeyDown(decreaseRepKey) && _currentTarget != null) {
            // Decrease their rep points by 1
            _currentTarget.RemoveReputation(1);

            // Print the new reputation points to the console
            Debug.Log($"Decreased {_currentTarget.name}'s reputation to {_currentTarget.ReputationPoints}");
        }

        // When the player gets to the end of the day (for now just pressing 'space' key), show the end of day reputation visual.
        if (Input.GetKeyDown(showEodRepVisual)) {
            //_reputationVisualizer.ShowEndOfDay();
        }

        // When a new day starts, clear the list (for now just pressing 'C' key) of characters the player has interacted with.
        if (Input.GetKeyDown(clearList)) {
            DayReputationTracker.Instance.NewDayClearList();
            Debug.Log("Cleared the list of characters visited today.");
        }
    }

    private void CycleNpc(int direction) {
        int next = (_currentNPCIndex + direction + npcObjects.Count) % npcObjects.Count;
        ActivateNpc(next);
    }

    public void ActivateNpc(int index) {
        if (npcObjects.Count == 0) {
            Debug.LogWarning("There are no NPCs in the list!");
            return;
        }

        // Hide old npc and show new npc
        //npcObjects[_currentNPCIndex].SetActive(false);
        //_currentNPCIndex = index;
        //npcObjects[_currentNPCIndex].SetActive(true);



        _currentTarget = CharacterSystem.instance.characterObjects[index].GetComponent<CharacterReputation>();

        // Tell visualizer which heart bar to show
        _reputationVisualizer.SetCurrentTarget(_currentTarget);

        // Register the current target with the DayReputationTracker
        DayReputationTracker.Instance.RegisterNpc(_currentTarget);
    }
}