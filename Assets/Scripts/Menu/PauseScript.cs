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
    [SerializeField] AudioSource pauseSoundSource;
    [SerializeField] AudioClip pauseClip;
    [SerializeField] AudioClip unpauseClip;

    // Update is called once per frame
    void Update()
    {
        //cannot pause if credits, recaps, or end of day screen is active
        if (Input.GetKeyDown(KeyCode.Escape) && !DaySystem.instance.gameEnd && !Recap.instance.recapOn && !DaySystem.instance.endOfDayCantPause)
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
        pauseSoundSource.PlayOneShot(pauseClip);
        pauseOverlay.SetActive(true);
        dayText.text = "Day " + DaySystem.instance.dayCount.ToString();
        GetCoinsTotal();
        textAudioSource.volume = 0f;
    }
    private void Unpaused()
    {
        pauseSoundSource.PlayOneShot(unpauseClip);
        TooltipUI.Instance.Hide();
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
