using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    [SerializeField] private GameObject tooltipBox;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text priceText;

    private void Awake() => Instance = this;

    public void ShowTooltip(string itemName, int price, Vector3 worldPos)
    {
        tooltipBox.SetActive(true);
        itemNameText.text = itemName;
        priceText.text = $"${price}";
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        tooltipBox.transform.position = screenPos + new Vector3(120, 50);
    }

    public void HideTooltip() => tooltipBox.SetActive(false);
}
