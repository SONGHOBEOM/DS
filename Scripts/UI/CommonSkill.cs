using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using UnityEngine.EventSystems;
using PlayerCORE;

public class CommonSkill : SkillBase, IPointerClickHandler
{
    protected override void OnEnable()
    {
        this.skillType = Define.SkillType.Common;
        base.OnEnable();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (coolTime > 0 || PlayerStateHelper.isAttacking == true)
            return;
        isCasted = true;
        PlayerAnimationManager.Instance.RunPlayerDodgeAnim(skillData);
        EffectSystem.Instance.ShowEffect(skillData.skillName);
    }
}
