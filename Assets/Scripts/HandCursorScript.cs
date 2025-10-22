using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HandCursorScript : MonoBehaviour
{
    [SerializeField] GameObject cursorUIObject;
    [SerializeField] Image cursorImage;
    [SerializeField] Sprite cursorPoint;
    [SerializeField] Sprite cursorClick;

    //NEW
    public RectTransform cursorUIObjectNew;
    public Canvas canvas;

    bool clicking;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name);

        Cursor.visible = false;
        cursorImage = cursorUIObject.GetComponent<Image>();
        cursorImage.sprite = cursorPoint;
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

        cursorUIObjectNew.localPosition = localPoint + new Vector2(18, -32); // offset
    }

    private void CursorStuff()
    {

        if (Input.GetMouseButtonDown(0))
        {
            clicking = true;
            cursorImage.sprite = cursorClick;
            StartCoroutine(ClickDelay());
        }
        else if (!clicking)
        {
            cursorImage.sprite = cursorPoint;
        }
    }

    private IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(0.15f);
        clicking = false;
    }
}
