using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCursorScript : MonoBehaviour
{
    [SerializeField] GameObject cursorUIObject;
    [SerializeField] Image cursorImage;
    [SerializeField] Sprite cursorPoint;
    [SerializeField] Sprite cursorClick;

    bool clicking;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        cursorImage = cursorUIObject.GetComponent<Image>();
        cursorImage.sprite = cursorPoint;
    }

    // Update is called once per frame
    void Update()
    {
        //MOUSE STUFF
        CursorStuff();
        cursorUIObject.transform.position = Input.mousePosition + new Vector3(18, -25, 0);
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
