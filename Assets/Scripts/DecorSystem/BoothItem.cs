using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothItem : MonoBehaviour
{
    public string itemName;
    public int cost;
    public Sprite icon;
    public InventoryManager.ItemType type;
    public GameObject visualPrefab;
}
