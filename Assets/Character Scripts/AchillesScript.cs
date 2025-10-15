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

    public static AchillesScript instance;

    public int achillesGoldGiven;
    public int achillesRequestedGold;
    bool askedForGold;

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
        cs.achillesP1.StartNewDialogue(cs.dialogueTriggerScript);
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
            ReviewManager.instance.CharacterGoldAccuracyCalculator(achillesGoldGiven, achillesRequestedGold);

        }
    }
}
