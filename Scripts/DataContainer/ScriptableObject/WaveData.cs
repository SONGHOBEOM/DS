using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class WaveInfo
{
    [SerializeField] private int _waveLevel;

    public int waveLevel { get { return _waveLevel; } }

    [SerializeField] private Define.WaveType _waveType;
    public Define.WaveType waveType { get { return _waveType; } }

    [SerializeField] private int _monsterCount;

    public int monsterCount { get { return _monsterCount; } }

    [SerializeField] private float _waveTime;

    public float waveTime { get { return _waveTime; } }

    [SerializeField] private GameObject[] _monsterPrefabs;

    public GameObject[] monsterPrefabs { get { return _monsterPrefabs; } }
}

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObject/WaveData")]
public class WaveData : ScriptableObject
{
    public List<WaveInfo> waveDatas = new List<WaveInfo>();

    public WaveInfo GetWaveInfo(int level) => waveDatas.FirstOrDefault(x => x.waveLevel == level);
}