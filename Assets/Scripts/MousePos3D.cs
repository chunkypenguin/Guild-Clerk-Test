using HeneGames.DialogueSystem;
using Lightbug.GrabIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    //[SerializeField] Texture2D currentCursorTexture;
    //[SerializeField] Texture2D cursorTexture;
    [SerializeField] GameObject cursorUIObject;
    [SerializeField] Image cursorImage;
    [SerializeField] Sprite cursorPoint;
    [SerializeField] Sprite cursorClick;
    [SerializeField] Sprite cursorCanGrab;
    [SerializeField] Sprite cursorGrab;

    [SerializeField] GrabIt grabItScript;

    public bool clicking;

    Vector2 cursorHotSpot;

    public bool questText;
    public bool dialogueOpen;

    public static MousePos3D instance;

    public bool isHoveringQuest;

    [Header("NewCursorPositionStuff")]
    public RectTransform cursorUIObjectNew;
    public Canvas canvas;
    [SerializeField] int mouseePaddingX;
    [SerializeField] int mouseePaddingY;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //cursorHotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        //Cursor.SetCursor(cursorTexture, cursorHotSpot, CursorMode.ForceSoftware);
        Cursor.visible = false;
        cursorImage = cursorUIObject.GetComponent<Image>();
        cursorImage.sprite = cursorPoint;

        grabItScript = GrabIt.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //MOUSE STUFF
        CursorStuff();
        //cursorUIObject.transform.position = Input.mousePosition + new Vector3(18, -25, 0);
        //NEW
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
        Input.mousePosition,
        canvas.renderMode == RenderMode.ScreenSpaceCamera ? canvas.worldCamera : null,
        out localPoint
        );

        cursorUIObjectNew.localPosition = localPoint + new Vector2(mouseePaddingX, mouseePaddingY); // offset



        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, guideLayer))
        {
            mousePos.position = raycastHit.point;
        }

        Ray questray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(questray, out RaycastHit questraycastHit, float.MaxValue, noGuideLayer))
        {
            if (DaySystem.instance.endOfDayCantPause || PauseScript.instance.paused)
            {
                return;
            }

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

            if (questraycastHit.collider.CompareTag("LornePotion") && Input.GetMouseButtonDown(0))
            {
                //make room dark and potion glow
                Debug.Log("potion");
                LorneScript.instance.LornePotion();
            }

            if (questraycastHit.collider.CompareTag("Quest"))
            {
                if (!questText)
                {
                    GameObject quest = questraycastHit.transform.gameObject;
                    questSystem.ShowQuestDescription(quest);
                    questText = true;
                }

                isHoveringQuest = true;
            }

            else //COULD BE OPTIMIZED WITH BOOL
            {
                questSystem.HideQuestDescription();
                questText = false;

                isHoveringQuest = false;
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
                if (cs.isQuest && !dialogueOpen)
                {
                    Debug.Log("I need a quest");
                    cs.TextHistory();
                    //cs.questDialogue.StartNewDialogue(cs.dialogueTriggerScript);
                    //mc.ButtonFlashUp(mc.rightButton);
                    //mc.flashWasOn = true;
                    dialogueOpen = true;
                }
                else if (cs.isEquipment && !dialogueOpen)
                {
                    Debug.Log("I need equipment");
                    cs.TextHistory();
                    //cs.equipmentDialogue.StartNewDialogue(cs.dialogueTriggerScript);
                    //mc.ButtonFlashUp(mc.leftButton);
                    //mc.flashWasOn = true;
                    dialogueOpen = true;
                }
                else if (cs.isReward && !dialogueOpen)
                {
                    Debug.Log("I need a reward");
                    cs.TextHistory();
                    //cs.rewardDialogue.StartNewDialogue(cs.dialogueTriggerScript);
                    //mc.ButtonFlashUp(mc.bottomButton);
                    //mc.flashWasOn = true;
                    dialogueOpen = true;
                }
                else if (LotestScript.instance.lotestWillSayAhem && !dialogueOpen)
                {
                    cs.TextHistory();
                    dialogueOpen = true;
                }
                else
                {
                    Debug.Log("idle");
                    DialogueUI.instance.NextSentenceSoft();
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

    private void CursorStuff()
    {
        if (grabItScript.m_grabbing)
        {
            cursorImage.sprite = cursorGrab;
        }
        else if (grabItScript.m_hoveringGrab && !DialogueBoxMouse.instance.hoveringDiaBox)
        {
            cursorImage.sprite = cursorCanGrab;
        }
        else if(Input.GetMouseButtonDown(0))
        {
            clicking = true;
            cursorImage.sprite = cursorClick;
            StartCoroutine(ClickDelay());
        }
        else if(!clicking)
        {
            cursorImage.sprite = cursorPoint;
        }
    }

    private IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(0.2f);
        clicking = false;
    }
}
