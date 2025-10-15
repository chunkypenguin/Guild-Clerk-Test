using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReviewManager : MonoBehaviour
{
    [SerializeField] float overallAverageRep;

    [SerializeField] int totalRequestedGold;

    [SerializeField] List<float> characterGoldAccuracy;

    [SerializeField] float averageBias, averageAccuracy;

    public void OverallRep()
    {
        overallAverageRep = DayReputationTracker.Instance.OverallAverageRep();
    }

    public void CalculateTotalRequestedGold()
    {
        totalRequestedGold =

         AndyScript.instance.andyRequestedGold
        + FinchScript.instance.finchRequestedGold
        + GregScript.instance.gregRequestedGold
        + JoleneScript.instance.joleneRequestedGold
        + KalinScript.instance.kalinRequestedGold
        + LotestScript.instance.lotestRequestedGold
        + MaggieScript.instance.maggieRequestedGold
        + NomiraScript.instance.nomiraRequestedGold
        + TahmasScript.instance.tahmasRequestedGold
        + VanelleScript.instance.vanelleRequestedGold
        +ZetoScript.instance.zetoRequestedGold;
    }

    public void CharacterGoldAccuracyCalculator(int goldGiven, int goldRequested)
    {
        if (goldRequested == 0)
        {
            characterGoldAccuracy.Add(0f);
            return;
        }

        float goldPercentageDifference = (goldGiven -  goldRequested) / (float)goldRequested;
        characterGoldAccuracy.Add(goldPercentageDifference);
    }

    public void CalculateGoldReview()
    {
        averageBias = characterGoldAccuracy.Average(); //- undergive too little!, + overgive too much!
        averageAccuracy = characterGoldAccuracy.Select(x => 1f - Mathf.Abs(x)).Average(); //how close, ignoring direction
    }
}
