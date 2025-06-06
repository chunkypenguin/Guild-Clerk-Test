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
    public GameObject TabButtonsList;
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
        
        TabButtonsList.transform.position = new Vector3(9999, 9999, 0);
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
        TabButtonsList.GetComponent<RectTransform>().anchoredPosition = new Vector3(145, 0, 0);
        foreach (var tab in tabs)
        {
            tab.panel.transform.position = new Vector3(9999, 9999, 0);
        }

        selectedTab.panel.transform.position = selectedTab.originalPosition;
        currentPanel = selectedTab.panel;
    }

    public void SelectTab(int index)
    {
        foreach (var tab in tabs)
        {
            tab.panel.transform.position = new Vector3(9999, 9999, 0);
        }

        switch (index)
        {
            case 0:
                tabs[0].panel.transform.position = tabs[0].originalPosition;
                break;
            case 1:
                tabs[1].panel.transform.position = tabs[1].originalPosition;
                break;
            case 2:
                tabs[2].panel.transform.position = tabs[2].originalPosition;
                break;
            case 3:
                tabs[3].panel.transform.position = tabs[3].originalPosition;
                break;
            case 4:
                tabs[4].panel.transform.position = tabs[4].originalPosition;
                break;
        }
    }
}
