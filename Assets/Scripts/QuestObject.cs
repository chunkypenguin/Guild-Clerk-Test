using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/New Quest", order = 1)]

public class QuestObject : ScriptableObject
{
    [TextArea(5, 5)]
    public string questTitle;
    [TextArea(10, 15)]
    public string questDescription;
    [TextArea(3, 3)]
    public string questReward;
}
