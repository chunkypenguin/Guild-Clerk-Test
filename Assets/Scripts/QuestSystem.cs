using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HeneGames.DialogueSystem;
using DG.Tweening;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] GameObject questDescriptionA;
    [SerializeField] GameObject questDescriptionB;
    [SerializeField] GameObject questDescriptionR;
    [SerializeField] GameObject bloodyQuestDescription;

    [SerializeField] DeskTrigger deskTrigger;
    [SerializeField] GameObject desk;

    [SerializeField] QuestObject[] questObjectA;
    [SerializeField] QuestObject[] questObjectB;

    //QUEST A
    [SerializeField] TMP_Text questATitle;
    [SerializeField] TMP_Text questADescription;
    [SerializeField] TMP_Text questAReward;
    //QUEST B
    [SerializeField] TMP_Text questBTitle;
    [SerializeField] TMP_Text questBDescription;
    [SerializeField] TMP_Text questBReward;
    //QUEST R(eturn)
    [SerializeField] TMP_Text questRTitle;
    [SerializeField] TMP_Text questRDescription;
    [SerializeField] TMP_Text questRReward;
    //bloody version
    [SerializeField] TMP_Text bquestRTitle;
    [SerializeField] TMP_Text bquestRDescription;
    [SerializeField] TMP_Text bquestRReward;

    [SerializeField] GameObject returnQuest;
    [SerializeField] GameObject bloodyReturnQuest;
    [SerializeField] Transform returnPoint;

    [SerializeField] CharacterSystem cs;
    [SerializeField] DeskTrigger dt;

    [SerializeField] GameObject questAHolder;
    [SerializeField] GameObject questBHolder;

    [SerializeField] GameObject visualQuests;
    [SerializeField] ParticleSystem poofFX;

    public int a = 0;
    public int b = 1;
    public int c = 2;
    // VACUUM SYSTEM
    [SerializeField] Rigidbody rb;
    [SerializeField] Rigidbody rb2;
    [SerializeField] bool isSuctionActive = false;
    [SerializeField] Transform vacuumPoint; // The position coins should be sucked toward
    [SerializeField] float suckForce = 20f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float collectDistance = 0.5f;
    private Vector3 startScale; // Store initial scale
    private float initialDistance;

    private void Start()
    {
        //UpdateQuests();
    }

    private void FixedUpdate()
    {
        if (!isSuctionActive) return;

        rb.useGravity = false;
        
        rb2.useGravity = false;

        Collider col = rb.gameObject.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Vector3 direction = (cs.currentCharacterObject.transform.position - rb.transform.position).normalized;
        rb.AddForce(direction * suckForce, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        // Shrink the object as it moves towards the vacuum point
        float distance = Vector3.Distance(rb.transform.position, cs.currentCharacterObject.transform.position);
        float shrinkFactor = Mathf.Clamp01(distance / initialDistance); // Normalize shrink between 1 and 0
        rb.transform.localScale = startScale * shrinkFactor; // Apply scale reduction

        // Check if close enough to be collected
        if (Vector3.Distance(rb.transform.position, cs.currentCharacterObject.transform.position) < collectDistance)
        {
            CollectQuest();
        }
    }

    public void GetQuestRB(GameObject questObject)
    {
        rb = questObject.GetComponent<Rigidbody>();
        rb2 = rb.transform.parent.gameObject.GetComponent<Rigidbody>();
        startScale = rb.transform.localScale; // Store initial scale
        initialDistance = Vector3.Distance(rb.transform.position, cs.currentCharacterObject.transform.position); // Get starting distance
        isSuctionActive = true;

        Invoke(nameof(CollectQuest), 2f); //prevent quest getting stuck
    }

    private void CollectQuest()
    {
        isSuctionActive = false;

        // Destroy the quest
        //questObject.GetComponent<ItemFloorScript>().ResetItem();
        //questObject.transform.parent.gameObject.SetActive(false);
        CancelInvoke(nameof(CollectQuest));
        //rb.transform.parent.gameObject.SetActive(false); //was before below
        returnQuest.SetActive(false);
        Debug.Log("Quest returned");
        questAHolder.SetActive(false); 
        questBHolder.SetActive(false);

        visualQuests.SetActive(true);

        rb.gameObject.GetComponent<ItemFloorScript>().ResetItem();

    }

    public void DropQuest(GameObject questObject)
    {
        questObject.SetActive(true);
        questObject.GetComponent<Rigidbody>().AddForce((Vector3.right * 5f) + Vector3.up * 5f, ForceMode.Impulse);

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
        else if(quest.name == "QuestReturn")
        {
            questDescriptionR.SetActive(true);
        }
        else if(quest.name == "BloodyQuestReturn")
        {
            bloodyQuestDescription.SetActive(true);
        }
        
    }

    public void HideQuestDescription()
    {
        questDescriptionA.SetActive(false);
        questDescriptionB.SetActive(false);
        questDescriptionR.SetActive(false);
        bloodyQuestDescription.SetActive(false);
    }

    public void FinalizeItems() //WHEN BELL PRESSED I THINK
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

        else if(cs.isEquipment)
        {
            dt.CheckForItems();
        }

        else if(cs.isIdle)
        {
            //Do nothing
        }

        deskTrigger.items.Clear();
    }

    public void UpdateQuests()
    {
        visualQuests.SetActive(false); //*poof*
        poofFX.Play();

        Debug.Log("update quests");
        questAHolder.SetActive(true);
        questBHolder.SetActive(true);

        if (cs.currentCharacter.characterName == "Andy" && cs.currentCharacter.choseQuestB)
        {
            Debug.Log("switch to alternative quest B text");

            questATitle.text = cs.currentCharacter.quest[a].questTitle;
            questADescription.text = cs.currentCharacter.quest[a].questDescription;
            questAReward.text = cs.currentCharacter.quest[a].questReward;

            questBTitle.text = cs.currentCharacter.quest[c].questTitle;
            questBDescription.text = cs.currentCharacter.quest[c].questDescription;
            questBReward.text = cs.currentCharacter.quest[c].questReward;
        }

        else
        {
            questATitle.text = cs.currentCharacter.quest[a].questTitle;
            questADescription.text = cs.currentCharacter.quest[a].questDescription;
            questAReward.text = cs.currentCharacter.quest[a].questReward;

            questBTitle.text = cs.currentCharacter.quest[b].questTitle;
            questBDescription.text = cs.currentCharacter.quest[b].questDescription;
            questBReward.text = cs.currentCharacter.quest[b].questReward;
        }





        //questATitle.text = questObjectA[0].questTitle;
        //questADescription.text = questObjectA[0].questDescription;

        //questBTitle.text = questObjectB[0].questTitle;
        //questBDescription.text = questObjectB[0].questDescription;
    }

    public void UpdateReturnQuest()
    {
        if (cs.currentCharacter.choseQuestA)
        {
            if(cs.currentCharacter.characterName != "Andy")
            {
                Debug.Log("throw out Quest A");
                returnQuest.SetActive(true);
                returnQuest.transform.position = returnPoint.position;
                //returnQuest.transform.parent.gameObject.GetComponent<Rigidbody>().AddForce((Vector3.right * 5f) + Vector3.up * 5f, ForceMode.Impulse);
                questRTitle.text = cs.currentCharacter.quest[a].questTitle;
                questRDescription.text = cs.currentCharacter.quest[a].questDescription;
                questRReward.text = cs.currentCharacter.quest[a].questReward;
            }
            else
            {
                Debug.Log("throw out Quest A");
                bloodyReturnQuest.SetActive(true);
                bloodyReturnQuest.transform.position = returnPoint.position;
                //returnQuest.transform.parent.gameObject.GetComponent<Rigidbody>().AddForce((Vector3.right * 5f) + Vector3.up * 5f, ForceMode.Impulse);
                questRTitle.text = cs.currentCharacter.quest[a].questTitle;
                questRDescription.text = cs.currentCharacter.quest[a].questDescription;
                questRReward.text = cs.currentCharacter.quest[a].questReward;
            }


        }
        else if (cs.currentCharacter.choseQuestB)
        {
            Debug.Log("throw out Quest B");
            returnQuest.SetActive(true);
            returnQuest.transform.position = returnPoint.position;
            //returnQuest.transform.parent.gameObject.GetComponent<Rigidbody>().AddForce((Vector3.right * 5f) + Vector3.up * 5f, ForceMode.Impulse);
            questRTitle.text = cs.currentCharacter.quest[b].questTitle;
            questRDescription.text = cs.currentCharacter.quest[b].questDescription;
            questRReward.text = cs.currentCharacter.quest[b].questReward;
        }
    }
}
