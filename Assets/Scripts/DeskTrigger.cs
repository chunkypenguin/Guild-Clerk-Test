using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskTrigger : MonoBehaviour
{
    public List<GameObject> items;

    [SerializeField] CharacterSystem cs;
    [SerializeField] GoldSystem gs;
    [SerializeField] QuestSystem qs;
    [SerializeField] ItemSystem itemS;

    [SerializeField] TutorialScript josieS;
    [SerializeField] GregScript gregS;
    [SerializeField] FinchScript finchS;
    [SerializeField] AndyScript andyS;

    public bool canPressBell;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            //Debug.Log(other.gameObject.tag.ToString());
            //items.Add(other.gameObject);
            AddObject(other.gameObject);
        }
    }

    public void AddObject(GameObject obj)
    {
        if (!items.Contains(obj))
        {
            items.Add(obj);
            Debug.Log(obj.name + " added to the list.");
        }

        //if(cs.isQuest)
        //{
        //    if(canPressBell)
        //    {
        //        CheckForQuest();
        //        canPressBell = false;
        //    }
        //}

        //else if(cs.isReward)
        //{
        //    if (canPressBell)
        //    {
        //        CheckForReward();
        //    }
        //}

    }

    public void CheckForQuest()
    {
        if ((items.Find(item => item.name == "QuestA") != null) && items.Find(item => item.name == "QuestB") == null)
        {
            GameObject questRB = items.Find(item => item.name == "QuestA");
            qs.GetQuestRB(questRB);
            cs.currentCharacter.choseQuestA = true;
            cs.currentCharacter.choseQuestB = false;
            Debug.Log("QuestA found! Performing action...");
            // Add your action here
            cs.QuestADialogue();
            cs.pickedQ1A = true;
        }

        else if ((items.Find(item => item.name == "QuestB") != null) && items.Find(item => item.name == "QuestA") == null)
        {
            GameObject questRB = items.Find(item => item.name == "QuestB");
            qs.GetQuestRB(questRB);
            cs.currentCharacter.choseQuestB = true;
            cs.currentCharacter.choseQuestA = false;
            Debug.Log("QuestB found! Performing action...");
            // Add your action here
            cs.QuestBDialogue();
            cs.pickedQ1B = true;
        }

        else
        {
            Debug.Log("two or no quests on desk");
        }
    }

    public void CheckForReward()
    {
        if(gs.coins.Count > 0)
        {

            if (cs.currentCharacter.characterName != "Finch")
            {
                gs.isSuctionActive = true;
                GameObject questRB = items.Find(item => item.name == "QuestReturn");
                qs.GetQuestRB(questRB);
                Debug.Log("not finch");
            }
            else
            {
                if (!finchS.askingForGold)
                {
                    cs.pickedQ1A = true;
                    gs.isSuctionActive = true;
                    GameObject questRB = items.Find(item => item.name == "QuestReturn");
                    qs.GetQuestRB(questRB);
                    finchS.askingForGold = true;
                    Debug.Log("Is Finch");
                }
                else
                {
                    gs.isSuctionActive = true;
                }


            }

        }

        if (cs.currentCharacter.characterName == "Josie")
        {
            josieS.CheckForReward();
        }

        if (cs.currentCharacter.characterName == "Greg")
        {
            gregS.CheckForReward();
        }

        if (cs.currentCharacter.characterName == "Finch")
        {
            finchS.CheckForReward();
            Debug.Log("Finch Reward call");
        }

        if (cs.currentCharacter.characterName == "Andy")
        {
            andyS.CheckForReward();
        }
    }

    public void CanPressBell()
    {
        canPressBell = true;
    }

    public void CheckForItems()
    {
        if ((items.Find(item => item.name == cs.currentCharacter.ItemAName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null)
        {
            GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemAName);
            //qs.GetQuestRB(itemRB);
            itemS.GetItemRb(itemRB);
            cs.currentCharacter.choseItemA = true;
            cs.currentCharacter.choseItemB = false;
            // Add your action here
            //cs.QuestADialogue();
            cs.ItemADialogue();
            //cs.pickedQ1A = true;
        }

        else if ((items.Find(item => item.name == cs.currentCharacter.ItemBName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemAName) == null)
        {
            GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemBName);
            //qs.GetQuestRB(itemRB);
            itemS.GetItemRb(itemRB);
            cs.currentCharacter.choseItemB = true;
            cs.currentCharacter.choseItemA = false;
            // Add your action here
            //cs.QuestADialogue();
            cs.ItemBDialogue();
            //cs.pickedQ1A = true;
        }

        else
        {
            Debug.Log("two or no quests on desk");
        }
    }

}
