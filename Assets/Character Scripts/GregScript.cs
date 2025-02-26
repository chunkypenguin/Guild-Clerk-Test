using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GregScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {
        cs.gregD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }
}
