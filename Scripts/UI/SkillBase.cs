using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillBase : MonoBehaviour
{
    [SerializeField] protected Image skill_Image;
    [SerializeField] protected Image coolTimeImage;

    protected SkillData skillData;
    protected Define.SkillType skillType;
    protected float coolTime = 0;
    protected bool isCasted = false;

    public static Action setOff = null;

    protected virtual void OnEnable()
    {
        setOff -= SetOff;
        setOff += SetOff;
    }
    public void OnActivate(SkillData skillData)
    {
        Clear();
        this.skillData = skillData;
        skill_Image.sprite = skillData.skill_Image;
        StartCoroutine(CheckCoolTIme());
        StartCoroutine(UpdateCoolTimeImage());
    }

    protected IEnumerator CheckCoolTIme()
    {
        coolTime = skillData.coolTime;
        while (gameObject.activeSelf)
        {
            if (coolTime > 0)
                coolTime -= Time.deltaTime;

            if (isCasted)
            {
                coolTime = skillData.coolTime;
                isCasted = false;
            }
            yield return null;
        }
    }

    protected IEnumerator UpdateCoolTimeImage()
    {
        while (gameObject.activeSelf)
        {
            yield return null;
            coolTimeImage.fillAmount = coolTime / skillData.coolTime;
        }
    }

    protected void Clear()
    {
        StopAllCoroutines();
        isCasted = false;
        coolTime = 0;
        skillData = null;
        skill_Image.sprite = null;
    }

    protected void SetOff()
    {
        Clear();
        gameObject.SetActive(false);
    }
    public Define.SkillType GetSkillType() => this.skillType;
    public SkillData GetSkillData() => this.skillData;
}

