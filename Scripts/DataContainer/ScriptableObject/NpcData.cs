using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "ScriptableObject/NpcData")]
public class NpcData : ScriptableObject
{
    [SerializeField] private int _npcId;
    public int npcId
    {
        get { return _npcId; }
    }

    [SerializeField] private string _npcName;
    public string npcName
    {
        get { return _npcName; }
    }
}