using PlayerCORE;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerAnimationManager : Singleton<PlayerAnimationManager>
{
    private Animator animator;
    public void SetPlayerAnimator(Animator animator) => this.animator = animator;

    public void RunPlayerEquipWeapon(ItemData itemData)
    {
        if (animator == null) return;

        var weaponData = itemData as WeaponData;
        if (weaponData.weaponType == Define.WeaponType.Sword)
            animator.SetTrigger("EquipSword");
        else
            animator.SetTrigger("EquipBigSword");
    }

    public void RunPlayerUnEquipWeapon()
    {
        if (animator == null) return;

        animator.SetTrigger("UnEquip");
    }

    public void RunPlayerSkillAnim(SkillData skillData)
    {
        if (animator == null) return;

        PlayerStateHelper.isAttacking = true;
        animator.SetTrigger(skillData.skillName);
    }

    public void RunPlayerDodgeAnim(SkillData skillData)
    {
        if(animator == null) return;

        PlayerStateHelper.isDodging = true;
        SoundManager.Instance.PlayClip("Dodge");
        animator.SetTrigger(skillData.skillName);
    }

    public void RunPlayerKnockDownAnim()
    {
        if (animator == null) return;

        animator.SetTrigger("KnockDown");
    }

}
