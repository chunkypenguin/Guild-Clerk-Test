using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lightbug.GrabIt;

public class movecam : MonoBehaviour
{

    [SerializeField] Vector3 questAngle;
    [SerializeField] Vector3 equipmentAngle;
    [SerializeField] Vector3 centerAngle;
    [SerializeField] Vector3 rewardsAngle;
    [SerializeField] Vector3 rewardsPosition;
    [SerializeField] Vector3 centerPosition;
    [SerializeField] float camMoveSpeed;
    [SerializeField] public bool center, left, right, bottom;
    public bool canMoveCam;

    [SerializeField] GrabIt grabItScript;

    private void Start()
    {
        centerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveCamToQuests();
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveCamToEquipment();
        }
    }

    public void LeftButton()
    {
        if (canMoveCam)
        {
            if (center || bottom)
            {
                center = false;
                bottom = false;
                left = true;
                MoveCamToEquipment();
            }
            else if (right)
            {
                right = false;
                center = true;
                MoveCamCenter();
            }
            else if (left)
            {
                //do nothing
            }
        }

    }

    public void RightButton()
    {
        if (canMoveCam)
        {
            if (center || bottom)
            {
                center = false;
                bottom = false;
                right = true;
                MoveCamToQuests();
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
            }
            else if (bottom)
            {
                //do nothing
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
        transform.DOMove(centerPosition, camMoveSpeed);
        Invoke(nameof(CanMoveCamera), 0.5f);

    }

    private void MoveCamToEquipment()
    {
        canMoveCam = false;
        transform.DORotate(equipmentAngle, camMoveSpeed).onComplete = grabItScript.CamMoveCheck;
        transform.DOMove(centerPosition, camMoveSpeed);
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
        transform.DOMove(rewardsPosition, camMoveSpeed);
        Invoke(nameof(CanMoveCamera), 0.5f);
    }

    private void CanMoveCamera()
    {
        canMoveCam = true;
    }
}
