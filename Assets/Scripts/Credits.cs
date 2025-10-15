using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

public class Credits : MonoBehaviour
{
    [SerializeField] float scrollSpeed;

    private RectTransform rectTransform;

    [SerializeField] TMP_Text returnMenu;

    public bool scroll;
    public float scrollDistance;

    [SerializeField] TMP_Text playerNameText;

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
        ThanksPlayerText();

        rectTransform.DOAnchorPosY(scrollDistance, scrollSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            Debug.Log("Credits end");
            if (!DaySystem.instance.escTextOn)
            {
                FadeReturnMenuText();
            }
            rectTransform.anchoredPosition = new Vector2(0, -800);
            ScrollToTarget();
        });
    }

    public void FadeReturnMenuText()
    {
        Color c = returnMenu.color;
        c.a = 0f;
        returnMenu.color = c;

        returnMenu.DOFade(1f, 1.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            DaySystem.instance.escTextOn = true;
        });
    }

    public void ThanksPlayerText()
    {
        playerNameText.text = string.Format(playerNameText.text, UIManager.instance.playerName);
    }
}
