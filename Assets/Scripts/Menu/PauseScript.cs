using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseScript : MonoBehaviour
{
    bool paused;

    [SerializeField] GameObject pauseOverlay;
    [SerializeField] TMP_Text dayText;
    [SerializeField] TMP_Text _totalCoinText;

    [SerializeField] AudioSource textAudioSource;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressPause();
        }
    }

    public void PressPause()
    {
        if (!paused)
        {
            paused = true;
            Paused();
            Time.timeScale = 0f;
        }
        else
        {
            paused = false;
            Time.timeScale = 1f;
            Unpaused();
        }
    }

    private void Paused()
    {
        pauseOverlay.SetActive(true);
        dayText.text = "Day " + DaySystem.instance.dayCount.ToString();
        GetCoinsTotal();
        textAudioSource.volume = 0f;
    }
    private void Unpaused()
    {
        pauseOverlay.SetActive(false);
        textAudioSource.volume = 0.5f;
    }

    void GetCoinsTotal()
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

        // Update the text to show the new amount of coins
        int coinAmount = DayReputationTracker.Instance.GetGold();
        coinTextComponent.text = coinAmount.ToString();
    }
}
