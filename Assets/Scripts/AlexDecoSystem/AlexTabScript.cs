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
    [SerializeField] GameObject premiumItemsTab;

    [SerializeField] Button doneTab;

    [Header("Show/Hide")]

    //[SerializeField] GameObject decorUI;
    [SerializeField] RectTransform decorUI;
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 endPos;
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

    //TESTING
    bool decorHidden = true;

    public static AlexTabScript instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //new!
        startPos = decorUI.anchoredPosition;

        // Move it off-screen (above the screen)
        Canvas canvas = decorUI.GetComponentInParent<Canvas>();
        float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        endPos = startPos - new Vector2(0, canvasHeight /2);
        decorUI.anchoredPosition = endPos;

        //startPos = decorUI.transform.position;
        //decorUI.transform.position = startPos - new Vector3(0, distance, 0);
        //endPos = decorUI.transform.position;

        SetTabColorSelect(imageList[0]);
    }

    private void Update()
    {
        //FOR TESTING
        //DELETE
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (decorHidden)
            {
                ShowDecorUI();
                decorHidden = false;
            }
            else
            {
                HideDecorUI();
                decorHidden = true;
            }
        }

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

    public void PremiumItemsTab()
    {
        Deactivate();
        premiumItemsTab.SetActive(true);
    }

    void Deactivate()
    {
        textureTab.SetActive(false);
        clerkAmenTab.SetActive(false);
        knickKnackTab.SetActive(false);
        ceilingDecorTab.SetActive(false);
        premiumItemsTab.SetActive(false);
    }

    public void ShowDecorUI()
    {
        decorUI.DOAnchorPos(startPos, moveSpeed).SetEase(Ease.OutQuad).onComplete = () =>
        {
            doneTab.enabled = true;
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
        doneTab.enabled = false;
        //decorUI.transform.DOMove(endPos, moveSpeed);
        decorUI.DOAnchorPos(endPos, moveSpeed).SetEase(Ease.OutQuad);
        goldObject.SetActive(false);
        ExitDecoTut();

        //FOR DECO UI CURSOR STUFF
        DialogueBoxMouse.instance.hoveringDiaBox = false;
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
