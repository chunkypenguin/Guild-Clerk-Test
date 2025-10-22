using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Slider[] volumeSliders;
    [SerializeField] Slider[] volumeSliders2;

    private void Start()
    {

        SetMasterVolume(0.5f);
        SetMusicVolume(0.5f);
        SetTextVolume(0.75f);
        SetSoundFXVolume(0.75f);

        foreach(Slider volume in volumeSliders)
        {
            volume.value = 0.5f;
        }
        foreach(Slider volume in volumeSliders2)
        {
            volume.value = 0.75f;
        }
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }

    public void SetTextVolume(float level)
    {
        audioMixer.SetFloat("TextVolume", Mathf.Log10(level) * 20f);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(level) * 20f);
    }
}
