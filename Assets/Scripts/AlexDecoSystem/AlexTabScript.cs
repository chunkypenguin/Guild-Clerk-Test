using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    private void Start()
    {
        startPos = decorUI.transform.position;
        decorUI.transform.position = startPos - new Vector3(0, distance, 0);
        endPos = decorUI.transform.position;
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
        decorUI.transform.DOMove(startPos, moveSpeed);
    }

    public void HideDecorUI()
    {
        decorUI.transform.DOMove(endPos, moveSpeed);
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
}
