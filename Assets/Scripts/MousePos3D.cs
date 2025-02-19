using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos3D : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] LayerMask guideLayer;
    [SerializeField] LayerMask pickedUpLayer;
    [SerializeField] GameObject pickUpObject;
    [SerializeField] bool pickedUp;
    [SerializeField] GameObject goldUI;
    private bool goldOpen;
    public Transform mousePos;

    [SerializeField] QuestSystem questSystem;
    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, guideLayer))
        {
            mousePos.position = raycastHit.point;
        }

        Ray questray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(questray, out RaycastHit questraycastHit))
        {
            if (questraycastHit.collider.CompareTag("Bell") && Input.GetMouseButtonDown(0))
            {
                questSystem.FinalizeItems();
            }

            if (questraycastHit.collider.CompareTag("Gold") && Input.GetMouseButtonDown(0))
            {
                if (!goldOpen)
                {
                    goldOpen = true;
                    goldUI.SetActive(true);
                }
                else
                {
                    goldOpen = false;
                    goldUI.SetActive(false);
                }

            }

            if (questraycastHit.collider.CompareTag("Quest"))
            {
                questSystem.ShowQuestDescription();
            }

            else
            {
                questSystem.HideQuestDescription();
            }
        }

        //Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        //if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, guideLayer))
        //{
        //    transform.position = raycastHit.point;

        //    if(Input.GetMouseButtonDown(0) && raycastHit.collider.gameObject.CompareTag("PickUppable"))
        //    {
        //        pickUpObject = raycastHit.collider.gameObject;
        //        pickUpObject.layer = LayerMask.NameToLayer("PickedUp");
        //        pickedUp = true;
        //    }

        //}

        //if (pickedUp)
        //{
        //    pickUpObject.transform.position = transform.position;
        //}

        //if(Input.GetMouseButtonUp(0) && pickedUp)
        //{
        //    pickedUp = false;
        //    pickUpObject.layer = LayerMask.NameToLayer("GuideLayer");
        //}
    }
}
