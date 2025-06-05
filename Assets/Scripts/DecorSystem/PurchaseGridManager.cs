using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseGridManager : MonoBehaviour
{
    public List<PurchaseGridSlot> gridSlots;
    public TMP_Text goldText;
    public TMP_Text totalCostText;
    public InventoryManager inventoryManager;
    public TabManager tabManager;
    private int totalCost = 0;

    private void Start()
    {
        GoldSystem.instance.goldAmount = 450;
        goldText.text = GoldSystem.instance.goldAmount.ToString() + "g";
        totalCostText.text = "Total cost: " + totalCost.ToString() + "g";

        foreach (PurchaseGridSlot slot in gridSlots)
        {
            slot.icon.sprite = slot.boothItem.icon;
            GameObject obj = slot.gameObject;
            TMP_Text text = obj.GetComponentInChildren<TMP_Text>();
            text.text = slot.boothItem.cost.ToString() + "g";
        }
    }

    public void LoadItems(List<BoothItem> availableItems)
    {
        for (int i = 0; i < gridSlots.Count; i++)
        {
            if (i < availableItems.Count)
            {
                gridSlots[i].Initialize(availableItems[i], this);
            }
            else
            {
                gridSlots[i].gameObject.SetActive(false);
            }
        }

        RecalculateTotalCost();
    }

    public void PayForSelectedItems()
    {
        RecalculateTotalCost();

        if (totalCost == 0)
        {
            Debug.Log("No items selected to purchase.");
            return;
        }

        // Check if the player has enough gold
        if (GoldSystem.instance.goldAmount < totalCost)
        {
            Debug.Log("Not enough gold to purchase selected items!");
            Debug.LogWarning("Not enough gold!");

            DeselectAllItems();
            return;
        }

        // Subtract gold from your inventory
        GoldSystem.instance.goldAmount -= totalCost;
        goldText.text = GoldSystem.instance.goldAmount.ToString() + "g";

        List<BoothItem> itemsToBuy = new();

        // Add items to inventory
        foreach (var slot in gridSlots)
        {
            if (slot.IsSelected && slot.boothItem != null)
            {
                itemsToBuy.Add(slot.boothItem);
                inventoryManager.AddItem(slot.boothItem.gameObject);

                // Clear slot
                Destroy(slot.boothItem.gameObject);
                slot.boothItem = null;
                slot.icon.sprite = null;
                slot.costText.text = "";
                slot.toggle.isOn = false;
                slot.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }

        if (itemsToBuy.Count > 0)
        {
            inventoryManager.tabManager.OpenTabByType(itemsToBuy[0].type);
        }

        totalCost = 0;
        totalCostText.text = "Total cost: 0g";
    }

    public void RecalculateTotalCost()
    {
        foreach (var slot in gridSlots)
        {
            if (slot.IsSelected && slot.boothItem != null)
            {
                totalCost += slot.boothItem.cost;
            }
        }

        totalCostText.text = "Total cost: " + totalCost.ToString() + "g";
    }
    
    public void DeselectAllItems()
    {
        foreach (var slot in gridSlots)
        {
            if (slot.IsSelected)
            {
                slot.toggle.isOn = false;
            }
        }

        totalCost = 0;
        totalCostText.text = "Total cost: 0g";
    }
}
