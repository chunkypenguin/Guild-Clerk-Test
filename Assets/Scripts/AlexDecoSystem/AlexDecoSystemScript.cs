using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using UnityEngine.UI;

public class AlexDecoSystemScript : MonoBehaviour
{
    [Header("Ceiling Decor")]
    [SerializeField] List<GameObject> ceilingDecorObjects; //decoration to be activated
    [SerializeField] List<GameObject> ceilingDecorButtons; //reference to buttons
    [SerializeField] List<bool> ceilingDecorLocked; //lock bools 
    [SerializeField] List<bool> ceilingDecorActive; //active bools 
    [SerializeField] List<int> ceilingDecorCosts; //object pricing

    [Header("Knick Knacks")]
    [SerializeField] List<GameObject> knickKnackObjects; //decoration to be activated
    [SerializeField] List<GameObject> knickKnackButtons; //reference to buttons
    [SerializeField] List<bool> knickKnackLocked; //lock bools 
    [SerializeField] List<bool> knickKnackActive; //active bools 
    [SerializeField] List<int> knickKnackCosts; //object pricing

    [Header("Music Record")]
    [SerializeField] List<GameObject> recordButtons;
    [SerializeField] List<AudioSource> recordAudioSource;
    [SerializeField] AudioSource previousAudioSource;
    [SerializeField] List<bool> recordLocked;
    [SerializeField] List<bool> recordActive;
    [SerializeField] List<int> recordCost;
    [SerializeField] float fadeTime;

    [Header("Clerk Amenities")]

    [Header("Desk Bell Mats")]
    [SerializeField] MeshRenderer deskBellRenderer;
    [SerializeField] List<Material> baseDeskBellMats;
    [SerializeField] List<Material> topDeskBellMats;
    [SerializeField] List<Material> botDeskBellMats;
    [SerializeField] List<GameObject> deskBellButtons;
    [SerializeField] List<bool> deskBellLocked; //lock bools 
    [SerializeField] List<bool> deskBellActive; //active bools 
    [SerializeField] List<int> deskBellCosts; //object pricing

    [Header("Quest Board Mats")]
    [SerializeField] MeshRenderer questYGCRenderer;
    [SerializeField] List<Material> baseQuestBoardMats;
    [SerializeField] List<Material> frameQuestBoardMats;
    [SerializeField] List<Material> corkQuestBoardMats;
    [SerializeField] List<GameObject> questBoardButtons;
    [SerializeField] List<bool> questBoardLocked; //lock bools 
    [SerializeField] List<bool> questBoardActive; //active bools 
    [SerializeField] List<int> questBoardCosts; //object pricing

    [Header("Table Cloth")]
    [SerializeField] List<GameObject> tableClothObjects; //decoration to be activated
    [SerializeField] List<GameObject> tableClothButtons;
    [SerializeField] List<bool> tableClothLocked; //lock bools 
    [SerializeField] List<bool> tableClothActive; //active bools
    [SerializeField] List<int> tableClothCosts; //object pricing

    [Header("Textures")]

    [SerializeField] MeshRenderer mainYGCRenderer;

    [Header("Wood Grain")]
    [SerializeField] MeshRenderer doorRenderer;
    [SerializeField] List<Material> woodGrainTextures;
    [SerializeField] List<Material> pillarMats;
    [SerializeField] List<Material> counterTopMats;
    [SerializeField] List<Material> doorMats;
    [SerializeField] List<Material> doorFrameMats;
    [SerializeField] List<Material> questBoardFrameMats;
    [SerializeField] List<Material> questCounterTopMats;
    [SerializeField] List<GameObject> woodGrainButtons;
    [SerializeField] List<bool> woodGrainLocked; //lock bools 
    [SerializeField] List<bool> woodGrainActive; //active bools 
    [SerializeField] List<int> woodGrainCosts; //object pricing

    [Header("Planks")]
    [SerializeField] MeshRenderer shelfRenderer;
    [SerializeField] MeshRenderer drawerRenderer;
    [SerializeField] MeshRenderer questCounterRenderer;
    [SerializeField] List<Material> plankTextures;
    [SerializeField] List<Material> counterMats;
    [SerializeField] List<Material> shelfMats;
    [SerializeField] List<Material> drawerMats;
    [SerializeField] List<Material> questCounterMats;
    [SerializeField] List<GameObject> planksButtons;
    [SerializeField] List<bool> planksLocked; //lock bools 
    [SerializeField] List<bool> planksActive; //active bools 
    [SerializeField] List<int> planksCosts; //object pricing

    [Header("Walls")]
    [SerializeField] List<Material> baseWallMats;
    [SerializeField] List<Material> wallMats;
    [SerializeField] List<GameObject> wallsButtons;
    [SerializeField] List<bool> wallsLocked; //lock bools 
    [SerializeField] List<bool> wallsActive; //active bools 
    [SerializeField] List<int> wallsCosts; //object pricing

    private Image image;
    [SerializeField] float iconLockedAlpha;
    [SerializeField] float iconUnlockedAlpha;
    int playerGold;

    [SerializeField] AudioSource purchaseAudio;

    //CEILING DECOR 
    public void CelingDecorButtonClick(int x) //on click of button
    {
        //check if locked
        if (ceilingDecorLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if (playerGold >= ceilingDecorCosts[x])
            {
                //spend
                DayReputationTracker.Instance.SpendGold(ceilingDecorCosts[x]);

                ceilingDecorLocked[x] = false; //unlock item
                ceilingDecorButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
                image = ceilingDecorButtons[x].transform.Find("Icon").gameObject.GetComponent<Image>();//get reference to icon
                Color c = image.color;
                c.a = 255f;
                image.color = c;

                purchaseAudio.Play();
            }
            else
            {
                //Do nothing
            }
        }
        else
        {
            if (ceilingDecorActive[x]) //if decor is active
            {
                //Deactivate
                CeilingDecorDeactivate(x);
            }
            else //if decor is inactive
            {
                //Deactivate all other decoration objects and their highlights
                CeilingDecorDeactivate(x);
                //activate this buttons corresponding decoration and highlight
                CeilingDecorActivate(x);
            }


        }
    }

    public void CeilingDecorDeactivate(int x)
    {
        foreach (GameObject item in ceilingDecorObjects) //set all decor inactive
        {
            item.SetActive(false);
        }
        foreach (GameObject item in ceilingDecorButtons) //set all highlights inactive
        {
            item.transform.Find("Highlight").gameObject.SetActive(false);
        }
        for (int i = 0; i < ceilingDecorActive.Count; i++)
        {
            ceilingDecorActive[i] = false;
        }
    }

    public void CeilingDecorActivate(int x)
    {
        ceilingDecorObjects[x].SetActive(true); //turn on corresponding object
        ceilingDecorButtons[x].transform.Find("Highlight").gameObject.SetActive(true); //turn on corresponding button highlight
        //ceilingDecorHighlights[x].SetActive(true); //turn on corresponding butto highlight
        ceilingDecorActive[x] = true;
    }

    //KNICK KNACKS
    public void KnickKnackButtonClick(int x) //on click of button
    {
        //check if locked
        if (knickKnackLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();

            if (x == 5)//if is mystic mushroom
            {
                if (GregScript.instance.gaveMysticMushroom)
                {
                    GregScript.instance.SpendMushroom();
                    knickKnackLocked[x] = false; //unlock item
                    knickKnackButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
                    image = knickKnackButtons[x].transform.Find("Icon").gameObject.GetComponent<Image>();//get reference to icon
                    Color c = image.color;
                    c.a = 255f;
                    image.color = c;

                    purchaseAudio.Play();
                }
            } 
            else if (playerGold >= knickKnackCosts[x]) //regular knick knack
            {
                DayReputationTracker.Instance.SpendGold(knickKnackCosts[x]);
                knickKnackLocked[x] = false; //unlock item
                knickKnackButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
                image = knickKnackButtons[x].transform.Find("Icon").gameObject.GetComponent<Image>();//get reference to icon
                Color c = image.color;
                c.a = 255f;
                image.color = c;

                purchaseAudio.Play();
            }
            else
            {
                //Do nothing
            }
        }
        else
        {
            if (knickKnackActive[x]) //if decor is active
            {
                //Deactivate
                KnickKnackDeactivate(x);
            }
            else //if decor is inactive
            {
                //activate
                KnickKnackActivate(x);
            }
        }
    }

    public void KnickKnackDeactivate(int x)
    {
        knickKnackObjects[x].SetActive(false); //turn on corresponding object
        knickKnackButtons[x].transform.Find("Highlight").gameObject.SetActive(false);
        knickKnackActive[x] = false;
    }

    public void KnickKnackActivate(int x)
    {
        knickKnackObjects[x].SetActive(true); //turn on corresponding object
        knickKnackButtons[x].transform.Find("Highlight").gameObject.SetActive(true); //turn on corresponding button highlight
        knickKnackActive[x] = true;

    }

    //MUSIC RECORD
    public void MusicRecordButtonClick(int x)
    {
        if (recordLocked[x])
        {
            playerGold = DayReputationTracker.Instance.GetGold();
            if(playerGold >= recordCost[x])
            {
                DayReputationTracker.Instance.SpendGold(recordCost[x]);
                recordLocked[x] = false;
                recordButtons[x].transform.Find("Lock").gameObject.SetActive(false);
                image = recordButtons[x].transform.Find("Icon").gameObject.GetComponent<Image>();//get reference to icon
                Color c = image.color;
                c.a = 255f;
                image.color = c;

                purchaseAudio.Play();
            }
        }
        else
        {
            if (recordActive[x])
            {
                //Do nothing if clicked on an already playing record
            }
            else
            {
                DeactivateRecords();
                recordActive[x] = true;
                recordButtons[x].transform.Find("Highlight").gameObject.SetActive(true);
                TransitionToNew(x);

            }
        }
    }

    void DeactivateRecords()
    {
        for(int x = 0; x < recordActive.Count; x++) //set all records inactive
        {
            recordActive[x] = false;
        }
        foreach(GameObject record in recordButtons) //dehighlight all records
        {
            record.transform.Find("Highlight").gameObject.SetActive(false);
        }
    }
    void TransitionToNew(int x)
    {
        previousAudioSource.DOFade(0f, fadeTime);
        recordAudioSource[x].DOFade(0.75f, fadeTime);
        previousAudioSource = recordAudioSource[x];
    }

    //CLERK AMENITIES

    //DESK BELL MATS
    public void DeskBellButtonClick(int x) //on click of button
    {
        //check if locked
        if (deskBellLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if (playerGold >= deskBellCosts[x])
            {
                DayReputationTracker.Instance.SpendGold(deskBellCosts[x]);
                deskBellLocked[x] = false; //unlock item
                deskBellButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
                image = deskBellButtons[x].transform.Find("Icon").gameObject.GetComponent<Image>();//get reference to icon
                Color c = image.color;
                c.a = 255f;
                image.color = c;
                purchaseAudio.Play();
            }
            else
            {
                //Do nothing
            }
        }
        else
        {
            if (deskBellActive[x]) //if decor is active
            {
                //Deactivate
                //DeskBellDeactivate(x);
            }
            else //if decor is inactive
            {
                DeskBellDeactivate();
                //activate
                DeskBellActivate(x);
            }
        }
    }

    public void DeskBellDeactivate()
    {
        //NEW
        for (int x = 0; x < deskBellActive.Count; x++) //set all records inactive
        {
            deskBellActive[x] = false;
        }
        foreach (GameObject bell in deskBellButtons) //dehighlight all records
        {
            bell.transform.Find("Highlight").gameObject.SetActive(false);
        }
    }

    public void DeskBellActivate(int x)
    {
        var mats = deskBellRenderer.materials;  // copy
        mats[0] = new Material(topDeskBellMats[x]);       // optional instantiate
        //mats[1] = new Material(botDeskBellMats[x]);
        deskBellRenderer.materials = mats;     // apply

        deskBellButtons[x].transform.Find("Highlight").gameObject.SetActive(true);
        deskBellActive[x] = true;
    }

    //QUEST BOARD MATS
    public void QuestBoardButtonClick(int x) //on click of button
    {
        //check if locked
        if (questBoardLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if (playerGold >= questBoardCosts[x])
            {
                DayReputationTracker.Instance.SpendGold(questBoardCosts[x]);
                questBoardLocked[x] = false; //unlock item
                questBoardButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
                image = questBoardButtons[x].transform.Find("Icon").gameObject.GetComponent<Image>();//get reference to icon
                Color c = image.color;
                c.a = 255f;
                image.color = c;
                purchaseAudio.Play();
            }
            else
            {
                //Do nothing
            }
        }
        else
        {
            if (questBoardActive[x]) //if decor is active
            {
                //Deactivate
                //QuestBoardDeactivate(x);
            }
            else //if decor is inactive
            {
                QuestBoardDeactivate();
                //activate
                QuestBoardActivate(x);
            }
        }
    }

    public void QuestBoardDeactivate()
    {
        //NEW
        for (int x = 0; x < questBoardActive.Count; x++) //set all records inactive
        {
            questBoardActive[x] = false;
        }
        foreach (GameObject questboard in questBoardButtons) //dehighlight all records
        {
            questboard.transform.Find("Highlight").gameObject.SetActive(false);
        }
    }

    public void QuestBoardActivate(int x)
    {
        var mats = questYGCRenderer.materials;  // copy
        //mats[6] = new Material(frameQuestBoardMats[x]);     // optional instantiate
        mats[7] = new Material(corkQuestBoardMats[x]);
        questYGCRenderer.materials = mats;// apply

        questBoardButtons[x].transform.Find("Highlight").gameObject.SetActive(true);
        questBoardActive[x] = true;
    }

    //TABLE CLOTH
    public void TableClothButtonClick(int x) //on click of button
    {
        //check if locked
        if (tableClothLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if (playerGold >= tableClothCosts[x])
            {
                DayReputationTracker.Instance.SpendGold(tableClothCosts[x]);
                tableClothLocked[x] = false; //unlock item
                tableClothButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
                image = tableClothButtons[x].transform.Find("Icon").gameObject.GetComponent<Image>();//get reference to icon
                Color c = image.color;
                c.a = 255f;
                image.color = c;
                purchaseAudio.Play();
            }
            else
            {
                //Do nothing
            }
        }
        else
        {
            if (tableClothActive[x]) //if decor is active
            {
                //Deactivate
                TableClothDeactivate();
            }
            else //if decor is inactive
            {
                //Deactivate all other decoration objects and their highlights
                TableClothDeactivate();
                //activate this buttons corresponding decoration and highlight
                TableClothActivate(x);
            }
            ////Deactivate all other decoration objects and their highlights
            //TableClothDeactivate();
            ////activate this buttons corresponding decoration and highlight
            //TableClothActivate(x);
        }
    }

    public void TableClothDeactivate()
    {
        foreach (GameObject item in tableClothObjects) //set all decor inactive
        {
            item.SetActive(false);
        }
        foreach (GameObject item in tableClothButtons) //set all highlights inactive
        {
            item.transform.Find("Highlight").gameObject.SetActive(false);
        }
        for (int x = 0; x < woodGrainActive.Count; x++) //set all records inactive
        {
            tableClothActive[x] = false;
        }
        //tableClothActive[x] = false;
    }

    public void TableClothActivate(int x)
    {
        tableClothObjects[x].SetActive(true); //turn on corresponding object
        tableClothButtons[x].transform.Find("Highlight").gameObject.SetActive(true); //turn on corresponding button highlight
        tableClothActive[x] = true;

    }

    //TEXTURES

    //wood grain
    public void WGTexturesButtonClick(int x) //on click of button
    {
        //check if locked
        if (woodGrainLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if (playerGold >= woodGrainCosts[x])
            {
                DayReputationTracker.Instance.SpendGold(woodGrainCosts[x]);
                woodGrainLocked[x] = false; //unlock item
                woodGrainButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
                image = woodGrainButtons[x].transform.Find("Icon").gameObject.GetComponent<Image>();//get reference to icon
                Color c = image.color;
                c.a = 255f;
                image.color = c;
                purchaseAudio.Play();
            }
            else
            {
                //Do nothing
            }
        }
        else
        {
            if (woodGrainActive[x]) //if decor is active
            {
                //Deactivate
                //WGTexturesDeactivate();
            }
            else //if decor is inactive
            {
                WGTexturesDeactivate();
                //activate
                WGTexturesActivate(x);
            }
        }
    }

    public void WGTexturesDeactivate()
    {
        //NEW
        for (int x = 0; x < woodGrainActive.Count; x++) //set all records inactive
        {
            woodGrainActive[x] = false;
        }
        foreach (GameObject woodgrain in woodGrainButtons) //dehighlight all records
        {
            woodgrain.transform.Find("Highlight").gameObject.SetActive(false);
        }
    }

    public void WGTexturesActivate(int x)
    {
        //ygc
        var mats = mainYGCRenderer.materials;  // copy
        mats[0] = new Material(pillarMats[x]);     // pillars
        mats[3] = new Material(counterTopMats[x]); //counter tops
        mats[4] = new Material(doorFrameMats[x]); //door frame
        mainYGCRenderer.materials = mats;// apply

        //door
        var doormats = doorRenderer.materials;
        doormats[0] = new Material(doorMats[x]);
        doorRenderer.materials = doormats;

        //Quest frame
        var questboardmats = questYGCRenderer.materials;
        questboardmats[6] = new Material(frameQuestBoardMats[x]);
        questYGCRenderer.materials = questboardmats;

        //quest counter
        var qcmats = questCounterRenderer.materials;
        qcmats[1] = new Material(questCounterTopMats[x]);
        questCounterRenderer.materials = qcmats;

        woodGrainButtons[x].transform.Find("Highlight").gameObject.SetActive(true);
        woodGrainActive[x] = true;
    }

    //PLANKS
    public void PlankTexturesButtonClick(int x) //on click of button
    {
        //check if locked
        if (planksLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if (playerGold >= planksCosts[x])
            {
                DayReputationTracker.Instance.SpendGold(planksCosts[x]);
                planksLocked[x] = false; //unlock item
                planksButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
                image = planksButtons[x].transform.Find("Icon").gameObject.GetComponent<Image>();//get reference to icon
                Color c = image.color;
                c.a = 255f;
                image.color = c;
                purchaseAudio.Play();
            }
            else
            {
                //Do nothing
            }
        }
        else
        {
            if (planksActive[x]) //if decor is active
            {
                //Deactivate
                //PlankTexturesDeactivate();
            }
            else //if decor is inactive
            {
                PlankTexturesDeactivate();
                //activate
                PlankTexturesActivate(x);
            }
        }
    }

    public void PlankTexturesDeactivate()
    {
        //NEW
        for (int x = 0; x < planksActive.Count; x++) //set all records inactive
        {
            planksActive[x] = false;
        }
        foreach (GameObject plank in planksButtons) //dehighlight all records
        {
            plank.transform.Find("Highlight").gameObject.SetActive(false);
        }
    }

    public void PlankTexturesActivate(int x)
    {
        //ygc
        var mats = mainYGCRenderer.materials;  // copy
        mats[1] = new Material(counterMats[x]); // pillars
        mats[5] = new Material(counterMats[x]); //counter tops
        mainYGCRenderer.materials = mats;// apply

        //drawer
        var drawermats = drawerRenderer.materials;
        drawermats[1] = new Material(drawerMats[x]);
        drawerRenderer.materials = drawermats;

        //shelf
        var shelfmats = shelfRenderer.materials;
        shelfmats[0] = new Material(shelfMats[x]);
        shelfRenderer.materials = shelfmats;

        //quest counter
        var qcmats = questCounterRenderer.materials;
        qcmats[0] = new Material(questCounterMats[x]);
        questCounterRenderer.materials = qcmats;

        planksButtons[x].transform.Find("Highlight").gameObject.SetActive(true);
        planksActive[x] = true;
    }

    //walls
    public void WallsTexturesButtonClick(int x) //on click of button
    {
        //check if locked
        if (wallsLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if (playerGold >= wallsCosts[x])
            {
                DayReputationTracker.Instance.SpendGold(wallsCosts[x]);
                wallsLocked[x] = false; //unlock item
                wallsButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
                image = wallsButtons[x].transform.Find("Icon").gameObject.GetComponent<Image>();//get reference to icon
                Color c = image.color;
                c.a = 255f;
                image.color = c;
                purchaseAudio.Play();
            }
            else
            {
                //Do nothing
            }
        }
        else
        {
            if (wallsActive[x]) //if decor is active
            {
                //Deactivate
                //WallsTexturesDeactivate();
            }
            else //if decor is inactive
            {
                WallsTexturesDeactivate();
                //activate
                WallsTexturesActivate(x);
            }
        }
    }

    public void WallsTexturesDeactivate()
    {
        //NEW
        for (int x = 0; x < wallsActive.Count; x++) //set all records inactive
        {
            wallsActive[x] = false;
        }
        foreach (GameObject wall in wallsButtons) //dehighlight all records
        {
            wall.transform.Find("Highlight").gameObject.SetActive(false);
        }
    }

    public void WallsTexturesActivate(int x)
    {
        //ygc
        var mats = mainYGCRenderer.materials;  // copy
        mats[2] = new Material(wallMats[x]); // pillars
        mainYGCRenderer.materials = mats;// apply

        wallsButtons[x].transform.Find("Highlight").gameObject.SetActive(true);
        wallsActive[x] = true;
    }
}
