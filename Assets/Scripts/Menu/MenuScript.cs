using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
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
        Debug.Log("credits");
    }
}
