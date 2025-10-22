using UnityEngine;
using UnityEngine.Audio;

public class CoinSoundManager : MonoBehaviour
{
    public static CoinSoundManager Instance;

    [Tooltip("Number of pooled AudioSources used for coin sounds.")]
    public int poolSize = 5;

    [Tooltip("Audio Mixer group for coin sound effects.")]
    public AudioMixerGroup sfxGroup;

    private AudioSource[] sources;
    private int nextIndex = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        sources = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].playOnAwake = false;

            if (sfxGroup != null)
                sources[i].outputAudioMixerGroup = sfxGroup;
        }
    }

    public void PlayCoinSound(AudioClip clip, float volume)
    {
        if (clip == null) return;

        sources[nextIndex].PlayOneShot(clip, volume);
        nextIndex = (nextIndex + 1) % poolSize;
    }
}
