using TreeEditor;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //[SerializeField] Transform parent;
    //void Update()
    //{
    //    transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
    //}

    [SerializeField] movecam moveCamScript;
    [SerializeField] Rigidbody rb;

    [SerializeField] public bool isHeld;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isHeld)
        {
            // Make the object's forward direction match the camera's forward, but without locking Y rotation
            Vector3 cameraForward = Camera.main.transform.forward;

            // Remove any vertical tilt so it stays upright
            cameraForward.y = 0;

            // Create the rotation that keeps the sprite's face parallel to the camera
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward, Vector3.up);

            // Apply rotation but keep the object's own Y-axis rotation free
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);


        }

        if (moveCamScript.center)
        {
            //rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            //rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }
        else if (moveCamScript.left || moveCamScript.right)
        {
            //rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            //rb.constraints |= RigidbodyConstraints.FreezePositionX;
        }
    }

    private void FixedUpdate()
    {

        if (moveCamScript.left)
        {
            if(isHeld)
            {
                // Lock the position along the global X-axis
                Vector3 currentPosition = rb.position;
                rb.position = new Vector3(-8.4f, currentPosition.y, currentPosition.z); // Lock the X value to 0 (or any other fixed value)
            }

        }

        else if (moveCamScript.right)
        {
            if (isHeld)
            {
                // Lock the position along the global X-axis
                Vector3 currentPosition = rb.position;
                rb.position = new Vector3(8.25f, currentPosition.y, currentPosition.z); // Lock the X value to 0 (or any other fixed value)
            }
        }

        else if (moveCamScript.center)
        {
            if (isHeld)
            {
                // Lock the position along the global X-axis
                Vector3 currentPosition = rb.position;
                rb.position = new Vector3(currentPosition.x, currentPosition.y, 8.5f); // Lock the X value to 0 (or any other fixed value)
            }
        }

    }

}
