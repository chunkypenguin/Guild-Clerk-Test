using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lightbug.GrabIt;
using UnityEngine.UI;

public class movecam : MonoBehaviour
{

    [SerializeField] Vector3 questAngle;
    [SerializeField] Vector3 equipmentAngle;
    [SerializeField] Vector3 centerAngle;
    [SerializeField] Vector3 rewardsAngle;
    [SerializeField] Vector3 rewardsPosition;
    [SerializeField] Vector3 centerPosition;
    [SerializeField] Vector3 questPosition;
    [SerializeField] Vector3 equipmentPosition;
    [SerializeField] float camMoveSpeed;
    public bool center, left, right, bottom;
    public bool canMoveCam;

    public GameObject leftButton, rightButton, topButton, bottomButton;
    public bool lockLeft, lockRight, lockTop, lockBottom;

    [SerializeField] GrabIt grabItScript;
    [SerializeField] GoldSystem goldSystemScript;
    [SerializeField] CharacterSystem cs;

    //TUTORIAL STUFF
    public bool tutorial;
    //[SerializeField] bool turnedRight;
    //[SerializeField] bool turnedLeft;
    [SerializeField] public bool dontFlash;
    [SerializeField] public bool flashOn;

    public bool flashWasOn;

    public bool drawerOpen;
    public bool canOpenDrawer;

    private void Start()
    {
        //centerPosition = transform.position;
        transform.position = centerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //ButtonFlashUp();
        }

        //else if (Input.GetKeyDown(KeyCode.A))
        //{
        //    MoveCamToEquipment();
        //}
    }

    public void LeftButtonLockToggle()
    {
        if(!lockLeft)
        {
            leftButton.SetActive(false);
            lockLeft = true;
        }
        else
        {
            leftButton.SetActive(true);
            lockLeft = false;
        }
    }

    public void RightButtonLockToggle()
    {
        if (!lockRight)
        {
            rightButton.SetActive(false);
            lockRight = true;
        }
        else
        {
            rightButton.SetActive(true);
            lockRight = false;
        }
    }

    public void TopButtonLockToggle()
    {
        if (!lockTop)
        {
            topButton.SetActive(false);
            lockTop = true;
        }
        else
        {
            topButton.SetActive(true);
            lockTop = false;
        }
    }

    public void BottomButtonLockToggle()
    {
        if (!lockBottom)
        {
            bottomButton.SetActive(false);
            lockBottom = true;
        }
        else
        {
            bottomButton.SetActive(true);
            lockBottom = false;
        }
    }

    public void LeftButton()
    {
        if (canMoveCam)
        {
            if (center)
            {
                center = false;
                bottom = false;
                left = true;
                MoveCamToEquipment();
            }
            else if (bottom)
            {
                center = false;
                bottom = false;
                left = true;
                MoveCamToEquipment();
                goldSystemScript.CloseGoldDrawer();
            }
            else if (right)
            {
                right = false;
                center = true;
                MoveCamCenter();

                ///Tutorial
                //if (turnedLeft)
                //{
                //    dontFlash = true;
                //    turnedLeft = false;
                //    tutorial = false;
                //}
            }
            else if (left)
            {
                //do nothing
            }

            if(flashWasOn)
            {
                ButtonFlashDown(leftButton);
                flashWasOn = false;
            }
        }

    }

    public void RightButton()
    {
        if (canMoveCam)
        {
            if (center)
            {
                center = false;
                bottom = false;
                right = true;
                MoveCamToQuests();

                //if (turnedRight)
                //{
                //    cs.josieD1P4.StartNewDialogue(cs.dialogueTriggerScript);
                //    dontFlash = true;
                //    turnedRight = false;
                //}
            }
            else if (bottom)
            {
                center = false;
                bottom = false;
                right = true;
                MoveCamToQuests();
                goldSystemScript.CloseGoldDrawer();
            }
            else if (left)
            {
                left = false;
                center = true;
                MoveCamCenter();
            }
            else if (right)
            {
                //do nothing
            }

            if (flashWasOn)
            {
                ButtonFlashDown(rightButton);
                flashWasOn = false;
            }
        }

    }

    public void BottomButton()
    {
        if (canMoveCam)
        {
            if (center || left || right)
            {
                center = false;
                left = false;
                right = false;
                bottom = true;
                MoveCamRewards();

                goldSystemScript.OpenGoldDrawer();
            }
            else if (bottom)
            {
                //do nothing
            }

            if (flashWasOn)
            {
                ButtonFlashDown(bottomButton);
                flashWasOn = false;
            }
        }

    }

    public void UpButton()
    {
        if (canMoveCam)
        {
            if (bottom)
            {
                bottom = false;
                center = true;
                MoveCamCenter();

                goldSystemScript.CloseGoldDrawer();
            }

            else
            {
                //do nothing
            }
        }

    }


    private void MoveCamToQuests()
    {
        canMoveCam = false;
        transform.DORotate(questAngle, camMoveSpeed).onComplete = grabItScript.CamMoveCheck;
        transform.DOMove(questPosition, camMoveSpeed);
        Invoke(nameof(CanMoveCamera), 0.5f);

    }

    private void MoveCamToEquipment()
    {
        canMoveCam = false;
        transform.DORotate(equipmentAngle, camMoveSpeed).onComplete = grabItScript.CamMoveCheck;
        transform.DOMove(equipmentPosition, camMoveSpeed);
        Invoke(nameof(CanMoveCamera), 0.5f);
    }

    private void MoveCamCenter()
    {
        canMoveCam = false;
        transform.DORotate(centerAngle, camMoveSpeed).onComplete = grabItScript.CamMoveCheck;
        transform.DOMove(centerPosition, camMoveSpeed);
        Invoke(nameof(CanMoveCamera), 0.5f);
    }

    private void MoveCamRewards()
    {
        canMoveCam = false;
        transform.DORotate(rewardsAngle, camMoveSpeed).onComplete = grabItScript.CamMoveCheck;
        transform.DOMove(centerPosition, camMoveSpeed);
        Invoke(nameof(CanMoveCamera), 0.5f);
    }

    private void CanMoveCamera()
    {
        canMoveCam = true;
    }

    public void PlayerTurnedRight()
    {
        //turnedRight = true;
    }

    public void PlayerTurnedLeft()
    {
        //turnedLeft = true;
    }

    public void ButtonFlashUp(GameObject button)
    {
        flashOn = true;
        button.GetComponent<Image>().DOFade(0.75f, 1f).onComplete = () =>
        {
            ButtonFlashDown(button);
            
        };
    }

    public void ButtonFlashDown(GameObject button)
    {
        button.GetComponent<Image>().DOFade(0, 1f).onComplete = () =>
        {
            if (!dontFlash)
            {
                ButtonFlashUp(button);
            }
            else
            {
                dontFlash = false;
                flashOn = false;
            }
            
        };
    }

    public void TurnFlashOff()
    {
        dontFlash = true;
        //flashOn = false;
    }

    public void UnlockDrawer()
    {
        canOpenDrawer = true;
    }
}
