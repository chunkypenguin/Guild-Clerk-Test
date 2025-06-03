using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndyScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] GoldSystem gs;

    public DialogueCharacter andyCharacter;

    [SerializeField] GameObject AndyImage;
    [SerializeField] Material andyIdle;
    [SerializeField] Material andyCoolImage;
    [SerializeField] Material andyPuppyEyesImage;
    [SerializeField] Material andySadEyesImage;
    [SerializeField] Material andyDissapointmentImage;
    [SerializeField] Material andyTearsImage;
    [SerializeField] Material andySobImage;
    [SerializeField] Material andyAngryImage;
    [SerializeField] Material andyEvilImage;
    [SerializeField] Material andyIrritatedImage;
    [SerializeField] Material andyBruhImage;
    [SerializeField] Material andyInjuredImage;
    [SerializeField] Material andyinjuredWinkImage;
    [SerializeField] Material andyWarCryImage;
    [SerializeField] Material andyWarCryInjuredImage;

    [SerializeField] GameObject AndyMomImage;

    [SerializeField] MeshRenderer mr;

    public bool partOneComplete;
    public bool partTwoComplete;

    public static AndyScript instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gs = GoldSystem.instance;
    }
    public void StartDialogue()
    {
        if (!partOneComplete)
        {
            cs.andyD1P1.StartNewDialogue(cs.dialogueTriggerScript);
            //partOneComplete = true;
        }
        else if (!partTwoComplete)
        {
            if (cs.currentCharacter.choseQuestA)//dragon quest (andys mom)
            {
                cs.andyD2Q1A.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else // fetch quest
            {
                cs.andyD2Q1B.StartNewDialogue(cs.dialogueTriggerScript);
                gameObject.GetComponent<CharacterReputation>().ModifyReputation(-1);
            }
            
        }
        else
        {
            cs.andyD3P1.StartNewDialogue(cs.dialogueTriggerScript);
            gameObject.GetComponent<CharacterReputation>().ModifyReputation(3);
        }
    }

    public void ChangeToMom()
    {

        if (cs.currentCharacter.choseQuestA)
        {
            AndyImage.SetActive(false);
            AndyMomImage.SetActive(true);
        }
        else
        {

        }

    }

    public void ChangeToAndy()
    {
        AndyImage.SetActive(true);
        AndyMomImage.SetActive(false);
        Debug.Log("Change Andy Pic");
    }

    public void CheckForReward()
    {
        int rep = 0;
        if (!partTwoComplete) //!cs.D3
        {
            cs.andyD2Q1BP2.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if(gs.goldAmount == 50)
        {
            cs.andyD3G1B.StartNewDialogue(cs.dialogueTriggerScript);
            rep = 1;
        }
        else if (gs.goldAmount > 50)
        {
            cs.andyD3G1B.StartNewDialogue(cs.dialogueTriggerScript);
            rep = 2;
        }
        else if(gs.goldAmount < 50)
        {
            cs.andyD3G1A.StartNewDialogue(cs.dialogueTriggerScript);
            rep = -1;
        }
        gameObject.GetComponent<CharacterReputation>().ModifyReputation(rep);
    }

    public void AndyIdle()
    {
        mr.material = andyIdle;
    }

    public void AndyCool()
    {
        mr.material = andyCoolImage;
    }

    public void AndyPuppyEyes()
    {
        mr.material = andyPuppyEyesImage;
    }

    public void AndySadEyes()
    {
        mr.material = andySadEyesImage;
    }

    public void AndyDisappointment()
    {
        mr.material = andyDissapointmentImage;
    }

    public void AndyTears()
    {
        mr.material = andyTearsImage;
    }

    public void AndySob()
    {
        mr.material = andySobImage;
    }

    public void AndyAngry()
    {
        mr.material = andyAngryImage;
    }

    public void AndyEvil()
    {
        mr.material = andyEvilImage;
    }

    public void AndyIrritated()
    {
        mr.material = andyIrritatedImage;
    }

    public void AndyBruh()
    {
        mr.material = andyBruhImage;
    }

    public void AndyInjuiredIdle()
    {
        mr.material = andyInjuredImage;
    }

    public void AndyInjuiredWink()
    {
        mr.material = andyinjuredWinkImage;
    }

    public void AndyWarCry()
    {
        mr.material = andyWarCryImage;
    }

    public void AndyWarCryInjured()
    {
        mr.material = andyWarCryInjuredImage;
    }
}
