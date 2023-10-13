using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBasicData", menuName = "ScriptableObject/PlayerBasicData")]
public class PlayerBasicData : CharacterBasicData
{
    [SerializeField] private float _runSpeed;
    public float runSpeed { get { return _runSpeed; } }

    [SerializeField] private float _dodgeSpeed;
    public float dodgeSpeed { get { return _dodgeSpeed; } }
    [SerializeField] private float _dodgeDistance;
    public float dodgeDistance { get {  return _dodgeDistance; } }
}
