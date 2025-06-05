using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseGridSlot : MonoBehaviour
{
    public Image icon;
    public Toggle toggle;
    public TMP_Text costText;
    public BoothItem boothItem;
    public bool IsSelected => toggle.isOn;
    private PurchaseGridManager gridManager;

    private void Start()
    {
        gridManager = GameObject.Find("PurchaseGridManager").GetComponent<PurchaseGridManager>();
    }

    public void Initialize(BoothItem data, PurchaseGridManager manager)
    {
        boothItem = data;
        icon.sprite = data.icon;
        costText.text = data.cost.ToString() + "g";
        toggle.isOn = false;

        gameObject.SetActive(true);
        gridManager = manager;
    }

    public void OnSlotToggleChanged(bool isOn)
    {
        if (gridManager != null)
        {
            gridManager.RecalculateTotalCost();
        }
    }
}
