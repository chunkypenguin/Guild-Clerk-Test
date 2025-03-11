using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloorScript : MonoBehaviour
{
    Vector3 startPos;
    Vector3 startScale;
    Quaternion startRotation;
    Rigidbody rb;
    Rigidbody prb;
    [SerializeField] float downForce;

    [SerializeField] ParticleSystem poofFX;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;

        rb = GetComponent<Rigidbody>();

        poofFX = GameObject.Find("CloudPoof").GetComponent<ParticleSystem>();
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.layer == 9)
    //    {
    //        Debug.Log("whats up dawg");
    //        //rb.velocity = Vector3.zero;
    //        rb.isKinematic = true;
    //        transform.position = startPos;
    //        //transform.rotation = Quaternion.identity;
    //        transform.rotation = startRotation;
    //        rb.isKinematic = false;
    //        //rb.AddForce((Vector3.down * downForce), ForceMode.Impulse);
    //        Debug.Log("hi");
    //        //if (gameObject.CompareTag("Quest"))
    //        //{
    //        //    rb.isKinematic = true;
    //        //}
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            ResetItem();
        }
    }

    public void ResetItem()
    {
        if (gameObject.CompareTag("Quest"))
        {
            if (gameObject.name == "QuestReturn")
            {
                prb = gameObject.transform.parent.GetComponent<Rigidbody>();

                rb.useGravity = true;
                prb.isKinematic = true;
                prb.transform.position = startPos;
                prb.transform.rotation = startRotation;
                transform.localScale = startScale;
                prb.isKinematic = false;

                if (prb.gameObject.activeInHierarchy)
                {
                    poofFX.transform.position = rb.transform.position;
                    poofFX.Play();
                }

                //prb.useGravity = false;

                //prb.constraints = RigidbodyConstraints.FreezeAll;
                prb.constraints =  RigidbodyConstraints.FreezePositionZ |
                 RigidbodyConstraints.FreezeRotation;

                // Get all colliders attached to this GameObject
                Collider[] colliderz = gameObject.GetComponents<Collider>();

                // Disable each collider
                foreach (Collider col in colliderz)
                {
                    col.enabled = true;
                    //col.isTrigger = true;
                }
            }

            else
            {
                prb = gameObject.transform.parent.GetComponent<Rigidbody>();

                rb.useGravity = true;
                prb.isKinematic = true;
                prb.transform.position = startPos;
                prb.transform.rotation = startRotation;
                transform.localScale = startScale;
                prb.isKinematic = false;

                if (prb.gameObject.activeInHierarchy)
                {
                    poofFX.transform.position = rb.transform.position;
                    poofFX.Play();
                }

                prb.useGravity = false;

                prb.constraints = RigidbodyConstraints.FreezeAll;

                // Get all colliders attached to this GameObject
                Collider[] colliderz = gameObject.GetComponents<Collider>();

                // Disable each collider
                foreach (Collider col in colliderz)
                {
                    col.enabled = true;
                    col.isTrigger = true;
                }
            }


        }
        else
        {
            Debug.Log("hi");
            rb.isKinematic = true;
            transform.position = startPos;
            transform.rotation = startRotation;
            transform.localScale = startScale;
            rb.isKinematic = false;

            if (rb.gameObject.activeInHierarchy)
            {
                poofFX.transform.position = rb.transform.position;
                poofFX.Play();
            }

        }
    }
}
