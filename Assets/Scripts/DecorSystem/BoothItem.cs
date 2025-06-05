using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothItem : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public InventoryManager.ItemType type;
    public GameObject visualPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckoutBox"))
        {
            InventoryManager inventory = FindObjectOfType<InventoryManager>();
            if (inventory != null)
            {
                inventory.AddItem(gameObject);
            }
        }
    }
}
