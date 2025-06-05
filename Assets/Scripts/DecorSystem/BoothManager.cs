using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothManager : MonoBehaviour
{
    public static BoothManager Instance;
    public Transform boothArea;
    public GameObject deskBell;
    public InventoryManager inventory;
    public bool purchasabledItems = false;

    private List<GameObject> spawnedItems = new();

    void Awake() => Instance = this;

    public void SpawnBoothItems(List<ChecklistUI.ChecklistOption> options)
    {
        foreach (var opt in options)
        {
            GameObject item = Instantiate(opt.boothItemPrefab, boothArea.transform);
            item.AddComponent<ItemTooltipTrigger>().Initialize(opt.label, opt.price);
            spawnedItems.Add(item);
            purchasabledItems = true;
        }
    }

    public void FinalizePurchase(GameObject item)
    {
        Debug.Log("delete purchase");
        purchasabledItems = false;
        //inventory.AddItem(item);
        spawnedItems.Remove(item);
        //Destroy(item);
    }
}
