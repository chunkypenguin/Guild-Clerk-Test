using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HeneGames.DialogueSystem;

public class NomiraScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;

    [SerializeField] GoldSystem gs;

    [SerializeField] MeshRenderer mr;

    public bool brokeStaff;

    public static NomiraScript instance;

    public bool partOneComplete;

    public GameObject druidStaffGlow;
    public GameObject divineStaffGlow;
    public GameObject cosmicStaffGlow;
    public GameObject weaponGlow;

    //emotes
    [SerializeField] Material peekMat;
    [SerializeField] Material posessedMat;
    [SerializeField] Material wickedSmileMat;

    [SerializeField] AudioSource audioSource;

    public bool curseDivine;
    public bool curseOther;
    public bool golemOther;
    public bool golemSword;

    public DialogueCharacter nomiraCharacter;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        cs = CharacterSystem.instance;
        gs = GoldSystem.instance;
        mr = gameObject.GetComponent<MeshRenderer>();
    }

    public void StartDialogue()
    {
        cs.nomiraD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void CheckForReward()
    {
        if (cs.currentCharacter.choseQuestA) //RUINS
        {
            if (cs.currentCharacter.choseItemB) //divine
            {
                curseDivine = true;
                if (gs.goldAmount == 40) //=
                {
                    //do this
                    cs.nomiraP2QBGDivineFocusEquals.StartNewDialogue(cs.dialogueTriggerScript);

                }
                else if (gs.goldAmount > 40) //+
                {
                    //do this
                    cs.nomiraP2QBGDivineFocusPlus.StartNewDialogue(cs.dialogueTriggerScript);
                    //rep = -1;
                }

                else if (gs.goldAmount < 40) //-
                {
                    //do this
                    cs.nomiraP2QBGDivineFocusMinus.StartNewDialogue(cs.dialogueTriggerScript);
                    //rep = -1;
                }
            }
            else //other
            {
                curseOther = true;
                cs.nomiraP2QBOtherFocus.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
        else if (cs.currentCharacter.choseQuestB) //GOLEM
        {
            if (cs.currentCharacter.choseItemA) //weapon
            {
                golemSword = true;
                if (gs.goldAmount == 35) //=
                {
                    //do this
                    cs.nomiraP2QAGWeaponEquals.StartNewDialogue(cs.dialogueTriggerScript);

                }
                else if (gs.goldAmount > 35) //+
                {
                    //do this
                    cs.nomiraP2QAGWeaponPlus.StartNewDialogue(cs.dialogueTriggerScript);
                    //rep = -1;
                }

                else if (gs.goldAmount < 35) //-
                {
                    //do this
                    cs.nomiraP2QAGWeaponMinus.StartNewDialogue(cs.dialogueTriggerScript);
                    //rep = -1;
                }
            }
            else //arcane staff
            {
                golemOther = true;
                if (gs.goldAmount == 35) //=
                {
                    //do this
                    cs.nomiraP2QAGArcaneFoucosEquals.StartNewDialogue(cs.dialogueTriggerScript);

                }
                else if (gs.goldAmount > 35) //+
                {
                    //do this
                    cs.nomiraP2QAGArcaneFoucosPlus.StartNewDialogue(cs.dialogueTriggerScript);
                    //rep = -1;
                }

                else if (gs.goldAmount < 35) //-
                {
                    //do this
                    cs.nomiraP2QAGArcaneFoucosMinus.StartNewDialogue(cs.dialogueTriggerScript);
                    //rep = -1;
                }
            }
        }
    }

    public void ChangeEmote(Material emote)
    {
        if(emote == null)
        {
            Debug.Log("No material assigned");
            return;
        }
        mr.material = emote;
    }

    public void NomiraHit()
    {
        audioSource.Play();

        transform.DOPunchPosition(
            punch: new Vector3(1f, 0f, 0f),  // direction and strength of the punch
            duration: 0.5f,                 // total time of the punch effect
            vibrato: 10,                    // how many times it vibrates
            elasticity: 1f                  // how far it goes beyond the punch vector
        );

        cs.NomiraD1QOw.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void NomiraPeek()
    {
        ChangeEmote(peekMat);
    }

    public void NomiraPosessed()
    {
        ChangeEmote(posessedMat);
    }

    public void NomiraWickedSmile()
    {
        ChangeEmote(wickedSmileMat);
    }

}
