using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MuteSFX : MonoBehaviour
{

    //[SerializeField] private AudioMixer sfxMixer; 
    //[SerializeField] private string sfxVolumeParameter = "SFXVolume"; 
    //[SerializeField] private float muteVolumeDb = -80f; 
    //[SerializeField] private float normalVolumeDb = 0f; 
    [SerializeField] private float muteDuration = 3f; 

    private void Start()
    {
        StartCoroutine(MuteSFXForSeconds());
    }

    private IEnumerator MuteSFXForSeconds()
    {

        //sfxMixer.SetFloat(sfxVolumeParameter, muteVolumeDb);
        SoundMixerManager.instance.SetSoundFXVolume(0f);

        yield return new WaitForSeconds(muteDuration);

        SoundMixerManager.instance.SetSoundFXVolume(0.75f);
        //sfxMixer.SetFloat(sfxVolumeParameter, normalVolumeDb);
        Debug.Log("SFX unmuted");
    }
}
