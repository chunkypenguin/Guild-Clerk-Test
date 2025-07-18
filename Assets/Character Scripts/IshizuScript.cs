using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IshizuScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] GoldSystem gs;
    [SerializeField] MeshRenderer mr;

    public GameObject redHerbGlow;
    public GameObject tealHerbGlow;
    public GameObject yourNextLetter;

    [SerializeField] Material ghostMat;

    public DialogueCharacter ishizuCharacter;

    public static IshizuScript instance;

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
        if (ishizuCharacter.choseItemA)
        {
            cs.ishizuRed2.StartNewDialogue(cs.dialogueTriggerScript);

        }
        else if (ishizuCharacter.choseItemB)
        {
            //Drop Bad Omen
            ItemSystem.instance.ReturnItem(yourNextLetter);
            LightSystem.instance.DimLights();
            Invoke(nameof(GhostLeave), 2);
            gameObject.GetComponent<CharacterReputation>().ModifyReputation(-2);
            //Reverse music, stall for a few seconds then Ishizu ghost leaves
        }
        else //start with this
        {
            cs.ishizuP1.StartNewDialogue(cs.dialogueTriggerScript);
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

    public void GhostMat()
    {
        mr.material = ghostMat;
    }

    void GhostLeave()
    {
        gameObject.GetComponent<MoveCharacter>().MoveToEnd();
        LightSystem.instance.UndimLights();
    }
}
