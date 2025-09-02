using UnityEngine;
using UnityEngine.UI;

public class GlobalButtonClickSound : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickAudio;

    void Start()
    {
        // Find all buttons in the scene
        Button[] buttons = FindObjectsOfType<Button>(true); // true includes inactive ones

        foreach (Button b in buttons)
        {
            // Skip buttons with the NoClickSound component
            if (b.GetComponent<NoClickSound>() != null)
                continue;

            b.onClick.AddListener(() => PlayClick());
        }
    }

    private void PlayClick()
    {
        if (buttonClickAudio != null)
        {
            buttonClickAudio.Play();
        }
    }
}