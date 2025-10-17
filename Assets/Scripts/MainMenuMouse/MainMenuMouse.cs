using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuMouse : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    Transform mousePos;
    private Transform currentHovered;
    private Vector3 originalScale;
    private Tween currentTween;
    [SerializeField] float scaleSizeMult;
    [SerializeField] float scaleTime;

    private Dictionary<Transform, Vector3> originalScales = new();


    private void Start()
    {
        // Find all objects in the "MMButtons" layer and store their original scales
        int buttonLayer = LayerMask.NameToLayer("MMButtons");

        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.layer == buttonLayer)
            {
                originalScales[obj.transform] = obj.transform.localScale;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue)) //Is the mouse ray hitting any collider in the scene? If yes, give me info about it and run the code inside the if.
        {
            // Hover enter
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("MMButtons"))
            {
                Transform hitTransform = hit.collider.transform;

                if (currentHovered != hitTransform)
                {
                    // Reset previous one
                    ResetHovered();

                    currentHovered = hitTransform;
                    //originalScale = currentHovered.localScale;
                    originalScale = originalScales[currentHovered];

                    currentTween?.Kill();
                    currentTween = currentHovered.DOScale(originalScale * scaleSizeMult, scaleTime).SetEase(Ease.OutBack);
                }
            }
            else
            {
                ResetHovered();
            }

            if (hit.collider.CompareTag("Play") && Input.GetMouseButtonDown(0))
            {
                MenuScript.instance.MainScene();
            }
            if (hit.collider.CompareTag("Quit") && Input.GetMouseButtonDown(0))
            {
                MenuScript.instance.QuitGame();
            }
        }
        else
        {
            ResetHovered();
        }
    }

    private void ResetHovered()
    {
        if (currentHovered != null)
        {
            currentTween?.Kill();
            currentHovered.DOScale(originalScale, scaleTime).SetEase(Ease.InOutSine);
            currentHovered = null;
        }
    }
}
