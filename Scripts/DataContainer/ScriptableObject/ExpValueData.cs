using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ExpValueInfo
{
    [SerializeField] private int _level;
    public int level
    {
        get { return _level; }
    }
    [SerializeField] private float _maxExpValue;
    public float maxExpValue
    {
        get { return _maxExpValue;  }
    }
}
[CreateAssetMenu(fileName = "ExpValueData", menuName = "ScriptableObject/ExpValueData")]
public class ExpValueData : ScriptableObject
{
    [SerializeField] private List<ExpValueInfo> maxExpInfos;

    public ExpValueInfo GetMaxExpInfo(int level) => maxExpInfos.Find(x=> x.level == level);
}
