using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZekeScript : MonoBehaviour
{
    [SerializeField] CharacterSystem cs;
    [SerializeField] DialogueCharacter zekeCharacter;

    [SerializeField] MeshRenderer mr;

    [SerializeField] GameObject refuseTag;

    [SerializeField] GameObject raspberries;
    [SerializeField] RaspberriesScript raspS;
    [SerializeField] RaspberriesScript mushroomS;
    [SerializeField] RaspberriesScript mushPottedS;
    [SerializeField] RaspberriesScript lornePotionS;

    [SerializeField] ParticleSystem zekePoof;
    [SerializeField] AudioSource zekePoofAudio;

    public GameObject raspberriesGlow;
    public GameObject magicMushroomGlow;
    public GameObject magicMushroomPotGlow;
    public GameObject lornePotionGlow;

    bool tagSystem;
    bool tagOn;

    public bool zekeRejected;

    public bool refusedOnce;

    [Header("Emote Swapping Gag")]
    [SerializeField] int emoteRepeatCount;
    [SerializeField] float interval;
    [SerializeField] float interval2;
    [SerializeField] Material zekeReal1;
    [SerializeField] Material zekeReal2;

    public static ZekeScript instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (tagSystem)
        {
            if ((!raspS.raspberriesOnDesk && !mushroomS.raspberriesOnDesk && !mushPottedS.raspberriesOnDesk && !lornePotionS.raspberriesOnDesk) && !tagOn)
            {
                refuseTag.SetActive(true);
                tagOn = true;
            }
            else if ((raspS.raspberriesOnDesk || mushroomS.raspberriesOnDesk || mushPottedS.raspberriesOnDesk || lornePotionS.raspberriesOnDesk) && tagOn)
            {
                refuseTag.SetActive(false);
                tagOn = false;
            }
        }
    }

    public void StartDialogue()
    {
        cs.zekeD3P1.StartNewDialogue(cs.dialogueTriggerScript);
    }

    public void RefuseRaspberries()
    {
        if(!refusedOnce)
        {
            cs.zekeD3Refuse1.StartNewDialogue(cs.dialogueTriggerScript);
            refusedOnce = true;
        }
        else
        {
            //zekeRejected = true;
            cs.zekeD3Refuse2.StartNewDialogue(cs.dialogueTriggerScript);
            //gameObject.GetComponent<CharacterReputation>().ModifyReputation(-3); //why isn't this working!
        }
    }

    public void TagSystemOn()
    {
        tagSystem = true;
    }

    public void TagSystemOff()
    {
        tagSystem = false;
        refuseTag.SetActive(false);
        tagOn = false;
    }

    public void ChangeEmote(Material emote)
    {
        mr.material = emote;
    }

    public void ZekeRejected()
    {
        zekeRejected = true;
    }

    public void ZekePoof()
    {
        zekePoofAudio.Play();
        zekePoof.Play();
    }

    public void EmoteSwap()
    {
        StartCoroutine(AlternateFunctions());
    }

    private IEnumerator AlternateFunctions()
    {
        //phase 1
        for (int i = 0; i < emoteRepeatCount; i++)
        {
            if (i % 2 == 0)
                ChangeEmote(zekeReal1);
            else
                ChangeEmote(zekeReal2);

            yield return new WaitForSeconds(interval);
        }

        yield return new WaitForSeconds(0.5f);

        //phase 2
        for (int i = 0; i < emoteRepeatCount; i++)
        {
            if (i % 2 == 0)
                ChangeEmote(zekeReal2);
            else
                ChangeEmote(zekeReal1);

            yield return new WaitForSeconds(interval2);
        }

        yield return new WaitForSeconds(0.5f);

        // After all alternations are done
        ContinueRealZekeDialogue();
    }

    void ContinueRealZekeDialogue()
    {
        cs.zekeD3FeedMushroomP2.StartNewDialogue(cs.dialogueTriggerScript);
    }
}
