using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

public class AlexDecoSystemScript : MonoBehaviour
{
    [Header("Ceiling Decor")]
    [SerializeField] List<GameObject> ceilingDecorObjects; //decoration to be activated
    [SerializeField] List<GameObject> ceilingDecorButtons; //reference to buttons
    [SerializeField] List<bool> ceilingDecorLocked; //lock bools 
    [SerializeField] List<int> ceilingDecorCosts; //object pricing

    [Header("Knick Knacks")]
    [SerializeField] List<GameObject> knickKnackObjects; //decoration to be activated
    [SerializeField] List<GameObject> knickKnackButtons; //reference to buttons
    [SerializeField] List<bool> knickKnackLocked; //lock bools 
    [SerializeField] List<bool> knickKnackActive; //active bools 
    [SerializeField] List<int> knickKnackCosts; //object pricing

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
    [SerializeField] MeshRenderer YGCRenderer;
    [SerializeField] List<Material> frameQuestBoardMats;
    [SerializeField] List<Material> corkQuestBoardMats;
    [SerializeField] List<GameObject> questBoardButtons;
    [SerializeField] List<bool> questBoardLocked; //lock bools 
    [SerializeField] List<bool> questBoardActive; //active bools 
    [SerializeField] List<int> questBoardCosts; //object pricing

    int playerGold;

    //CEILING DECOR 
    public void CelingDecorButtonClick(int x) //on click of button
    {
        //check if locked
        if (ceilingDecorLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if(playerGold >= ceilingDecorCosts[x])
            {
                ceilingDecorLocked[x] = false; //unlock item
                ceilingDecorButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
            }
            else
            {
                //Do nothing
            }
        }
        else
        {
            //Deactivate all other decoration objects and their highlights
            CeilingDecorDeactivate();
            //activate this buttons corresponding decoration and highlight
            CeilingDecorActivate(x);
        }
    }

    public void CeilingDecorDeactivate()
    {
        foreach (GameObject item in ceilingDecorObjects) //set all decor inactive
        {
            item.SetActive(false);
        }
        foreach (GameObject item in ceilingDecorButtons) //set all highlights inactive
        {
            item.transform.Find("Highlight").gameObject.SetActive(false);
        }
    }

    public void CeilingDecorActivate(int x)
    {
        ceilingDecorObjects[x].SetActive(true); //turn on corresponding object
        ceilingDecorButtons[x].transform.Find("Highlight").gameObject.SetActive(true); //turn on corresponding button highlight
        //ceilingDecorHighlights[x].SetActive(true); //turn on corresponding butto highlight

    }

    //KNICK KNACKS
    public void KnickKnackButtonClick(int x) //on click of button
    {
        //check if locked
        if (knickKnackLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if (playerGold >= knickKnackCosts[x])
            {
                knickKnackLocked[x] = false; //unlock item
                knickKnackButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
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

    //CLERK AMENITIES
    public void DeskBellButtonClick(int x) //on click of button
    {
        //check if locked
        if (deskBellLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if (playerGold >= deskBellCosts[x])
            {
                deskBellLocked[x] = false; //unlock item
                deskBellButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
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
                DeskBellDeactivate(x);
            }
            else //if decor is inactive
            {
                DeskBellDeactivate(x);
                //activate
                DeskBellActivate(x);
            }
        }
    }

    public void DeskBellDeactivate(int x)
    {
        var mats = deskBellRenderer.materials;
        mats[0] = baseDeskBellMats[0];
        mats[1] = baseDeskBellMats[1];
        deskBellRenderer.materials = mats;

        foreach (GameObject item in deskBellButtons) //set all highlights inactive
        {
            item.transform.Find("Highlight").gameObject.SetActive(false);
            deskBellActive[0] = false;
            deskBellActive[1] = false;
            deskBellActive[2] = false;
        }
        //deskBellActive[x] = false; //not active
    }

    public void DeskBellActivate(int x)
    {
        var mats = deskBellRenderer.materials;  // copy
        mats[0] = new Material(topDeskBellMats[x]);       // optional instantiate
        mats[1] = new Material(botDeskBellMats[x]);
        deskBellRenderer.materials = mats;     // apply

        deskBellButtons[x].transform.Find("Highlight").gameObject.SetActive(true);
        deskBellActive[x] = true;
    }

    public void QuestBoardButtonClick(int x) //on click of button
    {
        //check if locked
        if (questBoardLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if (playerGold >= questBoardCosts[x])
            {
                questBoardLocked[x] = false; //unlock item
                questBoardButtons[x].transform.Find("Lock").gameObject.SetActive(false); //remove lock image from button
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
                DeskBellDeactivate(x);
            }
            else //if decor is inactive
            {
                DeskBellDeactivate(x);
                //activate
                DeskBellActivate(x);
            }
        }
    }
}
