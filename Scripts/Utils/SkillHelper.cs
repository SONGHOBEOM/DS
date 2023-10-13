using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SkillHelper
{
    private static Dictionary<Define.PlayerClass, List<SkillData>> entireSkillDatas = new Dictionary<Define.PlayerClass, List<SkillData>>();

    public static void CreateSkillDatas()
    {
        var skillDatas = DataManager.Instance.entireDataContainer.skillDataContainer.Get();
        foreach (var skillData in skillDatas)
        {
            AddSkill(skillData);
        }


        void AddSkill(SkillData skillData)
        {
            if(entireSkillDatas.TryGetValue(skillData.playerClass, out var _skillDatas))
                _skillDatas.Add(skillData);
            else
            {
                var skillDatas = new List<SkillData> { skillData };
                entireSkillDatas.Add(skillData.playerClass, skillDatas);
            }
        }
    }

    public static List<SkillData> GetSkillDataList(Define.PlayerClass playerClass)
    {
        if(entireSkillDatas.TryGetValue(playerClass, out var skillDatas))
            return skillDatas.ToList();

        return default;
    }
}
