using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchillesScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] GoldSystem gs;
    [SerializeField] MeshRenderer mr;

    public DialogueCharacter achillesCharacter;

    public int achillesGoldGiven;
    public int achillesRequestedGold;
    bool askedForGold;

    public bool achillesCoinGiven;
    public GameObject coinGlow;

    public static AchillesScript instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cs = CharacterSystem.instance;
        gs = GoldSystem.instance;
        mr = gameObject.GetComponent<MeshRenderer>();
    }

    public void StartDialogue()
    {
        if (!achillesCoinGiven)
        {
            cs.achillesP1.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else
        {
            if (cs.currentCharacter.choseQuestA)
            {
                cs.achillesQ1AReturn.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (cs.currentCharacter.choseQuestB)
            {
                cs.achillesQ1BReturn.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }

    }

    public void CheckForReward()
    {
        achillesGoldGiven = gs.goldAmount;
        if (cs.currentCharacter.choseQuestA)
        {
            achillesRequestedGold = 12;
            if (gs.goldAmount == 12)
            {
                cs.achillesAReturnEquals.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 12)
            {
                cs.achillesAReturnPlus.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 12)
            {
                cs.achillesAReturnMinus.StartNewDialogue(cs.dialogueTriggerScript);
            }

        }
        else if (cs.currentCharacter.choseQuestB)
        {
            achillesRequestedGold = 17;
            if (gs.goldAmount == 17)
            {
                cs.achillesBReturnEquals.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 17)
            {
                cs.achillesBReturnPlus.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 17)
            {
                cs.achillesBReturnMinus.StartNewDialogue(cs.dialogueTriggerScript);
            }
        }
    }

    public void ChangeEmote(Material emote)
    {
        if (emote == null)
        {
            Debug.Log("No material assigned");
            return;
        }
        mr.material = emote;
    }

    public void AchillesGold()
    {
        if (askedForGold)
        {
            Debug.Log("Achilles: " + achillesGoldGiven + ", " + achillesRequestedGold);
            ReviewManager.instance.CharacterGoldAccuracyCalculator(achillesGoldGiven, achillesRequestedGold);

        }
    }
}
