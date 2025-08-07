using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class AlexTabScript : MonoBehaviour
{

    [SerializeField] GameObject textureTab;
    [SerializeField] GameObject clerkAmenTab;
    [SerializeField] GameObject knickKnackTab;
    [SerializeField] GameObject ceilingDecorTab;

    [Header("Show/Hide")]

    [SerializeField] GameObject decorUI;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    [SerializeField] float moveSpeed;
    [SerializeField] int distance;
    bool isShown;

    //UI
    [SerializeField] GameObject goldObject;
    [SerializeField] TMP_Text goldCount;
    int goldAmount;

    [SerializeField] GameObject decoTutorialUI;
    bool showedTut;
    [SerializeField] List<Image> imageList;
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;

    public static AlexTabScript instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        startPos = decorUI.transform.position;
        decorUI.transform.position = startPos - new Vector3(0, distance, 0);
        endPos = decorUI.transform.position;
    }

    private void Update()
    {
        goldAmount = DayReputationTracker.Instance.GetGold();
        goldCount.text = goldAmount.ToString();
    }

    public void SetTabColorSelect(Image currentImage)
    {
        foreach(var image in imageList)
        {
            image.color = defaultColor;
        }

        currentImage.color = Color.white;
    }

    public void SetTabColorHighlight(Image currentImage)
    {
        if(currentImage.color != Color.white)
        {
            currentImage.color = highlightColor;
        }
    }

    public void SetTabColorDefault(Image currentImage)
    {
        if (currentImage.color != Color.white)
        {
            currentImage.color = defaultColor;
        }
    }

    public void TextureTab()
    {
        Deactivate();
        textureTab.SetActive(true);
    }
    public void ClerkAmenTab()
    {
        Deactivate();
        clerkAmenTab.SetActive(true);
    }
    public void KnickKnackTab()
    {
        Deactivate();
        knickKnackTab.SetActive(true);
    }
    public void CeilingDecorTab()
    {
        Deactivate();
        ceilingDecorTab.SetActive(true);
    }

    void Deactivate()
    {
        textureTab.SetActive(false);
        clerkAmenTab.SetActive(false);
        knickKnackTab.SetActive(false);
        ceilingDecorTab.SetActive(false);
    }

    public void ShowDecorUI()
    {
        decorUI.transform.DOMove(startPos, moveSpeed).onComplete = () =>
        {
            goldObject.SetActive(true);
            if (!showedTut)
            {
                decoTutorialUI.SetActive(true);
                showedTut = true;
            }
        };
    }

    public void HideDecorUI()
    {
        decorUI.transform.DOMove(endPos, moveSpeed);
        goldObject.SetActive(false);
        ExitDecoTut();
    }

    public void DisplayDecorUI()
    {
        if(!isShown)
        {
            ShowDecorUI();
            isShown = true;
        }
        else
        {
            HideDecorUI();
            isShown= false;
        }
    }

    public void ExitDecoTut()
    {
        decoTutorialUI.SetActive(false);
    }
}
