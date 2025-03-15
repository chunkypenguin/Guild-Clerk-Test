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

    [SerializeField] GameObject AndyMomImage;

    [SerializeField] MeshRenderer mr;

    public void StartDialogue()
    {
        if (cs.D1 && !cs.D2)
        {
            cs.andyD1P1.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (!cs.D1 && cs.D2)
        {
            if (cs.currentCharacter.choseQuestA)
            {
                cs.andyD2Q1A.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else
            {
                cs.andyD2Q1B.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
        else if (cs.D3)
        {
            cs.andyD3P1.StartNewDialogue(cs.dialogueTriggerScript);
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
        if (!cs.D3)
        {
            cs.andyD2Q1BP2.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if(gs.goldAmount >= 50)
        {
            cs.andyD3G1B.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if(gs.goldAmount < 50)
        {
            cs.andyD3G1A.StartNewDialogue(cs.dialogueTriggerScript);
        }

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
}
