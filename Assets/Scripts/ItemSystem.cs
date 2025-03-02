using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{

    [SerializeField] CharacterSystem cs;

    [SerializeField] bool isSuctionActive;

    [SerializeField] Rigidbody rb;

    [SerializeField] Transform vacuumPoint; // The position coins should be sucked toward
    [SerializeField] float suckForce = 20f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float collectDistance = 0.5f;
    private Vector3 startScale; // Store initial scale
    private float initialDistance;

    private void FixedUpdate()
    {
        if (!isSuctionActive) return;

        if (rb != null)
        {
            // Turn off gravity and collider while getting sucked
            rb.useGravity = false;
            Collider col = rb.GetComponent<Collider>();
            if (col != null) col.enabled = false;

            Vector3 direction = (vacuumPoint.position - rb.transform.position).normalized;
            rb.AddForce(direction * suckForce, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

            // Shrink the object as it moves towards the vacuum point
            float distance = Vector3.Distance(rb.transform.position, vacuumPoint.position);
            float shrinkFactor = Mathf.Clamp01(distance / initialDistance); // Normalize shrink between 1 and 0
            rb.transform.localScale = startScale * shrinkFactor; // Apply scale reduction
        }

        // Check if close enough to be collected
        if (Vector3.Distance(rb.transform.position, vacuumPoint.position) < collectDistance)
        {
            CollectItem(rb.gameObject);
        }
    }

    public void GetItemRb(GameObject item)
    {
        rb = item.GetComponent<Rigidbody>();
        startScale = rb.transform.localScale; // Store initial scale
        initialDistance = Vector3.Distance(rb.transform.position, vacuumPoint.position); // Get starting distance
        isSuctionActive = true;
    }

    private void CollectItem(GameObject itemObject)
    {
        isSuctionActive = false;

        // Destroy the quest
        itemObject.GetComponent<ItemFloorScript>().ResetItem();
        itemObject.SetActive(false);

    }
}
