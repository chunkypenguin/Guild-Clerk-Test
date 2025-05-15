// This script is for the Floating Heart Animation.

using UnityEngine;
using System.Collections;

public class ReputationFX : MonoBehaviour
{
    [Tooltip("How long heart stays on screen")]
    [SerializeField] private float _lifetime = 1f;

    [Tooltip("How far it floats vertically")]
    [SerializeField] private float _verticalDistance = 60f;

    // This is true for the positive heart, false for the negative heart to make it float down.
    [Tooltip("Set true for upward movement, false for downward")]
    [SerializeField] private bool _floatUp = true;

    [Tooltip("How far it swings left/right")]
    [SerializeField] private float _horizontalAmplitude = 10f;

    [Tooltip("How fast it goes left/right")]
    [SerializeField] private float _waveFreq = 6f;
    
    [Tooltip("When fading starts, as % of life (0 = fade immediately, 1 = fade at end)")]
    [Range(0f, 1f)]
    [SerializeField] private float _fadeStartPercent = 0.5f;

    // This is a reference to the CanvasGroup component. This is used to control the alpha of the heart.
    private CanvasGroup canvasGroup;
    // This is a reference to the RectTransform component. This is used to control the position of the heart.
    private RectTransform rectTransform;
    // This is the starting position of the heart. This is used to reset the position when the heart is activated.
    private Vector3 startPos;

    // Awake is called at the very start of the game, before anything else.
    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    // This function is called when the object is enabled.
    private void OnEnable() {
        // Reset visuals
        canvasGroup.alpha = 1f;
        rectTransform.anchoredPosition = startPos;

        // Start the animation
        StartCoroutine(Animate());
    }

    // This is the coroutine that handles the animation of the heart.
    // A coroutine is a function that can last for a specified amount of time.
    // This coroutine lasts for the lifetime of the heart.
    private IEnumerator Animate() {
        // Set the timer to 0
        float timer = 0f;

        // This is the direction of the heart. It will be either up or down depending on the _floatUp variable.
        Vector2 direction = _floatUp ? Vector2.up : Vector2.down;

        // This is the fade start time. It is calculated by multiplying the lifetime by the fade start percent.
        float fadeStartTime = _lifetime * _fadeStartPercent;

        // This while loop runs until the timer is less than the lifetime of the heart.
        while (timer < _lifetime) {
            // t is the normalized time. It goes from 0 to 1 over the lifetime of the heart.
            float t = timer / _lifetime;

            // Vert movement
            float yOffset = direction.y * _verticalDistance * t;

            // Horizontal movement
            float xOffset = Mathf.Sin(Time.time * _waveFreq) * _horizontalAmplitude;

            // Update position
            rectTransform.anchoredPosition = startPos + new Vector3(xOffset, yOffset);

            // Start fading out when the timer reaches the fade start time.
            if (timer >= fadeStartTime) {
                float fadeProgress = (timer - fadeStartTime) / (_lifetime - fadeStartTime);
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, fadeProgress);
            }

            // timer is incremented by the time since the last frame.
            timer += Time.deltaTime;
            yield return null;
        }

        // Deactivate the object after animation
        gameObject.SetActive(false);
    }
}
