using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    [SerializeField] GameObject creditsObj; [SerializeField] GameObject menuObj;
    bool creditsOn;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void MainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsButton()
    {
        Debug.Log("options");
    }

    public void CreditsButton()
    {
        creditsObj.SetActive(true);
        menuObj.SetActive(false);
    }

    public void MainMenuButton()
    {
        creditsObj.SetActive(false);
        menuObj.SetActive(true);
    }
}
