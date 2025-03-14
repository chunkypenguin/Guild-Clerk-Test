using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaspberriesScript : MonoBehaviour
{
    public bool raspberriesOnDesk;

    public bool refusedOnce;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("YarnTrigger"))
        {
            raspberriesOnDesk = true;
        }

        if (other.gameObject.CompareTag("YarnOutTrigger"))
        {
            raspberriesOnDesk = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("YarnTrigger"))
        {
            raspberriesOnDesk = false;
        }
    }
}
