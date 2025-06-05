using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [System.Serializable]
    public class Tab
    {
        public Button button;
        public GameObject panel;
        public Vector3 originalPosition;
        public InventoryManager.ItemType type;
    }

    public Tab[] tabs;
    private GameObject currentPanel;

    void Start()
    {
        foreach (var tab in tabs)
        {
            tab.originalPosition = tab.panel.transform.position;
            tab.button.onClick.AddListener(() => OnTabSelected(tab));
        }

        foreach (var tab in tabs)
        {
            tab.panel.transform.position = new Vector3(9999, 9999, 0);
        }
    }

    public void OpenTabByType(InventoryManager.ItemType type)
    {
        foreach (var tab in tabs)
        {
            if (tab.type == type)
            {
                OnTabSelected(tab);
                break;
            }
        }
    }

    private void OnTabSelected(Tab selectedTab)
    {
        foreach (var tab in tabs)
        {
            tab.panel.transform.position = new Vector3(9999, 9999, 0);
        }

        selectedTab.panel.transform.position = selectedTab.originalPosition;
        currentPanel = selectedTab.panel;
    }
}
