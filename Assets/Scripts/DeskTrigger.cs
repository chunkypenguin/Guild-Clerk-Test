using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskTrigger : MonoBehaviour
{
    public List<GameObject> items;

    [SerializeField] CharacterSystem cs;
    [SerializeField] GoldSystem gs;

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

        if(cs.isQuest)
        {
            if(canPressBell)
            {
                CheckForQuest();
                canPressBell = false;
            }
        }

        else if(cs.isReward)
        {
            if (canPressBell)
            {
                CheckForReward();
            }
        }

    }

    public void CheckForQuest()
    {
        if ((items.Find(item => item.name == "QuestA") != null) && items.Find(item => item.name == "QuestB") == null)
        {
            Debug.Log("QuestA found! Performing action...");
            // Add your action here
            cs.josieD1Q1AP1.StartNewDialogue(cs.dialogueTriggerScript);
            cs.pickedQ1A = true;
        }

        else if ((items.Find(item => item.name == "QuestB") != null) && items.Find(item => item.name == "QuestA") == null)
        {
            Debug.Log("QuestB found! Performing action...");
            // Add your action here
            cs.josieD1Q1BP1.StartNewDialogue(cs.dialogueTriggerScript);
            cs.pickedQ1B = true;
        }

        else
        {
            Debug.Log("two or no quests on desk");
        }
    }

    public void CheckForReward()
    {
        if(cs.pickedQ1A)
        {
            if (gs.goldAmount == 7)
            {
                //Do this
                cs.josieD1G1BP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if(gs.goldAmount > 7)
            {
                //do this
                cs.josieD1G1AP1.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if(gs.goldAmount < 7)
            {
                //do this
                cs.josieD1G1CP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
        else if(cs.pickedQ1B)
        {
            if (gs.goldAmount == 4)
            {
                //Do this
                cs.josieD1G1BP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 4)
            {
                //do this
                cs.josieD1G1AP1.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 4)
            {
                //do this
                cs.josieD1G1CP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
        
    }

    public void CanPressBell()
    {
        canPressBell = true;
    }
}
