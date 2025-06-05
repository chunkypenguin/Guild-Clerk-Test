using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseBell : MonoBehaviour
{
    public InventoryManager inventory;
    private bool canBuy;
    void Update()
    {
        canBuy = BoothManager.Instance != null && BoothManager.Instance.purchasabledItems;
        if (canBuy && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var bell = hit.collider.GetComponent<PurchaseBell>();
                if (bell != null)
                {
                    inventory.PurchaseSelectedItems();
                }
            }
        }
    }
}
