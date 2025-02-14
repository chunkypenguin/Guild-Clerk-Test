using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] GameObject questDescription;
    [SerializeField] DeskTrigger deskTrigger;
    [SerializeField] GameObject desk;


    public void ShowQuestDescription()
    {
        questDescription.SetActive(true);
    }

    public void HideQuestDescription()
    {
        questDescription.SetActive(false);
    }

    public void FinalizeItems()
    {
        desk.SetActive(true);
        Invoke(nameof(CountItemsOnDesk), 0.2f);
    }

    public void CountItemsOnDesk()
    {
        desk.SetActive(false);
        //Debug.Log("You chose" + deskTrigger.items[0]);
        Debug.Log("You give: ");
        foreach (GameObject obj in deskTrigger.items)
        {
            Debug.Log(obj.name);
        }

        deskTrigger.items.Clear();

    }
}
