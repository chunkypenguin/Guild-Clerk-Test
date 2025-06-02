using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePos3D : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] LayerMask guideLayer;
    [SerializeField] LayerMask noGuideLayer;
    [SerializeField] LayerMask pickedUpLayer;
    [SerializeField] GameObject pickUpObject;
    [SerializeField] bool pickedUp;
    [SerializeField] GameObject goldUI;
    private bool goldOpen;
    public Transform mousePos;

    [SerializeField] AudioSource bellSound;

    [SerializeField] QuestSystem questSystem;
    [SerializeField] CharacterSystem cs;
    [SerializeField] movecam mc;

    //MOUSE CURSOR STUFF
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] GameObject cursorUIObject;


    Vector2 cursorHotSpot;

    private void Start()
    {
        //cursorHotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        //Cursor.SetCursor(cursorTexture, cursorHotSpot, CursorMode.ForceSoftware);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //MOUSE STUFF
        cursorUIObject.transform.position = Input.mousePosition + new Vector3(18, -25, 0);

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, guideLayer))
        {
            mousePos.position = raycastHit.point;
        }

        Ray questray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(questray, out RaycastHit questraycastHit, float.MaxValue, noGuideLayer))
        {

            if (questraycastHit.collider.CompareTag("Bell") && Input.GetMouseButtonDown(0))
            {
                questSystem.FinalizeItems();
                bellSound.Play();
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
                GameObject quest = questraycastHit.transform.gameObject;
                questSystem.ShowQuestDescription(quest);
            }

            else //COULD BE OPTIMIZED WITH BOOL
            {
                questSystem.HideQuestDescription();
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // Stops processing clicks on 3D objects
            }

            if (questraycastHit.collider.CompareTag("GoldObject") && Input.GetMouseButtonDown(0))
            {
                if (mc.canOpenDrawer)
                {
                    Debug.Log("drawer pressed");
                    if (!mc.drawerOpen)
                    {
                        mc.BottomButton();
                        mc.drawerOpen = true;
                    }
                    else if (mc.drawerOpen)
                    {
                        mc.UpButton();
                        mc.drawerOpen = false;
                    }
                }
            }

            if (questraycastHit.collider.CompareTag("Character") && Input.GetMouseButtonDown(0))
            {
                //Debug.Log("ClickedCharacter");
                if (cs.isQuest)
                {
                    Debug.Log("I need a quest");
                    cs.TextHistory();
                    //cs.questDialogue.StartNewDialogue(cs.dialogueTriggerScript);
                    //mc.ButtonFlashUp(mc.rightButton);
                    //mc.flashWasOn = true;
                }
                else if (cs.isEquipment)
                {
                    Debug.Log("I need equipment");
                    cs.TextHistory();
                    //cs.equipmentDialogue.StartNewDialogue(cs.dialogueTriggerScript);
                    //mc.ButtonFlashUp(mc.leftButton);
                    //mc.flashWasOn = true;
                }
                else if (cs.isReward)
                {
                    Debug.Log("I need a reward");
                    cs.TextHistory();
                    //cs.rewardDialogue.StartNewDialogue(cs.dialogueTriggerScript);
                    //mc.ButtonFlashUp(mc.bottomButton);
                    //mc.flashWasOn = true;
                }
                else
                {
                    Debug.Log("idle");
                }
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
