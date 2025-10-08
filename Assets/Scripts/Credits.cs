using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField] float scrollSpeed;

    private RectTransform rectTransform;

    [SerializeField] TMP_Text returnMenu;

    public bool scroll;
    public float scrollDistance;

    public static Credits instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        Color c = returnMenu.color;
        c.a = 0f;
        returnMenu.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        if(scroll)
        {
            //rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        }
    }

    public void ScrollToTarget()
    {
        rectTransform.DOAnchorPosY(scrollDistance, scrollSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            Debug.Log("Credits end");
            FadeReturnMenuText();
            rectTransform.anchoredPosition = new Vector2(0, -800);
            ScrollToTarget();
        });
    }

    private void FadeReturnMenuText()
    {
        Color c = returnMenu.color;
        c.a = 0f;
        returnMenu.color = c;

        returnMenu.DOFade(1f, 1.5f).SetEase(Ease.InOutQuad);
    }
}
