using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData")]
public class WeaponData : ItemData
{ 

    [SerializeField] private Define.PlayerClass _playerClass;
    public Define.PlayerClass playerClass { get { return _playerClass; } }

    [SerializeField] private Define.WeaponType _weaponType;
    public Define.WeaponType weaponType { get { return _weaponType; } }

    [SerializeField] private Define.Rarity _rarity;
    public Define.Rarity rarity { get {  return _rarity; } }

    [SerializeField] private int _equipLevel;
    public int equipLevel { get { return _equipLevel; } }

    [SerializeField] private float _maxDamage;
    public float maxDamage { get { return _maxDamage; } }

    [SerializeField] private float _minDamage;
    public float minDamage { get { return _minDamage; } }

    [SerializeField] private float _maxHp;
    public float maxHp { get { return _maxHp; } }

    [SerializeField] private float _attackDelay;
    public float attackDelay { get { return _attackDelay; } }

    [SerializeField] private float _defense;
    public float defense { get { return _defense; } }

    [SerializeField] private string _description;
    public string description { get { return _description; } }

}