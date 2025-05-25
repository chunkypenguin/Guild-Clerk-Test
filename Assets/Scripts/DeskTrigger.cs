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
    [SerializeField] MaggieScript maggieS;
    [SerializeField] JoleneScript joleneS;
    [SerializeField] TahmasScript tahmasS;
    [SerializeField] LotestScript lotestS;
    [SerializeField] LorneScript lorneS;
    [SerializeField] VanelleScript vanelleS;
    [SerializeField] NomiraScript nomiraS;
    [SerializeField] ZetoScript zetoS;

    [SerializeField] YarnScript yarnS;
    [SerializeField] RaspberriesScript raspS;

    public bool canPressBell;

    [SerializeField] ItemSystem itemScript;
    [SerializeField] movecam movecamScript;

    public static DeskTrigger instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        vanelleS = VanelleScript.instance;
        zetoS = ZetoScript.instance;
        nomiraS = NomiraScript.instance;
    }

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
        //Quest A
        if ((items.Find(item => item.name == "QuestA") != null) && items.Find(item => item.name == "QuestB") == null)
        {
            //nomira stuff
            if (cs.currentCharacter.characterName == "Nomira" && !nomiraS.brokeStaff)
            {
                cs.nomiraD1Q1AP1.StartNewDialogue(cs.dialogueTriggerScript);
                cs.currentCharacter.choseQuestA = true;
                cs.currentCharacter.choseQuestB = false;
                zetoS.zetoQuest = items.Find(item => item.name == "QuestA");
                cs.IsIdle();
                //nomiraS.brokeStaff = true;
            }
            else //everyone else
            {
                GameObject questRB = items.Find(item => item.name == "QuestA");
                qs.GetQuestRB(questRB);
                cs.currentCharacter.choseQuestA = true;
                cs.currentCharacter.choseQuestB = false;
                //Debug.Log("QuestA found! Performing action...");
                // Add your action here
                cs.QuestADialogue();
                cs.pickedQ1A = true;

                cs.IsIdle();// go back to idle task
            }
        }
        //Quest B
        else if ((items.Find(item => item.name == "QuestB") != null) && items.Find(item => item.name == "QuestA") == null)
        {
            //nomira stuff
            if (cs.currentCharacter.characterName == "Nomira" && !nomiraS.brokeStaff)
            {
                cs.nomiraD1Q1BP1.StartNewDialogue(cs.dialogueTriggerScript);
                cs.currentCharacter.choseQuestB = true;
                cs.currentCharacter.choseQuestA = false;
                zetoS.zetoQuest = items.Find(item => item.name == "QuestB");
                cs.IsIdle();
                //nomiraS.brokeStaff = true;
            }
            else //everyone else
            {
                GameObject questRB = items.Find(item => item.name == "QuestB");
                qs.GetQuestRB(questRB);
                cs.currentCharacter.choseQuestB = true;
                cs.currentCharacter.choseQuestA = false;
                //Debug.Log("QuestB found! Performing action...");
                // Add your action here
                cs.QuestBDialogue();
                cs.pickedQ1B = true;

                cs.IsIdle();// go back to idle task
            }
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
            if (cs.currentCharacter.characterName == "Finch")
            {
                if (!finchS.askingForGold)
                {
                    cs.pickedQ1A = true;
                    gs.isSuctionActive = true;
                    //GameObject questRB = items.Find(item => item.name == "QuestReturn");
                    //qs.GetQuestRB(questRB);
                    finchS.askingForGold = true;
                    Debug.Log("Is Finch");
                }
                else
                {
                    gs.isSuctionActive = true;
                }
            }
            else
            {
                gs.isSuctionActive = true;
                //GameObject questRB = items.Find(item => item.name == "QuestReturn");
                //qs.GetQuestRB(questRB);
                Debug.Log("not finch");
                //cs.IsIdle();
            }
            cs.IsIdle();
        }

        //always take return quest
        GameObject questRB = items.Find(item => item.name == "QuestReturn");
        if(questRB != null)
        {
            qs.GetQuestRB(questRB);
        }
       

        if (cs.currentCharacter.characterName == "Josie")
        {
            josieS.CheckForReward();
            cs.IsIdle();
        }

        if (cs.currentCharacter.characterName == "Greg")
        {
            gregS.CheckForReward();
            cs.IsIdle();
        }

        if (cs.currentCharacter.characterName == "Finch")
        {
            finchS.CheckForReward();
            Debug.Log("Finch Reward call");
            cs.IsIdle();
        }

        if (cs.currentCharacter.characterName == "Andy")
        {
            andyS.CheckForReward();
            cs.IsIdle();
        }

        if (cs.currentCharacter.characterName == "Maggie")
        {
            maggieS.CheckForReward();
            cs.IsIdle();
        }

        if (cs.currentCharacter.characterName == "Jolene")
        {
            joleneS.CheckForReward();
            cs.IsIdle();
        }

        if (cs.currentCharacter.characterName == "Tahmas")
        {
            tahmasS.CheckForReward();
            cs.IsIdle();
        }

        if (cs.currentCharacter.characterName == "Lotest")
        {
            lotestS.CheckForReward();
            cs.IsIdle();
        }

        if (cs.currentCharacter.characterName == "Vanelle")
        {
            vanelleS.CheckForReward();
            cs.IsIdle();
        }

        if (movecamScript.bottom)
        {
            movecamScript.UpButton();//move button up
        }
        
    }

    public void CanPressBell()
    {
        canPressBell = true;
    }

    public void CheckForItems()
    {
        //CHECK TO SEE FOR LORNE STUFF
        if (cs.currentCharacter.characterName == "Lorne")
        {
            if (cs.D1)
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
                    cs.IsIdle();
                }
            }

            else if (cs.D2)
            {
                //If lorne gets yarn
                if ((items.Find(item => item.name == cs.currentCharacter.ItemBName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemAName) == null)
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
                    cs.IsIdle();
                    lorneS.gaveYarn = true;
                }

                else
                {
                    Debug.Log("two or no quests on desk");
                    if (!yarnS.yarnOnDesk)
                    {
                        cs.lorneD2ItemRefuse.StartNewDialogue(cs.dialogueTriggerScript);
                        itemScript.ItemGlowOff();
                        cs.IsIdle();
                    }
                }
            }


        }

        else if(cs.currentCharacter.characterName == "Zeke")
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
                cs.IsIdle();
            }

            else
            {
                Debug.Log("two or no quests on desk");
                if (!raspS.raspberriesOnDesk)
                {

                    if (!raspS.refusedOnce)
                    {
                        cs.zekeD3Refuse1.StartNewDialogue(cs.dialogueTriggerScript);
                        raspS.refusedOnce = true;
                    }
                    else
                    {
                        cs.zekeD3Refuse2.StartNewDialogue(cs.dialogueTriggerScript);
                        itemScript.ItemGlowOff();
                        
                    }
                    cs.IsIdle();
                }
            }
        }

        else if (cs.currentCharacter.characterName == "Nomira")
        {
            if (cs.currentCharacter.choseQuestA)
            {
                //weapon
                if ((items.Find(item => item.name == cs.currentCharacter.ItemAName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null)
                {
                    cs.nomiraD1Q1AEWeapon.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemA = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemAName);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }
                //divine arcane focus
                else if ((items.Find(item => item.name == cs.currentCharacter.ItemBName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemAName) == null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null)
                {
                    cs.nomiraD1Q1BEDivine.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemB = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemBName);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }
                //arcane focus
                else if ((items.Find(item => item.name == cs.currentCharacter.ItemANameA) != null) && items.Find(item => item.name == cs.currentCharacter.ItemAName) == null && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null)
                {
                    cs.nomiraD1Q1ABEOther.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemAA = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemANameA);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }

            }
            else if (cs.currentCharacter.choseQuestB)
            {
                //arcane focus
                if ((items.Find(item => item.name == cs.currentCharacter.ItemANameA) != null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null)
                {
                    cs.nomiraD1Q1ABEOther.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemAA = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemAName);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }
                //divine arcane focus
                else if ((items.Find(item => item.name == cs.currentCharacter.ItemBName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null)
                {
                    cs.nomiraD1Q1BEDivine.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemB = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemBName);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }
            }
        }

        //REGULAR STUFF FOR OTHER CHARACTERS
        //item a
        else if ((items.Find(item => item.name == cs.currentCharacter.ItemAName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null)
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
            cs.IsIdle();
        }
        //ietm b
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
            cs.IsIdle();
        }

        else
        {
            Debug.Log("two or no quests on desk");
        }
    }

}
