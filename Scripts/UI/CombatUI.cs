using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [SerializeField] private Button attackButton;
    [SerializeField] private List<SkillBase> skills;
    [SerializeField] private Button interActionButton;

    public static Action setInteractionButton = null;
    public static Action setAttackButton = null;
    public static Action<List<SkillData>> setSkillButtons = null;
    

    private void Start()
    {
        AddButtonFunc();
        setInteractionButton += SetInterActionButton;
        setAttackButton += SetAttackButton;
        setSkillButtons+= SetSkillButtons;
    }

    void AddButtonFunc()
    {
        attackButton.onClick.AddListener(OnBasicAttack);
        interActionButton.onClick.AddListener(OnDialog);
    }

    void OnBasicAttack() => EntityManager.Instance.player.BasicAttackWithDelay();


    void SetInterActionButton()
    {
        attackButton.gameObject.SetActive(false);
        interActionButton.gameObject.SetActive(true);
    }

    void SetAttackButton()
    {
        attackButton.gameObject.SetActive(true);
        interActionButton.gameObject.SetActive(false);
    }

    void SetSkillButtons(List<SkillData> skillDatas)
    {
        for(int i = 0; i < skillDatas.Count; i++)
        {
            var skillData = skillDatas[i];
            foreach(var skill in skills)
            {
                skill.gameObject.SetActive(true);
                if(OnAble())
                {
                    skill.OnActivate(skillData);
                    break;
                }
                bool OnAble() => skill.GetSkillData() == null && skill.GetSkillType() == skillData.skillType;
            }
        }

    }

    void OnDialog() => NpcManager.Instance.OnNpcDialog();

}
