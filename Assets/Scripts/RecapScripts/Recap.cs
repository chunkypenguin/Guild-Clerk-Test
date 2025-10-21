using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Recap : MonoBehaviour
{
    public GameObject RecapPage;

    [SerializeField] Image fadeBG;

    [SerializeField] DialogueCharacter[] diaCharacter;

    int greg = 0, finch = 1, lorne = 2, andy = 3,
        lotest = 4, maggie = 5, zeto = 6, nomira = 7,
        achilles = 8, vanelle = 9, ishizu = 10, jolene = 11, 
        zeke = 12, kalin = 13, tahmas = 14;

    public bool recapOn;

    [SerializeField] Scrollbar scrollbar;

    [System.Serializable]
    public class CharacterRecap
    {
        public TMP_Text nameDisplay;
        public string[] name;

        public GameObject[] picture;

        public TMP_Text textDisplay;

        [TextArea(3, 10)]
        public string[] text;
    }

    public CharacterRecap[] characterRecaps;

    public static Recap instance;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            //DisplayRecap();
        }
    }

    public void Greg()
    {
        if (diaCharacter[greg].choseQuestA) //arrow
        {
            Picture(characterRecaps[greg], 0);
            DisplayText(characterRecaps[greg], 0);
        }
        else if (diaCharacter[greg].choseQuestB)//mushroom
        {
            Picture(characterRecaps[greg], 1);
            if(GregScript.instance.gaveMysticMushroom)
            {
                DisplayText(characterRecaps[greg], 2);
            }
            else //if did not give mushroom
            {
                DisplayText(characterRecaps[greg], 1);
            }
        }
    }

    public void Finch()
    {
        if (!FinchScript.instance.finchUpset) 
        {
            Picture(characterRecaps[finch], 0);
            DisplayText(characterRecaps[finch], 0);
        }
        else
        {
            Picture(characterRecaps[finch], 1);
            DisplayText(characterRecaps[finch], 1);
        }
    }

    public void Lorne()
    {
        if(LorneScript.instance.gaveYarn)
        {
            DisplayName(characterRecaps[lorne], 0);
            Picture(characterRecaps[lorne], 0);
            DisplayText(characterRecaps[lorne], 0);
        }
        else
        {
            DisplayName(characterRecaps[lorne], 1);
            Picture(characterRecaps[lorne], 1);
            DisplayText(characterRecaps[lorne], 1);
        }
    }

    public void Andy()
    {
        if(AndyScript.instance.andyMomVisited)
        {
            if (AndyScript.instance.gaveEqualOrMoreGold)
            {
                Picture(characterRecaps[andy], 0);
                DisplayText(characterRecaps[andy], 0);
            }
            else if (AndyScript.instance.gaveLessGold)
            {
                Picture(characterRecaps[andy], 0);
                DisplayText(characterRecaps[andy], 1);
            }

        }
        else
        {
            Picture(characterRecaps[andy], 1);
            DisplayText(characterRecaps[andy], 2);
        }
    }

    public void Lotest()
    {
        if(LotestScript.instance.gaveEqualOrMoreGold) //green bundle 
        {
            Picture(characterRecaps[lotest], 0);
            DisplayText(characterRecaps[lotest], 0);
        }
        else if(LotestScript.instance.gaveLessGold) //green bundle
        {
            Picture(characterRecaps[lotest], 1);
            DisplayText(characterRecaps[lotest], 1);
        }
        else //velvet bundle
        {
            Picture(characterRecaps[lotest], 2);
            DisplayText(characterRecaps[lotest], 2);
        }
    }

    public void Maggie()
    {
        if (diaCharacter[maggie].choseQuestB) //goblin
        {
            Picture(characterRecaps[maggie], 0);
            DisplayText(characterRecaps[maggie], 0);
        }
        else if (diaCharacter[maggie].choseQuestA)//food
        {
            Picture(characterRecaps[maggie], 1);
            DisplayText(characterRecaps[maggie], 1);
        }
    }

    public void Zeto()
    {
        if (diaCharacter[zeto].choseQuestB) //Golem
        {
            Picture(characterRecaps[zeto], 0);
            DisplayText(characterRecaps[zeto], 0);
        }
        else if (diaCharacter[zeto].choseQuestA) //Curse
        {
            Picture(characterRecaps[zeto], 1);
            DisplayText(characterRecaps[zeto], 1);
        }
    }

    public void Nomira()
    {
        if (NomiraScript.instance.curseDivine)
        {
            Picture(characterRecaps[nomira], 0);
            DisplayText(characterRecaps[nomira], 0);
        }
        else if(NomiraScript.instance.curseOther)
        {
            Picture(characterRecaps[nomira], 1);
            DisplayText(characterRecaps[nomira], 1);
        }
        else if(NomiraScript.instance.golemOther)
        {
            Picture(characterRecaps[nomira], 0);
            DisplayText(characterRecaps[nomira], 2);
        }
        else if (NomiraScript.instance.golemSword)
        {
            Picture(characterRecaps[nomira], 2);
            DisplayText(characterRecaps[nomira], 3);
        }
    }

    public void Achilles()
    {
        if (diaCharacter[achilles].choseQuestA && !AchillesScript.instance.achillesCoinGiven) //Family w/ out coin
        {
            Picture(characterRecaps[achilles], 0);
            DisplayText(characterRecaps[achilles], 0);
        }
        else if (diaCharacter[achilles].choseQuestB && !AchillesScript.instance.achillesCoinGiven) //fiance w/ out coin
        {
            Picture(characterRecaps[achilles], 1);
            DisplayText(characterRecaps[achilles], 1);
        }
        else if (diaCharacter[achilles].choseQuestA && AchillesScript.instance.achillesCoinGiven) //Family w/ coin (still dies lol)
        {
            Picture(characterRecaps[achilles], 0);
            DisplayText(characterRecaps[achilles], 2);
        }
        else if (diaCharacter[achilles].choseQuestB && AchillesScript.instance.achillesCoinGiven) //fiance w/ coin
        {
            Picture(characterRecaps[achilles], 3);
            DisplayText(characterRecaps[achilles], 3);
        }
    }

    public void Vanelle()
    {
        if (VanelleScript.instance.mandrakeQuestGiven) //mandrake
        {
            Picture(characterRecaps[vanelle], 0);
            DisplayText(characterRecaps[vanelle], 0);
        }
        else
        {
            Picture(characterRecaps[vanelle], 1);
            DisplayText(characterRecaps[vanelle], 1);
        }
    }

    public void Ishizu()
    {
        if (diaCharacter[ishizu].choseItemA) 
        {
            DisplayName(characterRecaps[ishizu], 0);
            Picture(characterRecaps[ishizu], 0);
            DisplayText(characterRecaps[ishizu], 0);
        }
        else if (diaCharacter[ishizu].choseItemB)
        {
            DisplayName(characterRecaps[ishizu], 1);
            Picture(characterRecaps[ishizu], 1);
            DisplayText(characterRecaps[ishizu], 1);
        }
    }

    public void Jolene()
    {
        if (diaCharacter[jolene].choseQuestA) //Apple
        {
            Picture(characterRecaps[jolene], 0);
            DisplayText(characterRecaps[jolene], 0);
        }
        else if(diaCharacter[jolene].choseQuestB && AndyScript.instance.andyMomVisited) //spider and andy went on dragon quest
        {
            Picture(characterRecaps[jolene], 1);
            DisplayText(characterRecaps[jolene], 1);
        }
        else if(diaCharacter[jolene].choseQuestB && !AndyScript.instance.andyMomVisited)
        {
            Picture(characterRecaps[jolene], 2);
            DisplayText(characterRecaps[jolene], 2);
        }
    }

    public void Zeke()
    {

        if(!ZekeScript.instance.zekeRejected) //gave rasperries
        {
            if (diaCharacter[zeke].choseItemA) //raspberries
            {
                Picture(characterRecaps[zeke], 0);
                DisplayText(characterRecaps[zeke], 0);
            }
            else if (diaCharacter[zeke].choseItemB) //mushroom
            {
                Picture(characterRecaps[zeke], 1);
                DisplayText(characterRecaps[zeke], 1);
            }
            else if (diaCharacter[zeke].choseItemAA) //lorne potion
            {
                Picture(characterRecaps[zeke], 2);
                DisplayText(characterRecaps[zeke], 2);
            }
            else if (diaCharacter[zeke].choseItemBB) //pie
            {
                Picture(characterRecaps[zeke], 3);
                DisplayText(characterRecaps[zeke], 3);
            }
            else if (diaCharacter[zeke].choseItemC) //grim note
            {
                Picture(characterRecaps[zeke], 4);
                DisplayText(characterRecaps[zeke], 4);
            }
        }
        else //rejected 
        {
            Picture(characterRecaps[zeke], 5);
            DisplayText(characterRecaps[zeke], 5);
        }
    }

    public void Kalin()
    {

        if (KalinScript.instance.gaveEqualGold)
        {
            if(KalinScript.instance.gaveJosieBundle)
            {
                DisplayName(characterRecaps[kalin], 0);
                Picture(characterRecaps[kalin], 0);
                DisplayText(characterRecaps[kalin], 0);
            }
            else
            {
                DisplayName(characterRecaps[kalin], 0);
                Picture(characterRecaps[kalin], 1);
                DisplayText(characterRecaps[kalin], 1);
            }



        }
        else
        {
            DisplayName(characterRecaps[kalin], 1);
            Picture(characterRecaps[kalin], 2);
            DisplayText(characterRecaps[kalin], 2);
        }
    }

    public void Tahmas()
    {
        if (TahmasScript.instance.gaveMoreGold)
        {
            Picture(characterRecaps[tahmas], 0);
            DisplayText(characterRecaps[tahmas], 0);
        }
        else if(TahmasScript.instance.gaveEqualOrLessGold)
        {
            Picture(characterRecaps[tahmas], 0);
            DisplayText(characterRecaps[tahmas], 1);
        }
        else //never appeared
        {
            DisplayName(characterRecaps[tahmas], 1);
            Picture(characterRecaps[tahmas], 1);
            DisplayText(characterRecaps[tahmas], 2);
        }
    }

    public void Picture(CharacterRecap character, int count)
    {
        foreach(GameObject pic in character.picture)
        {
            pic.SetActive(false);
        }
        character.picture[count].gameObject.SetActive(true);
    }

    public void DisplayText(CharacterRecap character, int count)
    {
        character.textDisplay.text = character.text[count];
    }

    public void DisplayName(CharacterRecap character, int count)
    {
        character.nameDisplay.text = character.name[count];
    }

    public void DisplayRecap()
    {
        Greg();
        Finch();
        Lorne();
        Andy();
        Lotest();
        Maggie();
        Zeto();
        Nomira();
        Achilles();
        Vanelle();
        Ishizu();
        Jolene();
        Zeke();
        Kalin();
        Tahmas();
    }

    public void FadeToRecap()
    {
        recapOn = true;
        scrollbar.value = 1; //set scrollbar to top of page
        DisplayRecap();
        fadeBG.DOFade(0f, 2f).OnComplete(() =>
        {
            fadeBG.gameObject.SetActive(false);
        });
    }
}
