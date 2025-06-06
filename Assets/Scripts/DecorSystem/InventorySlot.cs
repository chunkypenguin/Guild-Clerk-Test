using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage;
    public Button selectButton;
    public Button cancelButton;

    public bool isUnlocked = true;
    public bool isFilled = false;

    public void SetLocked(Sprite lockedIcon)
    {
        isUnlocked = false;
        isFilled = false;
        iconImage.sprite = lockedIcon;
        selectButton.interactable = false;
        cancelButton.gameObject.SetActive(false);
    }

    public void SetItem(Sprite icon, System.Action onSelect, System.Action onCancel)
    {
        isUnlocked = true;
        isFilled = true;
        iconImage.sprite = icon;
        selectButton.interactable = true;
        cancelButton.gameObject.SetActive(true);

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => onSelect());

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() => onCancel());
    }

    public void ClearSlot(Sprite lockedIcon)
    {
        isFilled = false;
        SetLocked(lockedIcon);
    }
}
