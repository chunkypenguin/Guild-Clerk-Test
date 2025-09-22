using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Credits : MonoBehaviour
{
    [SerializeField] float scrollSpeed;

    private RectTransform rectTransform;

    [SerializeField] GameObject returnMenu;

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
            returnMenu.SetActive(true);
            rectTransform.anchoredPosition = new Vector2(0, -800);
            ScrollToTarget();
        });
    }
}
