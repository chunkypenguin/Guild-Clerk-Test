using UnityEngine;
using System.Collections.Generic; // Needed for List handling

public class SuckScript : MonoBehaviour
{
    public float suckForce = 20f;
    public float maxSpeed = 10f;
    public float collectDistance = 0.5f; // Distance to trigger collection
    private Rigidbody rb;
    private Collider col;
    private Transform target;
    private bool isSucking = false;

    // Reference to the list (this will be set by the manager script)
    private GoldSystem gs;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        gs = GameObject.FindGameObjectWithTag("GoldObject").GetComponent<GoldSystem>();

        // Find the vacuum in the scene automatically
        GameObject vacuum = GameObject.FindGameObjectWithTag("Vacuum");
        if (vacuum != null)
        {
            target = vacuum.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //isSucking = true;
            //col.enabled = false; // Disable collider
            //rb.useGravity = false; // Disable gravity
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //isSucking = false;
            //col.enabled = true; // Enable collider
            //rb.useGravity = true; // Re-enable gravity
        }
    }

    void FixedUpdate()
    {
        if (!isSucking || target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        rb.AddForce(direction * suckForce, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        // Check if close enough to be collected
        if (Vector3.Distance(transform.position, target.position) < collectDistance)
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        // Call any function here (e.g., increase score, play sound, etc.)
        Debug.Log(gameObject.name + " collected!");

        // Remove from the coin list if it's tracked
        if (gs.coins != null)
        {
            gs.coins.Remove(gameObject);
        }
        gs.goldAmount--;
        // Destroy the coin
        Destroy(gameObject);
    }
}
