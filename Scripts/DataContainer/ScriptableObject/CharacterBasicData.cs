using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterBasicData", menuName = "ScriptableObject/CharacterBasicData")]
public class CharacterBasicData : ScriptableObject
{
    [SerializeField] private int _characterID;
    public int characterID { get { return _characterID; } }

    [SerializeField] private Define.ObjectType _objectType;
    public Define.ObjectType objectType { get { return _objectType; } }

    [SerializeField] private float _basicSpeed;
    public float basicSpeed { get { return _basicSpeed; } }
}
