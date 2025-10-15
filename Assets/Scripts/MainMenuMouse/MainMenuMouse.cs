using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMouse : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    Transform mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue))
        {
            if (raycastHit.collider.CompareTag("Play") && Input.GetMouseButtonDown(0))
            {
                MenuScript.instance.MainScene();
            }
            if (raycastHit.collider.CompareTag("Quit") && Input.GetMouseButtonDown(0))
            {
                MenuScript.instance.QuitGame();
            }
        }
    }
}
