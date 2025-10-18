using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReviewManager : MonoBehaviour
{
    public float overallAverageRep;

    [SerializeField] int totalRequestedGold;
    [SerializeField] int totalGoldGiven;
    public int totalGoldOverUnder;

    [SerializeField] List<float> characterGoldAccuracy;

    public float averageBias, averageAccuracy;


    public static ReviewManager instance;   
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //CallCharacterAccuracy();
            //OverallRep();
        }
    }

    public void GetReview()
    {
        CallCharacterAccuracy();
        OverallRep();
    }

    public void OverallRep()
    {
        overallAverageRep = DayReputationTracker.Instance.OverallAverageRep();
    }

    public void CalculateTotalRequestedGold() //I DONT THINK WE NEED THIS
    {
        totalRequestedGold =

         AndyScript.instance.andyRequestedGoldFetch
        + FinchScript.instance.finchRequestedGold
        + GregScript.instance.gregRequestedGold
        + JoleneScript.instance.joleneRequestedGold
        + KalinScript.instance.kalinRequestedGold
        + LotestScript.instance.lotestRequestedGold
        + MaggieScript.instance.maggieRequestedGold
        + NomiraScript.instance.nomiraRequestedGold
        + TahmasScript.instance.tahmasRequestedGold
        + VanelleScript.instance.vanelleRequestedGold;
        //+ZetoScript.instance.zetoRequestedGold;
    }

    public void CallCharacterAccuracy()
    {
        AndyScript.instance.AndyGold();
        NomiraScript.instance.NomiraGold();
        ZetoScript.instance.ZetoGold();
        AchillesScript.instance.AchillesGold();
        TahmasScript.instance.TahmasGold();
        LotestScript.instance.LotestGold();
        FinchScript.instance.FinchGold();
        GregScript.instance.GregGold();
        KalinScript.instance.KalinGold();
        VanelleScript.instance.VanelleGold();
        MaggieScript.instance.MaggieGold();
        JoleneScript.instance.JoleneGold();

        CalculateGoldReview();
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

        totalRequestedGold += goldRequested;
        totalGoldGiven += goldGiven;
    }

    public void CalculateGoldReview()
    {
        totalGoldOverUnder = totalGoldGiven - totalRequestedGold; //ex: gave 400 - total 600 = -200 (gave 200 less than asked)

        averageBias = characterGoldAccuracy.Average(); //- undergive too little!, + overgive too much!
        averageAccuracy = characterGoldAccuracy.Select(x => 1f - Mathf.Abs(x)).Average(); //how close, ignoring direction
    }
}
