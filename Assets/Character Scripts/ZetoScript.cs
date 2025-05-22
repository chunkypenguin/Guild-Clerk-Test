using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

public class ZetoScript : MonoBehaviour
{
    QuestSystem qs;
    public static ZetoScript instance;

    public Transform zetoTransform;
    public GameObject zetoQuest;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        zetoTransform = transform;
        qs = QuestSystem.instance;
    }
    public void ZetoQuestSteal()
    {
        qs.GetQuestRB(zetoQuest);
    }
}
