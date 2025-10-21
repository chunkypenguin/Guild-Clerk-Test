using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewClipboard : MonoBehaviour
{
    public List<GameObject> clipBoardIcons;
    [SerializeField] List<GameObject> redHearts;
    [SerializeField] List <GameObject> purpleHearts;

    [SerializeField] AudioSource heartAudio;
    [SerializeField] AudioClip redHeartClip;

    [Header("Slide")]
    [SerializeField] private RectTransform targetImage; // The image you want to move
    [SerializeField] private float tweenDuration = 0.5f; // Speed of animation

    private Vector2 originalPos;
    private Vector2 offscreenPos;

    public static ReviewClipboard instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // Save the starting anchored position
        originalPos = targetImage.anchoredPosition;

        // Move it off-screen (above the screen)
        Canvas canvas = targetImage.GetComponentInParent<Canvas>();
        float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        offscreenPos = originalPos + new Vector2(0, canvasHeight * 1.2f);
        targetImage.anchoredPosition = offscreenPos;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            SlideIn();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            SlideOut();
        }
    }

    public void TurnOnHeart(int order)
    {
        clipBoardIcons[order].gameObject.transform.Find("RedHeart").gameObject.SetActive(true);
        heartAudio.pitch = 1.2f;
        heartAudio.PlayOneShot(redHeartClip);
    }

    public void TurnOffHeart(int order)
    {
        clipBoardIcons[order].gameObject.transform.Find("PurpleHeart").gameObject.SetActive(true);
        heartAudio.pitch = 0.8f;
        heartAudio.PlayOneShot(redHeartClip);
    }

    public void SlideIn()
    {
        targetImage.DOAnchorPos(originalPos, tweenDuration).SetEase(Ease.OutQuad);
    }

    public void SlideOut()
    {
        targetImage.DOAnchorPos(offscreenPos, tweenDuration).SetEase(Ease.InQuad);
    }
}
