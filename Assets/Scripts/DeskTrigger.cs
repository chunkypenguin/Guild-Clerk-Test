using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] KalinScript kalinS;

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
        kalinS = KalinScript.instance;
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
                cs.zetoCharacter.choseQuestA = true;
                cs.zetoCharacter.choseQuestB = false;

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
                cs.zetoCharacter.choseQuestB = true;
                cs.zetoCharacter.choseQuestA = false;

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

            //if both quests
            if((items.Find(item => item.name == "QuestB") != null) && items.Find(item => item.name == "QuestA") != null)
            {
                //josie dialogue "remember what josie said..."
                //set at start of dia is idle, then at end back to is quest
                cs.josieTwoQuestReminder.StartNewDialogue(cs.dialogueTriggerScript);
            }

        }
    }

    public void CheckForReward()
    {


        //always take return quest
        GameObject questRB = items.Find(item => item.name == "QuestReturn");
        if(questRB != null)
        {
            //qs.GetQuestRB(questRB); //commenting to instead *POOF* away quest
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

        if (cs.currentCharacter.characterName == "Andy Cheesington")
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

        if (cs.currentCharacter.characterName == "Lotest Altall")
        {
            lotestS.CheckForReward();
            cs.IsIdle();
        }

        if (cs.currentCharacter.characterName == "Vanelle")
        {
            vanelleS.CheckForReward();
            cs.IsIdle();
        }

        if(cs.currentCharacter.characterName == "Zeto Storma")
        {
            zetoS.CheckForReward();
            cs.IsIdle();
        }

        if (cs.currentCharacter.characterName == "Nomira")
        {
            nomiraS.CheckForReward();
            cs.IsIdle();
        }

        if (cs.currentCharacter.characterName == "Kalin")
        {
            kalinS.CheckForReward();
            cs.IsIdle();
        }

        if(cs.currentCharacter.characterName == "Achilles")
        {
            AchillesScript.instance.CheckForReward();
            cs.IsIdle();
        }

        if (movecamScript.bottom)
        {
            movecamScript.UpButton();//move button up
        }

        //Suck up the gold (WAS UP TOP OF THIS FUNCTION) before 10/11
        //trying to have vanelles quest stay when you give her less gold
        if (gs.coins.Count > 0)
        {
            if (cs.currentCharacter.characterName == "Finch")
            {
                if (!finchS.askingForGold)
                {
                    cs.pickedQ1A = true;
                    gs.isSuctionActive = true;
                    gs.CollectCoinsCheck();
                    //GameObject questRB = items.Find(item => item.name == "QuestReturn");
                    //qs.GetQuestRB(questRB);
                    finchS.askingForGold = true;
                    Debug.Log("Is Finch");
                }
                else
                {
                    gs.isSuctionActive = true;
                    gs.CollectCoinsCheck();
                }
            }
            else
            {
                gs.isSuctionActive = true;
                gs.CollectCoinsCheck();
                //GameObject questRB = items.Find(item => item.name == "QuestReturn");
                //qs.GetQuestRB(questRB);
                Debug.Log("not finch");
                //cs.IsIdle();
            }
            cs.IsIdle();
        }
        else //if no gold rewarded, still make poof return quest
        {
            GoldSystem.instance.QuestPoof();
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
            if (!LorneScript.instance.partOneComplete)
            {
                if ((items.Find(item => item.name == cs.currentCharacter.ItemAName) != null) && (items.Find(item => item.name == cs.currentCharacter.ItemBName) == null))
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

                    LorneScript.instance.partOneComplete = true;
                }
                else if((items.Find(item => item.name == cs.currentCharacter.ItemAName) == null) && (items.Find(item => item.name == cs.currentCharacter.ItemBName) != null))
                {
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemBName);
                    //qs.GetQuestRB(itemRB);
                    itemS.GetItemRb(itemRB);
                    cs.currentCharacter.choseItemA = true;
                    cs.currentCharacter.choseItemB = false;
                    // Add your action here
                    //cs.QuestADialogue();
                    cs.ItemADialogue();
                    //cs.pickedQ1A = true;
                    cs.IsIdle();

                    LorneScript.instance.partOneComplete = true;
                }
            }

            else if (!LorneScript.instance.partTwoComplete)
            {
                //If lorne gets yarn
                if ((items.Find(item => item.name == cs.currentCharacter.ItemANameA) != null) && items.Find(item => item.name == cs.currentCharacter.ItemAName) == null && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null)
                {
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemANameA);
                    //qs.GetQuestRB(itemRB);
                    itemS.GetItemRb(itemRB);
                    cs.currentCharacter.choseItemB = true; //this is yarn (at least functionality)
                    cs.currentCharacter.choseItemA = false;
                    // Add your action here
                    //cs.QuestADialogue();
                    cs.ItemBDialogue();
                    //cs.pickedQ1A = true;
                    cs.IsIdle();
                    lorneS.gaveYarn = true;

                    LorneScript.instance.partTwoComplete = true;
                }

                else
                {
                    Debug.Log("two or no quests on desk");
                    if (!yarnS.yarnOnDesk)
                    {
                        cs.lorneD2ItemRefuse.StartNewDialogue(cs.dialogueTriggerScript);
                        itemScript.ItemGlowOff();
                        cs.IsIdle();

                        LorneScript.instance.partTwoComplete = true;
                    }
                }

            }


        }

        else if(cs.currentCharacter.characterName == "Zeke")
        {
            if ((items.Find(item => item.name == cs.currentCharacter.ItemAName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null && items.Find(item => item.name == cs.currentCharacter.ItemBNameB) == null && items.Find(item => item.name == cs.currentCharacter.ItemCName) == null)
            {

                GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemAName);
                //qs.GetQuestRB(itemRB);
                itemS.GetItemRb(itemRB);
                cs.currentCharacter.choseItemA = true;
                //cs.currentCharacter.choseItemB = false;
                //cs.currentCharacter.choseItemAA = false;
                // Add your action here
                //cs.QuestADialogue();
                cs.ItemADialogue();
                //cs.pickedQ1A = true;
                cs.IsIdle();
            }

            else if((items.Find(item => item.name == cs.currentCharacter.ItemAName) == null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) != null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null && items.Find(item => item.name == cs.currentCharacter.ItemBNameB) == null && items.Find(item => item.name == cs.currentCharacter.ItemCName) == null)
            {
                GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemBName);
                //qs.GetQuestRB(itemRB);
                itemS.GetItemRb(itemRB);
                //cs.currentCharacter.choseItemA = false;
                cs.currentCharacter.choseItemB = true;
                //cs.currentCharacter.choseItemAA = false;
                // Add your action here
                //cs.QuestADialogue();
                cs.ItemBDialogue();
                //cs.pickedQ1A = true;
                cs.IsIdle();
            }
            else if((items.Find(item => item.name == cs.currentCharacter.ItemAName) == null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) != null && items.Find(item => item.name == cs.currentCharacter.ItemBNameB) == null && items.Find(item => item.name == cs.currentCharacter.ItemCName) == null)
            {
                GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemANameA);
                //qs.GetQuestRB(itemRB);
                itemS.GetItemRb(itemRB);
                //cs.currentCharacter.choseItemA = false;
                //cs.currentCharacter.choseItemB = false;
                cs.currentCharacter.choseItemAA = true;
                // Add your action here
                //cs.QuestADialogue();
                //cs.ItemBDialogue();
                cs.zekeD3FeedLornePotion.StartNewDialogue(cs.dialogueTriggerScript);
                //cs.pickedQ1A = true;
                cs.IsIdle();
            }
            else if ((items.Find(item => item.name == cs.currentCharacter.ItemAName) == null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null && items.Find(item => item.name == cs.currentCharacter.ItemBNameB) != null && items.Find(item => item.name == cs.currentCharacter.ItemCName) == null)
            {
                GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemBNameB);
                //qs.GetQuestRB(itemRB);
                itemS.GetItemRb(itemRB);
                cs.currentCharacter.choseItemBB = true;
                // Add your action here
                //cs.QuestADialogue();
                //cs.ItemBDialogue();
                //cs.zekeD3FeedLornePotion.StartNewDialogue(cs.dialogueTriggerScript);
                cs.zekeFeedPie.StartNewDialogue(cs.dialogueTriggerScript);
                //cs.pickedQ1A = true;
                cs.IsIdle();
            }
            else if ((items.Find(item => item.name == cs.currentCharacter.ItemAName) == null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null && items.Find(item => item.name == cs.currentCharacter.ItemBNameB) == null && items.Find(item => item.name == cs.currentCharacter.ItemCName) != null)
            {
                GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemCName);
                //qs.GetQuestRB(itemRB);
                itemS.GetItemRb(itemRB);
                cs.currentCharacter.choseItemC = true;
                // Add your action here
                //cs.QuestADialogue();
                //cs.ItemBDialogue();
                cs.zekeFeedGrimNote.StartNewDialogue(cs.dialogueTriggerScript);
                //cs.pickedQ1A = true;
                cs.IsIdle();
            }
            else
            {
                if((items.Find(item => item.name == cs.currentCharacter.ItemAName) == null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null && items.Find(item => item.name == cs.currentCharacter.ItemBNameB) == null && items.Find(item => item.name == cs.currentCharacter.ItemCName) == null)
                {
                    ZekeScript.instance.RefuseRaspberries();
                    cs.IsIdle();
                }


                
                //Debug.Log("two or no quests on desk");
                //if (!raspS.raspberriesOnDesk)
                //{

                //    if (!raspS.refusedOnce)
                //    {
                //        cs.zekeD3Refuse1.StartNewDialogue(cs.dialogueTriggerScript);
                //        raspS.refusedOnce = true;
                //    }
                //    else
                //    {
                //        cs.zekeD3Refuse2.StartNewDialogue(cs.dialogueTriggerScript);
                //        itemScript.ItemGlowOff();
                        
                //    }
                //    cs.IsIdle();
                //}
            }
        }

        else if (cs.currentCharacter.characterName == "Josie")
        {
            if ((items.Find(item => item.name == cs.currentCharacter.ItemAName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null)
            {
                GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemAName);
                itemS.GetItemRb(itemRB);
                cs.currentCharacter.choseItemA = true;
                cs.currentCharacter.choseItemB = false;
                cs.ItemADialogue();
                cs.IsIdle();
            }

            else
            {
                //refuse
                cs.josieKalinERefuse.StartNewDialogue(cs.dialogueTriggerScript);
                itemScript.ItemGlowOff();
                cs.IsIdle();
            }
        }

        else if (cs.currentCharacter.characterName == "Nomira")
        {
            if (cs.currentCharacter.choseQuestB) //QUEST B (GOLEM)
            {
                //weapon
                if ((items.Find(item => item.name == cs.currentCharacter.ItemAName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null && items.Find(item => item.name == cs.currentCharacter.ItemBNameB) == null)
                {
                    cs.nomiraD1Q1AEWeapon.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemA = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemAName);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }
                //divine arcane focus
                else if ((items.Find(item => item.name == cs.currentCharacter.ItemBName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemAName) == null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null && items.Find(item => item.name == cs.currentCharacter.ItemBNameB) == null)
                {
                    cs.nomiraD1Q1BEDivine.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemB = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemBName);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }
                //arcane focus
                else if ((items.Find(item => item.name == cs.currentCharacter.ItemANameA) != null) && items.Find(item => item.name == cs.currentCharacter.ItemAName) == null && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null && items.Find(item => item.name == cs.currentCharacter.ItemBNameB) == null)
                {
                    cs.nomiraD1Q1ABEOther.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemAA = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemANameA);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }
                //druidic focus
                else if ((items.Find(item => item.name == cs.currentCharacter.ItemBNameB) != null) && items.Find(item => item.name == cs.currentCharacter.ItemAName) == null && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null)
                {
                    cs.nomiraD1Q1ABEOther.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemAA = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemBNameB);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }

            }
            else if (cs.currentCharacter.choseQuestA) //QUEST A (RUINS)
            {
                //arcane focus
                if ((items.Find(item => item.name == cs.currentCharacter.ItemANameA) != null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null && items.Find(item => item.name == cs.currentCharacter.ItemBNameB) == null)
                {
                    cs.nomiraD1Q1ABEOther.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemAA = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemANameA);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }
                else if ((items.Find(item => item.name == cs.currentCharacter.ItemBNameB) != null) && items.Find(item => item.name == cs.currentCharacter.ItemBName) == null && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null)
                {
                    cs.nomiraD1Q1ABEOther.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemAA = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemBNameB);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }
                //divine arcane focus
                else if ((items.Find(item => item.name == cs.currentCharacter.ItemBName) != null) && items.Find(item => item.name == cs.currentCharacter.ItemANameA) == null && items.Find(item => item.name == cs.currentCharacter.ItemBNameB) == null)
                {
                    cs.nomiraD1Q1BEDivine.StartNewDialogue(cs.dialogueTriggerScript);
                    cs.currentCharacter.choseItemB = true;
                    GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemBName);
                    itemS.GetItemRb(itemRB);
                    cs.IsIdle();
                }
            }
        }

        else if (cs.currentCharacter.characterName == "Achilles")
        {
            //get coin
            //dont put into idle, let quest do that

            //if coin is on desk and either quest is on the desk too
            if(items.Find(item => item.name == cs.currentCharacter.ItemAName) != null)
            {
                //suck up coin
                //say dialogue about coin
                AchillesScript.instance.achillesCoinGiven = true;
                cs.achillesP1CoinGiven.StartNewDialogue(cs.dialogueTriggerScript); //isQuest and checkquest at end of this dialogue
                cs.currentCharacter.choseItemA = true;
                GameObject itemRB = items.Find(item => item.name == cs.currentCharacter.ItemAName);
                itemS.GetItemRb(itemRB);
                cs.IsIdle();
            }
            else if(items.Find(item => item.name == cs.currentCharacter.ItemAName) == null)
            {
                CheckForQuest();
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
            Debug.Log("two or no items on desk");

            //CheckForDuplicateItems();

        }

        CheckForDuplicateItems(); //should only work when more than two items are on the desk which should never be allowed to turn in
    }

    private void CheckForDuplicateItems()
    {
        string[] targetNames = {
        cs.currentCharacter.ItemAName,
        cs.currentCharacter.ItemBName,
        cs.currentCharacter.ItemANameA,
        cs.currentCharacter.ItemBNameB,
        cs.currentCharacter.ItemCName
        };

        // Count how many of the target items exist in the list
        int totalMatches = items.Count(item => targetNames.Contains(item.name));

        if (totalMatches > 1)
        {
            cs.josieTwoItemReminder.StartNewDialogue(cs.dialogueTriggerScript);
        }
        Debug.Log(totalMatches);
    }

}
