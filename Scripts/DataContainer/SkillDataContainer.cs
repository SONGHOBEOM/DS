using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SkillDataContainer : ISimpleContainer
{
    public List<SkillData> skillDatas { get; set; } = new();
    public void Regist(SkillData skillData) => skillDatas.Add(skillData);
    public List<SkillData> Get() => skillDatas.ToList();

}
