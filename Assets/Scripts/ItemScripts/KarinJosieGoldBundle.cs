using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarinJosieGoldBundle : MonoBehaviour
{
    public bool goldBundleOnDesk;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("YarnTrigger"))
        {
            goldBundleOnDesk = true;
        }

        if (other.gameObject.CompareTag("YarnOutTrigger"))
        {
            goldBundleOnDesk = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("YarnTrigger"))
        {
            goldBundleOnDesk = false;
        }
    }
}
