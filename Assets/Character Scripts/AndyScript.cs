using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndyScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    [SerializeField] GameObject AndyImage;
    [SerializeField] Material andyIdle;
    [SerializeField] Material andyCoolImage;
    [SerializeField] Material andyPuppyEyesImage;
    [SerializeField] Material andySadEyesImage;
    [SerializeField] Material andyDissapointmentImage;
    [SerializeField] Material andyTearsImage;
    [SerializeField] Material andySobImage;
    [SerializeField] Material andyAngryImage;

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

    public void CheckForReward()
    {
        cs.andyD2Q1BP2.StartNewDialogue(cs.dialogueTriggerScript);
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
}
