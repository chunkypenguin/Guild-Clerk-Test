using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReviewBar : MonoBehaviour
{
    public Slider reviewSlider;
    public Slider easeSlider;
    public float maxReviewPoints;
    public float reviewPoints;
    private float lerpSpeed = 0.1f;

    [SerializeField] GameObject parent;
    public Vector3 baseScale;  // original scale
    public float punchAmount = 0.2f;         // how much to scale up
    public float duration = 0.2f;            // total duration of the punch
    public float maxScaleMultiplier = 1.5f;  // cap max scale

    private Tween currentTween;

    public static ReviewBar instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        reviewPoints = maxReviewPoints/2;
    }

    // Update is called once per frame
    void Update()
    {
        if(reviewSlider.value != reviewPoints)
        {
            reviewSlider.value = Mathf.Lerp(reviewSlider.value, reviewPoints, lerpSpeed);
        }
        if(reviewSlider.value != easeSlider.value)
        {
            //easeSlider.value = Mathf.Lerp(easeSlider.value, reviewPoints, 0.05f);
        }
    }

    public void AddReviewPoints(float points)
    {
        reviewPoints += points;
        PunchScale();
    }


    public void PunchScale()
    {
        // Kill any existing tween so we don't stack multiple tweens endlessly
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        // Calculate target scale, capping by maxScaleMultiplier
        Vector3 targetScale = parent.transform.localScale + Vector3.one * punchAmount;
        targetScale = Vector3.Min(targetScale, baseScale * maxScaleMultiplier);

        // Animate to target scale and back
        currentTween = parent.transform.DOScale(targetScale, duration / 2f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // Return to base scale smoothly
                currentTween = parent.transform.DOScale(baseScale, duration / 2f)
                    .SetEase(Ease.InQuad);
            });
    }

    private void Reset()
    {
        // Ensure starting scale is correct
        parent.transform.localScale = baseScale;
    }
}
