using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistUI : MonoBehaviour
{
    public GameObject optionPrefab;
    public Transform optionList;
    public Button confirmButton;

    [System.Serializable]
    public class ChecklistOption
    {
        public string id;
        public string label;
        public int price;
        public GameObject boothItemPrefab;
    }

    public List<ChecklistOption> options;
    private Dictionary<string, bool> selectedOptions = new();

    void Start()
    {
        foreach (var option in options)
        {
            var item = Instantiate(optionPrefab, optionList);
            item.GetComponentInChildren<TMP_Text>().text = option.label;

            Toggle toggle = item.GetComponentInChildren<Toggle>();
            toggle.onValueChanged.AddListener(isOn => {
                selectedOptions[option.id] = isOn;
            });

            selectedOptions[option.id] = false;
        }

        confirmButton.onClick.AddListener(ConfirmSelection);
    }

    void ConfirmSelection()
    {
        List<ChecklistOption> chosen = options.FindAll(opt => selectedOptions[opt.id]);
        BoothManager.Instance.SpawnBoothItems(chosen);
        gameObject.SetActive(false);
    }
}
