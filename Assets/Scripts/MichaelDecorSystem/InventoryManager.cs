using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public Sprite icon;
    public InventoryManager.ItemType type;
    public GameObject visualPrefab;
}

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public enum ItemType { Texture, Clerk, KnickKnack, Ceiling }
    public TabManager tabManager;

    [Header("Slots and Icon Prefabs")]
    public Sprite lockedIcon;
    public List<InventorySlot> textureSlots;
    public List<InventorySlot> clerkSlots;
    public List<InventorySlot> knickKnackSlots;
    public List<InventorySlot> ceilingSlots;

    private Dictionary<string, GameObject> activeItems = new();
    private List<GameObject> selectedBoothItems = new();

    void Start()
    {
        //InitializeSlots(textureSlots);
        //InitializeSlots(clerkSlots);
        //InitializeSlots(knickKnackSlots);
        //InitializeSlots(ceilingSlots);

        InitializePreFilledSlots(textureSlots, ItemType.Texture);
        InitializePreFilledSlots(clerkSlots, ItemType.Clerk);
        InitializePreFilledSlots(knickKnackSlots, ItemType.KnickKnack);
        InitializePreFilledSlots(ceilingSlots, ItemType.Ceiling);
    }

    // void InitializeSlots(List<InventorySlot> slots)
    // {
    //     foreach (var slot in slots)
    //     {
    //         slot.SetLocked(lockedIcon);
    //     }
    // }

    void InitializePreFilledSlots(List<InventorySlot> slots, ItemType type)
    {
        foreach (var slot in slots)
        {
            if (slot.isFilled)
            {
                BoothItem data = slot.GetComponent<BoothItem>();
                slot.SetItem(data.icon,
                onSelect: () => HandleSelection(slot, data),
                onCancel: () => HandleDeselection(slot, data));

                selectedBoothItems.Add(slot.gameObject);
            }
        }
    }

    public void AddItem(GameObject boothItem)
    {
        BoothItem data = boothItem.GetComponent<BoothItem>();
        if (data == null)
        {
            Debug.LogError("BoothItem component missing!");
            return;
        }

        List<InventorySlot> slots = GetSlotList(data.type);
        InventorySlot slot = slots.Find(s => s.isUnlocked && !s.isFilled);

        if (slot == null)
        {
            Debug.LogWarning("No available inventory slot!");
            return;
        }

        slot.SetItem(data.icon,
            onSelect: () => HandleSelection(slot, data),
            onCancel: () => HandleDeselection(slot, data));

        selectedBoothItems.Add(boothItem);

        Debug.Log("Added item: " + data);
    }

    void HandleSelection(InventorySlot slot, BoothItem data)
    {
        if (activeItems.TryGetValue(data.type.ToString(), out GameObject oldVisual))
        {
            Destroy(oldVisual);
        }

        GameObject newVisual = Instantiate(data.visualPrefab);
        activeItems[data.type.ToString()] = newVisual;

        slot.GetComponent<Image>().color = Color.yellow;
    }

    void HandleDeselection(InventorySlot slot, BoothItem data)
    {
        if (activeItems.TryGetValue(data.type.ToString(), out GameObject visual))
        {
            Destroy(visual);
            activeItems.Remove(data.type.ToString());
        }
        slot.GetComponent<Image>().color = Color.white;
    }

    List<InventorySlot> GetSlotList(ItemType type)
    {
        switch (type)
        {
            case ItemType.Texture:
                return textureSlots;
            case ItemType.Clerk:
                return clerkSlots;
            case ItemType.KnickKnack:
                return knickKnackSlots;
            case ItemType.Ceiling:
                return ceilingSlots;
            default:
                return null;
        }
    }
}
