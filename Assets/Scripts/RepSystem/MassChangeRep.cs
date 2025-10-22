using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassChangeRep : MonoBehaviour
{
    [SerializeField] CharacterReputation[] characterRep;

    public bool canShowRep;

    public static MassChangeRep instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GiveCharactersRep(1);
        Invoke(nameof(CanShowRepVisual), 1f);
    }
    void GiveCharactersRep(int rep)
    {
        foreach (CharacterReputation character in characterRep)
        {
            character.ModifyReputation(rep);
        }
    }

    void CanShowRepVisual()
    {
        canShowRep = true;
    }
}
