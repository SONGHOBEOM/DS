using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PatternInfo
{
    [SerializeField] private string _bossMonsterName;
    public string bossMonsterName { get { return _bossMonsterName; } }
    [SerializeField] private string[] _attackPatterns;
    public string[] attackPatterns { get { return _attackPatterns; } }

    [SerializeField] private float _playDelay;
    public float playDelay { get { return _playDelay; } }
    [SerializeField] private float _playDuration;
    public float playDuration { get { return _playDuration; } }

    [SerializeField] private float _markingTime;
    public float markingTime { get { return _markingTime; } }
}

[CreateAssetMenu(fileName = "PatternData", menuName = "ScriptableObject/PatternData")]
public class PatternData : ScriptableObject
{
    [SerializeField]
    public List<PatternInfo> patternDatas = new List<PatternInfo>();

    public PatternInfo GetPatternInfo(string name) => patternDatas.FirstOrDefault(x => x.bossMonsterName == name);
}
