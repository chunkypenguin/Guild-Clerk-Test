﻿using UnityEngine;

namespace HeneGames.DialogueSystem
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Dialogue System/New Dialogue Character", order = 1)]

    public class DialogueCharacter : ScriptableObject
    {
        public Sprite characterPhoto;
        public string characterName;

        public QuestObject[] quest;

        public string ItemAName;
        public string ItemBName;

        public string ItemANameA;
        public string ItemBNameB;

        public bool choseQuestA;
        public bool choseQuestB;

        public bool choseItemA;
        public bool choseItemB;
        public bool choseItemAA;

        public Color textColor;

        public void ResetBools()
        {
            choseQuestA = false;
            choseQuestB = false;

            choseItemA = false;
            choseItemB = false;
            choseItemAA = false;
    }
    }
}