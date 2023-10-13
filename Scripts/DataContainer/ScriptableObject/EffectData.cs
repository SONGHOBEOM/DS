using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectData", menuName = "ScriptableObject/EffectData")]
public class EffectData : ScriptableObject
{
    [SerializeField] private  Define.ObjectType _objType;
    public Define.ObjectType objType { get { return _objType; } }

    [SerializeField] private  int _weaponIndex;
    public int weaponIndex { get { return _weaponIndex; } }

    [SerializeField] private  string _skillName;
    public string skillName { get { return _skillName; } }

    [SerializeField] private Vector3 _effectRange;
    public Vector3 effectRange { get { return _effectRange; } }

    [SerializeField] private Quaternion _effectRotation;
    public Quaternion effectRotation { get { return _effectRotation; } }

    [SerializeField] private float _playDelay;
    public float playDelay { get { return _playDelay; } }

    [SerializeField] private float _playDuration;
    public float playDuration { get { return _playDuration; } }

    [SerializeField] private GameObject _effect;
    public GameObject effect { get { return _effect; } }

}
