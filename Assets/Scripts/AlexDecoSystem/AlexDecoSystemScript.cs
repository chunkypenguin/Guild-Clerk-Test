using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexDecoSystemScript : MonoBehaviour
{
    [Header("Ceiling Decor")]
    [SerializeField] List<GameObject> ceilingDecorObjects; //decoration to be activated
    [SerializeField] List<GameObject> ceilingDecorHighlights; //Button Highlights (for activated decor)
    [SerializeField] List<bool> celingDecorLocked; //lock bools 
    [SerializeField] List<int> celingDecorCosts; //object pricing

    int playerGold;

    public void ButtonClick(int x) //on click of button
    {
        //check if locked
        if (celingDecorLocked[x])
        {
            //check if has enough gold to purchase unlock
            playerGold = DayReputationTracker.Instance.GetGold();
            if(playerGold >= celingDecorCosts[x])
            {
                celingDecorLocked[x] = false; //unlock item
            }
            else
            {
                //Do nothing
            }
        }
        else
        {
            //Deactivate all other decoration objects and their highlights
            Deactivate();
            //activate this buttons corresponding decoration and highlight
            Activate(x);
        }
    }

    public void Deactivate()
    {
        foreach (GameObject item in ceilingDecorObjects) //set all decor inactive
        {
            item.SetActive(false);
        }
        foreach (GameObject item in ceilingDecorHighlights) //set all highlights inactive
        {
            item.SetActive(false);
        }
    }

    public void Activate(int x)
    {
        ceilingDecorObjects[x].SetActive(true); //turn on corresponding object
        ceilingDecorHighlights[x].SetActive(true); //turn on corresponding butto highlight

    }
}
