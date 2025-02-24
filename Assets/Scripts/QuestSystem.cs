using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HeneGames.DialogueSystem;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] GameObject questDescriptionA;
    [SerializeField] GameObject questDescriptionB;
    [SerializeField] DeskTrigger deskTrigger;
    [SerializeField] GameObject desk;

    [SerializeField] QuestObject[] questObjectA;
    [SerializeField] QuestObject[] questObjectB;

    [SerializeField] TMP_Text questATitle;
    [SerializeField] TMP_Text questADescription;
    [SerializeField] TMP_Text questAReward;

    [SerializeField] TMP_Text questBTitle;
    [SerializeField] TMP_Text questBDescription;
    [SerializeField] TMP_Text questBReward;



    [SerializeField] CharacterSystem cs;
    [SerializeField] DeskTrigger dt;

    public int a = 0;
    public int b = 1;

    private void Start()
    {
        UpdateQuests();
    }

    public void ShowQuestDescription(GameObject quest)
    {
        if(quest.name == "QuestA")
        {
            questDescriptionA.SetActive(true);
        }
        else if(quest.name == "QuestB")
        {
            questDescriptionB.SetActive(true);
        }
        
    }

    public void HideQuestDescription()
    {
        questDescriptionA.SetActive(false);
        questDescriptionB.SetActive(false);
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

        if (cs.isQuest)
        {
            dt.CheckForQuest();
        }

        else if (cs.isReward)
        {
            dt.CheckForReward();
        }

        deskTrigger.items.Clear();
    }

    public void UpdateQuests()
    {
        questATitle.text = cs.currentCharacter.quest[a].questTitle;
        questADescription.text = cs.currentCharacter.quest[a].questDescription;
        questAReward.text = cs.currentCharacter.quest[a].questReward;

        questBTitle.text = cs.currentCharacter.quest[b].questTitle;
        questBDescription.text = cs.currentCharacter.quest[b].questDescription;
        questBReward.text = cs.currentCharacter.quest[b].questReward;



        //questATitle.text = questObjectA[0].questTitle;
        //questADescription.text = questObjectA[0].questDescription;

        //questBTitle.text = questObjectB[0].questTitle;
        //questBDescription.text = questObjectB[0].questDescription;
    }
}
