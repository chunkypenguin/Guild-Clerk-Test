using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnScript : MonoBehaviour
{
    public bool yarnOnDesk;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("YarnTrigger"))
        {
            yarnOnDesk = true;
        }

        if (other.gameObject.CompareTag("YarnOutTrigger"))
        {
            yarnOnDesk = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("YarnTrigger"))
        {
            yarnOnDesk = false;
        }
    }
}
