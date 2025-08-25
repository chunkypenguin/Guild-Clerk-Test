using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalinScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] GoldSystem gs;
    [SerializeField] MeshRenderer mr;

    public bool gaveGold;

    public static KalinScript instance;

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
        cs.KalinP1.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void CheckForReward()
    {
        gs.totalGoldGiven = gs.goldAmount;
        int rep = 0;
        if (cs.currentCharacter.choseQuestA) 
        {
            gs.goldWanted = 30;
            if (gs.goldAmount == 30)
            {
                //do this
                cs.KalinQ1AGEquals.StartNewDialogue(cs.dialogueTriggerScript);
                rep = 1;
            }
            else if (gs.goldAmount > 30)
            {
                //do this
                cs.KalinQ1AGPlus.StartNewDialogue(cs.dialogueTriggerScript);
                rep = -1;
            }

            else if (gs.goldAmount < 30)
            {
                //do this
                cs.KalinQ1AGMinus.StartNewDialogue(cs.dialogueTriggerScript);
                rep = 0;
            }
        }

        gameObject.GetComponent<CharacterReputation>().ModifyReputation(rep);
    }

    public void GaveGold(bool gold)
    {
        gaveGold = gold;
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
}
