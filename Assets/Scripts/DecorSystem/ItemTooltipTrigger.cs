using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTooltipTrigger : MonoBehaviour
{
    public string itemName;
    public int price;
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody rb;

    public void Initialize(string name, int price)
    {
        this.itemName = name;
        this.price = price;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var tooltip = hit.collider.GetComponent<ItemTooltipTrigger>();
            if (tooltip != null)
            {
                TooltipUI.Instance.ShowTooltip(tooltip.itemName, tooltip.price, hit.point);
            }
        }
        // else
        // {
        //     TooltipUI.Instance.HideTooltip();
        // }
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        rb.isKinematic = true;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;
    }

    private void OnMouseExit()
    {
        TooltipUI.Instance.HideTooltip();
    }
}
