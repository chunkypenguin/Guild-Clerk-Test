using UnityEngine;
using UnityEngine.UI;

public class PlayerRepTrackerGuild : MonoBehaviour {
    [Header("Daily Settings")]
    [SerializeField] private int _minGoldRequired = 10;
    [SerializeField] private int _maxGoldAllowed = 50;

    [Header("Guild Reputation (-5 to 5)")]
    [SerializeField] private float _guildRepStars = 2f;

    [Header("Gold Tracking")]
    private int goldGivenToday = 0;

    // This is a reference to the UIVisualizer script. This just changes the visuals of the UI.
    [SerializeField] private UIVisualizer _reputationVisualizer;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            GiveGold(10);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            EvalDayAndReset();
        }
    }

    public void GiveGold(int amount) {
        if (amount <= 0) {
            Debug.LogWarning("Amount must be greater than 0");
            return;
        }

        DayReputationTracker tracker = DayReputationTracker.Instance;
        if (tracker == null) {
            Debug.LogError("DayRepTracker Instance not found.");
            return;
        }

        if (tracker.GetGold() < amount) {
            Debug.Log("Not enough gold to give out");
            return;
        }

        tracker.SpendGold(amount);
        goldGivenToday += amount;
        Debug.Log($"Gave out {amount} gold. Total given today: {goldGivenToday}");
    }

    public void EvalDayAndReset() {
        Debug.Log($"EOD Evaluation: Min={_minGoldRequired}, Max={_maxGoldAllowed}, Given={goldGivenToday}");

        if (goldGivenToday < _minGoldRequired) {
            Debug.Log("Too stingy - lost 0.5 stars");
            AdjustGuildRep(-0.5f);
        }
        else if (goldGivenToday > _maxGoldAllowed) {
            int excess = goldGivenToday - _maxGoldAllowed;
            int penalty = Mathf.FloorToInt(excess / 10f);   // 1 star for every 10 excess gold
            float repPenalty = 0.5f * penalty;
            Debug.Log($"Too generous — lost {repPenalty} star(s).");
            AdjustGuildRep(-repPenalty);
        }
        else {
            Debug.Log("Gold given is within the acceptable range. Increasing rep.");
            AdjustGuildRep(0.5f);
        }

        // Reset for the next day
        goldGivenToday = 0;
    }

    public void AdjustGuildRep(float change) { 
        _guildRepStars = Mathf.Clamp(_guildRepStars + change, -5f, 5f);
        Debug.Log($"Guild Rep Stars: {_guildRepStars}");

        // This will update the visuals for the guild rep.
        _reputationVisualizer.UpdateGuildRepVisual(_guildRepStars);
    }

    public void SetGuildThresholds(int min, int max) {
        _minGoldRequired = min;
        _maxGoldAllowed = max;
        Debug.Log($"Guild thresholds set: Min={_minGoldRequired}, Max={_maxGoldAllowed}");
    }
}