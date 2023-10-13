using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActiveSkill : SkillBase, IPointerClickHandler
{
    protected override void OnEnable()
    {
        this.skillType = Define.SkillType.Active;
        base.OnEnable();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (coolTime > 0)
            return;
        isCasted = true;
        PlayerAnimationManager.Instance.RunPlayerSkillAnim(skillData);

        var weaponData = EntityManager.Instance.player.GetPlayerWeapon().GetWeaponData();
        EffectSystem.Instance.ShowActiveEffect(weaponData.itemId, skillData.skillName);
    }
}
