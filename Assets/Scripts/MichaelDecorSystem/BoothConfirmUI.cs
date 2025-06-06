using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothConfirmUI : MonoBehaviour
{
    public GameObject confirmTab;

    public void OpenConfirmTab()
    {
        confirmTab.SetActive(true);
    }

    public void Confirm()
    {
        confirmTab.SetActive(false);
    }

    public void Cancel()
    {
        confirmTab.SetActive(false);
    }
}
