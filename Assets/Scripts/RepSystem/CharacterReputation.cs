// This script is used to store and manage a character's reputation points.
// Other parts of the game (like dialogue or UI) can check this score or update it when needed.
// Put this script on every character that you want to have a reputation score.
// CALL 'ReputationPoints' TO GET THE CURRENT SCORE

using UnityEngine;
using UnityEngine.Events;   // This is for the Unity Event system. Makes it easy to make events that can be automatically triggered if certain conditions are met.

public class CharacterReputation : MonoBehaviour
{
    // This is the range for each character's reputation points. It can only go from -5 to 5.
    private const int MIN_REPUTATION = -5;   // The minimum reputation points a character can have.
    private const int MAX_REPUTATION = 5;    // The maximum reputation points a character can have.

    // These two lines makes a private variable to store the reputation points.
    // 'private' means this variable can only be changed or used by this script.
    // Tooltip attribute shows the note in the quotes when hovering over the variable in the inspector.
    [Tooltip("Reputation score of the character. This can be used for the end of day player reputation bar calculation and maybe determine how NPCs react to the character?")]
    [SerializeField] private int _reputationPoints = 0;     // Start the character with 0 reputation points at the beginning of the game.

    // 'public' just gives a way for other scripts to use this variable without being able to change it directly.
    // The '=>' is a shorthand way to write a simple function that returns a value.
    public int ReputationPoints => _reputationPoints;    // when you call for ReputationPoints, it returns _reputationPoints.

    [Tooltip("Triggered at any point when the reputation changes. The int parameter is the new reputation score.")]
    public UnityEvent<int> onReputationChanged;

    // This function can be called to change the reputation by any amount.
    // The 'int amount' parameter is the amount to change the reputation by. It can be positive or negative.
    public void ModifyReputation(int amount) {
        // Increase or decrease the _reputationPoints by 'amount'
        _reputationPoints += amount;

        // Clamp it so it stays in the range of -5 to 5.
        _reputationPoints = Mathf.Clamp(_reputationPoints, MIN_REPUTATION, MAX_REPUTATION);

        // This triggers the "onReputationChanged" event, telling other parts of the game that the reputation has been changed.
        onReputationChanged?.Invoke(_reputationPoints);
    }

    // This function is used to add reputation points.
    // It makes sure the number added is always positive.
    public void AddReputation(int amount) => ModifyReputation(Mathf.Abs(amount));

    // This function is used to remove reputation points.
    // It makes sure the number subtracted is always negative.
    public void RemoveReputation(int amount) => ModifyReputation(-Mathf.Abs(amount));
}
