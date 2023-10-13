using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObject/MonsterData")]
public class MonsterData : CharacterBasicData
{
    [SerializeField] private string _monsterName;
    public string monsterName { get { return _monsterName; } }

    [SerializeField] private float _monsterArmor;
    public float monsterArmor { get { return _monsterArmor; } }
    
    [SerializeField] private float _monsterDamage;
    public float monsterDamage { get { return _monsterDamage; } }

    [SerializeField] private float _monsterHealth;
    public float monsterHealth { get { return _monsterHealth; } }

    [SerializeField] private float _attackDelay;
    public float attackDelay { get { return _attackDelay; } }

    [SerializeField] private float _onTriggerTime;
    public float onTriggerTime { get { return _onTriggerTime; } }

    [SerializeField] private float _offTriggerTime;
    public float offTriggerTime { get { return _offTriggerTime; } }

    [SerializeField] private float _expValue;
    public float expValue { get { return _expValue; } }

    [SerializeField] private int _goldValue;
    public int goldValue { get { return _goldValue; } }
}
