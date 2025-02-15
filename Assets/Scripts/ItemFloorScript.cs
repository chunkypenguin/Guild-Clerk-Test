using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloorScript : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRotation;
    Rigidbody rb;
    [SerializeField] float downForce;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
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
            Debug.Log("hi");
            rb.isKinematic = true;
            transform.position = startPos;
            transform.rotation = startRotation;
            rb.isKinematic = false;
            if (gameObject.CompareTag("Quest"))
            {
                rb.useGravity = false;

                // Get all colliders attached to this GameObject
                Collider[] colliderz = gameObject.GetComponents<Collider>();

                // Disable each collider
                foreach (Collider col in colliderz)
                {
                    col.isTrigger = true;
                }
            }
        }
    }
}
