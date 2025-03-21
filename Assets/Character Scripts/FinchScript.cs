using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinchScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] GoldSystem gs;

    [SerializeField] MeshRenderer mr;

    [SerializeField] GameObject refuseTag;
    bool tagOn;

    bool gaveGold;
    bool tagSystem;
    int goldRequestCount;

    int a;
    int b;
    int c;
    int d;

    bool pickedA, pickedB, pickedC;

    public bool askingForGold;

    private void Update()
    {
        if (tagSystem)
        {
            if (gs.goldAmount == 0 && !tagOn)
            {
                refuseTag.SetActive(true);
                tagOn = true;
            }
            else if(gs.goldAmount > 0 && tagOn)
            {
                refuseTag.SetActive(false);
                tagOn = false;
            }
        }
    }

    public void StartDialogue()
    {
        cs.finchD1P1.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void CheckForReward()
    {
        if (cs.pickedQ1A && !gaveGold)
        {
            if (gs.goldAmount == 15)
            {
                //Do this
                cs.finchD1G1BP1.StartNewDialogue(cs.dialogueTriggerScript);
            }
            else if (gs.goldAmount > 15)
            {
                //do this
                cs.finchD1G1CP1.StartNewDialogue(cs.dialogueTriggerScript);
            }

            else if (gs.goldAmount < 15)
            {
                //do this
                cs.finchD1G1AP1.StartNewDialogue(cs.dialogueTriggerScript);
            }

            gaveGold = true;
        }
        else if (gaveGold)
        {
            if(gs.goldAmount > 0)
            {
                if(!pickedA)
                {
                    a++;
                    pickedA = true;
                }
                else if(!pickedB)
                {
                    b++;
                    pickedB = true;
                }
                else if(!pickedC)
                {
                    c++;
                    pickedC = true;
                }
                else
                {
                    d++;
                }

                ChooseDialogue();

            }
            else
            {
                Refuse();

            }
        }
    }

    public void ChooseDialogue()
    {
        if(a == 1 && b == 0 && c == 0 && d == 0)
        {
            cs.finchD1GR1.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == 1 && b == 1 && c == 0 && d == 0)
        {
            cs.finchD1GR2.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == 1 && b == 1 && c == 1 && d == 0)
        {
            cs.finchD1GR3.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == 1 && b == 1 && c == 1 && d == 1)
        {
            cs.finchD1GR4.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == 1 && b == 1 && c == 1 && d == -1)
        {
            cs.finchD1GR5.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == 1 && b == 1 && c == -1 && d == 0)
        {
            cs.finchD1GR6.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == 1 && b == -1 && c == 0 && d == 0)
        {
            cs.finchD1GR6.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == -1 && b == 0 && c == 0 && d == 0)
        {
            cs.finchD1GR7.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == -1 && b == 1 && c == 0 && d == 0)
        {
            cs.finchD1GR2.StartNewDialogue(cs.dialogueTriggerScript);

        }
        else if (a == -1 && b == 1 && c == 1 && d == 0)
        {
            cs.finchD1GR3.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == -1 && b == 1 && c == 1 && d == 1)
        {
            cs.finchD1GR8.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == -1 && b == 1 && c == 1 && d == -1)
        {
            cs.finchD1GR9.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == -1 && b == 1 && c == -1 && d == 0)
        {
            cs.finchD1GR10.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == -1 && b == 1 && c == -1 && d == 1)
        {
            cs.finchD1GR11.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == -1 && b == 1 && c == -1 && d == -1)
        {
            cs.finchD1GR12.StartNewDialogue(cs.dialogueTriggerScript);
        }
        else if (a == -1 && b == -1 && c == 0 && d == 0)
        {
            cs.finchD1GR12.StartNewDialogue(cs.dialogueTriggerScript);
        }
    }

    public void Refuse()
    {
        if (!pickedA)
        {
            a--;
            pickedA = true;
        }
        else if (!pickedB)
        {
            b--;
            pickedB = true;
        }
        else if (!pickedC)
        {
            c--;
            pickedC = true;
        }
        else
        {
            d--;
        }
        ChooseDialogue();
    }

    public void TagSystemOn()
    {
        tagSystem = true;
    }
    public void TagSystemOff()
    {
        tagSystem = false;
        refuseTag.SetActive(false);
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }
}
