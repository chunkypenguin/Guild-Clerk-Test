using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanelleQuestAScript : MonoBehaviour
{
    public bool questAOnDesk;

    public static VanelleQuestAScript instance;

    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("YarnTrigger"))
        {
            questAOnDesk = true;
        }

        if (other.gameObject.CompareTag("YarnOutTrigger"))
        {
            questAOnDesk = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("YarnTrigger"))
        {
            questAOnDesk = false;
        }
    }
}
