using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorData", menuName = "ScriptableObject/ArmorData")]
public class ArmorData : ItemData
{
    [SerializeField] private int _equipLevel;
    public int equipLevel { get { return _equipLevel; } }

    [SerializeField] private float _defense;
    public float defense { get { return _defense; } }

    [SerializeField] private float _maxHp;
    public float maxHp { get { return _maxHp; } }

    [SerializeField] private Define.Rarity _rarity;
    public Define.Rarity rarity { get {  return _rarity; } }
}
