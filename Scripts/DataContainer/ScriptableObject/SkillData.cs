using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObject/SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] private Define.ObjectType _objType;
    public Define.ObjectType objType { get { return _objType; } }

    [SerializeField] private Define.SkillType _skillType;
    public Define.SkillType skillType { get { return _skillType; } }

    [SerializeField] private Define.PlayerClass _playerClass;
    public Define.PlayerClass playerClass { get {  return _playerClass; } }

    [SerializeField] private string _skillName;
    public string skillName { get { return _skillName; } }

    [SerializeField] private float _skillDamage;
    public float skillDamage { get { return _skillDamage; } }

    [SerializeField] private float _coefficient;
    public float coefficient { get { return _coefficient; } }

    [SerializeField] private float _coolTime;
    public float coolTime { get { return _coolTime; } }

    [SerializeField] private Sprite _skill_Image;
    public Sprite skill_Image { get { return _skill_Image; } }

}
